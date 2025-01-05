using AutoMapper;
using DecorGearApplication.DataTransferObj.ImageList;
using DecorGearApplication.DataTransferObj.KeyBoardDetails;
using DecorGearApplication.DataTransferObj.MouseDetails;
using DecorGearApplication.DataTransferObj.Product;
using DecorGearApplication.Interface;
using DecorGearApplication.ValueObj.Response;
using DecorGearDomain.Data.Entities;
using DecorGearDomain.Enum;
using DecorGearInfrastructure.Database.AppDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
        

namespace DecorGearInfrastructure.Implement
{
    public class ProductRespository : IProductRespository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public ProductRespository(AppDbContext context, IMapper mapper)
        {
            _appDbContext = context;
            _mapper = mapper;
        }
        public async Task<string> GenerateProductCodeAsync()
        {
            var maxProductCode = await _appDbContext.Products
                .OrderByDescending(p => p.ProductCode)
                .Select(p => p.ProductCode)
                .FirstOrDefaultAsync();

            int nextNumber = 1;
            if (!string.IsNullOrEmpty(maxProductCode))
            {
                string numberPart = maxProductCode.Substring(2);
                if (int.TryParse(numberPart, out int currentNumber))
                {
                    nextNumber = currentNumber + 1;
                }
            }
            return $"SP{nextNumber:D2}";
        }

        public async Task<ResponseDto<ProductDto>> CreateProduct(CreateProductRequest request, CancellationToken cancellationToken)
        {
            // Kiểm tra tính hợp lệ của dữ liệu
            if (request == null)
            {
                return new ResponseDto<ProductDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Chưa có request."
                };
            }

            try
            {
                // Kiểm tra định dạng hình ảnh
                if (!IsValidImageFormat(request.AvatarProduct))
                {
                    return new ResponseDto<ProductDto>
                    {
                        DataResponse = null,
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Sai định dạng hình ảnh."
                    };
                }
                var productCode = await GenerateProductCodeAsync();
                var createProduct = _mapper.Map<Product>(request);
                createProduct.ProductCode = productCode;

                await _appDbContext.Products.AddAsync(createProduct, cancellationToken);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                var productDto = _mapper.Map<ProductDto>(createProduct);

                return new ResponseDto<ProductDto>
                {
                    DataResponse = productDto,
                    Status = StatusCodes.Status201Created,
                    Message = "Tạo sản phẩm thành công."
                };
            }
            catch (DbUpdateException)
            {
                return new ResponseDto<ProductDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status500InternalServerError,
                    Message = "Lỗi khi tạo cơ sở dữ liệu."
                };
            }
            catch (ArgumentException)
            {
                return new ResponseDto<ProductDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Tham số không hợp lệ."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<ProductDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Lỗi không xác định: " + ex.Message + "."
                };
            }
        }


        public async Task<ResponseDto<bool>> DeleteProduct(int id, CancellationToken cancellationToken)
        {
            var deleteProducts = await _appDbContext.Products.FindAsync(id, cancellationToken);
            if (deleteProducts != null)
            {
                _appDbContext.Products.Remove(deleteProducts);
                _appDbContext.SaveChanges();
                return new ResponseDto<bool>
                {
                    DataResponse = true,
                    Status = StatusCodes.Status200OK,
                    Message = "Xóa thành công."
                };
            }
            return new ResponseDto<bool>
            {
                DataResponse = false,
                Status = StatusCodes.Status400BadRequest,
                Message = "Sửa thất bại."
            };
        }

        public bool IsValidImageFormat(string imagePath)
        {
            var rootDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

            var fullImagePath = Path.GetFullPath(imagePath);

            if (!fullImagePath.StartsWith(rootDirectory))
            {
                return false;
            }

            var validExtensions = new List<string> { ".jpg", ".jpeg" };

            // Lấy phần mở rộng của tệp
            var extension = Path.GetExtension(imagePath)?.ToLower();

            // Kiểm tra phần mở rộng
            if (!validExtensions.Contains(extension))
            {
                return false; // Phần mở rộng không hợp lệ
            }

            // Kiểm tra file có tồn tại không
            if (!File.Exists(fullImagePath))
            {
                return false;
            }

            return true;
        }

        //public bool IsValidImageFormat(string imagePath)
        //{
        //    // Các định dạng hợp lệ
        //    var validExtensions = new List<string> { ".jpg", ".jpeg" };

        //    // Lấy phần mở rộng của tệp
        //    var extension = Path.GetExtension(imagePath)?.ToLower();

        //    // Nếu phần mở rộng không hợp lệ, trả về false
        //    if (!validExtensions.Contains(extension))
        //    {
        //        return false;
        //    }

        //    // Tất cả các tệp đều hợp lệ
        //    return true;
        //}

        public async Task<List<ProductDto>> GetAllProduct(ViewProductRequest? request, CancellationToken cancellationToken)
        {
            var query = _appDbContext.Products
                .Include(p => p.ProductSubCategories)
                    .ThenInclude(psc => psc.SubCategory)
                    .ThenInclude(sc => sc.Category)
                .Include(p => p.MouseDetails)
                .Include(p => p.KeyboardDetails)
                .Include(p => p.ImageLists)
                .AsQueryable();

            if (request.ProductID.HasValue)
            {
                query = query.Where(p => p.ProductID == request.ProductID);
            }
            if (!string.IsNullOrEmpty(request.ProductName))
            {
                query = query.Where(p => p.ProductName.Contains(request.ProductName));
            }
            if (request.Price.HasValue)
            {
                query = query.Where(p => p.Price == request.Price);
            }
            if (request.View.HasValue)
            {
                query = query.Where(p => p.View == request.View);
            }
            if (request.Quantity.HasValue)
            {
                query = query.Where(p => p.Quantity == request.Quantity);
            }
            if (request.Weight.HasValue)
            {
                query = query.Where(p => p.Weight == request.Weight);
            }
            if (!string.IsNullOrEmpty(request.Description))
            {
                query = query.Where(p => p.Description.Contains(request.Description));
            }
            if (!string.IsNullOrEmpty(request.Size))
            {
                query = query.Where(p => p.Size == request.Size);
            }
            if (request.BatteryCapacity.HasValue)
            {
                query = query.Where(p => p.BatteryCapacity == request.BatteryCapacity);
            }

            var products = await query.Select(p => new ProductDto
            {
                ProductID = p.ProductID,
                ProductName = p.ProductName,
                ProductCode = p.ProductCode,
                Price = p.Price,
                Category = p.ProductSubCategories
                .Select(psc => psc.SubCategory.Category.CategoryName)
                .FirstOrDefault(),
                SubCategories = p.ProductSubCategories
                 .Select(psc => psc.SubCategory.SubCategoryName)
                 .ToList(),
                SaleID = p.Sale.SaleID,
                SaleCode = p.Sale.SalePercent,
                ImageProduct = p.ImageLists.Select(img => img.ImagePath).ToList(),
                ProductDetail = p.ProductSubCategories.Any(psc => psc.SubCategory.Category.CategoryID == 1)
                ? (object?)p.MouseDetails.Select(md => new MouseDetailsDto
                {
                    MouseDetailID = md.MouseDetailID,
                    Color = md.Color,
                    DPI = md.DPI,
                    Connectivity = md.Connectivity,
                    Dimensions = md.Dimensions,
                    Material = md.Material,
                    EyeReading = md.EyeReading,
                    Button = md.Button,
                    LED = md.LED,
                    SS = md.SS
                }).ToList()
                : p.ProductSubCategories.Any(psc => psc.SubCategory.Category.CategoryID == 2)
                ? (object?)p.KeyboardDetails.Select(kd => new KeyBoardDetailsDto
                {
                    KeyboardDetailID = kd.KeyboardDetailID,
                    Color = kd.Color,
                    Layout = kd.Layout,
                    Case = kd.Case,
                    Switch = kd.Switch,
                    SwitchLife = kd.SwitchLife,
                    Led = kd.Led,
                    KeycapMaterial = kd.KeycapMaterial,
                    SwitchMaterial = kd.SwitchMaterial,
                    SS = kd.SS,
                    Stabilizes = kd.Stabilizes,
                    PCB = kd.PCB,
                }).ToList()
                : null
            }).ToListAsync(cancellationToken);

            return products;
        }

        public async Task<ProductDto> GetKeyProductById(int id, CancellationToken cancellationToken)
        {
            var productIds = await _appDbContext.Products.FindAsync(id, cancellationToken);

            return _mapper.Map<ProductDto>(productIds);
        }

        public async Task<ResponseDto<ProductDto>> UpdateProduct(int id, UpdateProductRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return new ResponseDto<ProductDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Chưa có request."
                };
            }

            try
            {
                var product = await _appDbContext.Products
                    .Include(p => p.ProductSubCategories)
                    .FirstOrDefaultAsync(p => p.ProductID == id, cancellationToken);

                if (product == null)
                {
                    return new ResponseDto<ProductDto>
                    {
                        DataResponse = null,
                        Status = StatusCodes.Status404NotFound,
                        Message = "Không tìm thấy sản phẩm."
                    };
                }

                // Cập nhật thông tin của sản phẩm
                product.ProductName = request.ProductName;
                product.Price = request.Price;
                product.View = request.View;
                product.Quantity = request.Quantity;
                product.Weight = request.Weight;
                product.Description = request.Description;
                product.Size = request.Size;
                product.BatteryCapacity = request.BatteryCapacity;
                product.SaleID = request.SaleID;
                product.BrandID = request.BrandID;
                product.AvatarProduct = request.AvatarProduct;

                if (!IsValidImageFormat(request.AvatarProduct))
                {
                    return new ResponseDto<ProductDto>
                    {
                        DataResponse = null,
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Sai định dạng hình ảnh."
                    };
                }

                if (request.SubCategoryIDs != null && request.SubCategoryIDs.Any())
                {
                    foreach (var subCategoryId in request.SubCategoryIDs)
                    {
                        product.ProductSubCategories.Add(new ProductSubCategory
                        {
                            ProductID = product.ProductID,
                            SubCategoryID = subCategoryId
                        });
                    }
                }

                // Cập nhật thông tin sản phẩm
                _appDbContext.Products.Update(product);

                // Lưu các thay đổi vào cơ sở dữ liệu
                await _appDbContext.SaveChangesAsync(cancellationToken);

                // Trả về kết quả thành công
                return new ResponseDto<ProductDto>
                {
                    DataResponse = new ProductDto
                    {
                        ProductID = product.ProductID,
                        ProductName = product.ProductName,
                        Price = product.Price,
                        Category = string.Join(", ", product.ProductSubCategories.Select(psc => psc.SubCategory.SubCategoryName).ToList()),
                        SaleID = product.SaleID,
                        BrandId = product.BrandID,
                        AvatarProduct = product.AvatarProduct
                    },
                    Status = StatusCodes.Status200OK,
                    Message = "Cập nhật sản phẩm thành công."
                };
            }
            catch (DbUpdateException)
            {
                return new ResponseDto<ProductDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status500InternalServerError,
                    Message = "Lỗi khi cập nhật cơ sở dữ liệu."
                };
            }
            catch (ArgumentException)
            {
                return new ResponseDto<ProductDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Tham số không hợp lệ."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<ProductDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Lỗi không xác định: " + ex.Message + "."
                };
            }
        }

    }
}