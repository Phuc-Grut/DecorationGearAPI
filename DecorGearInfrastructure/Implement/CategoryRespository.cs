using AutoMapper;
using DecorGearApplication.DataTransferObj.Brand;
using DecorGearApplication.DataTransferObj.Category;
using DecorGearApplication.DataTransferObj.KeyBoardDetails;
using DecorGearApplication.DataTransferObj.MouseDetails;
using DecorGearApplication.Interface;
using DecorGearApplication.ValueObj.Response;
using DecorGearDomain.Data.Entities;
using DecorGearDomain.Enum;
using DecorGearInfrastructure.Database.AppDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DecorGearInfrastructure.Implement
{
    public class CategoryRespository : ICategoryRespository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public CategoryRespository(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ResponseDto<CategoryDto>> CreateCategory(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return new ResponseDto<CategoryDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Chưa có request."
                };
            }
            try
            {

                var createCategory = _mapper.Map<Category>(request);

                await _appDbContext.Categories.AddAsync(createCategory, cancellationToken);

                await _appDbContext.SaveChangesAsync(cancellationToken);

                return new ResponseDto<CategoryDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status200OK,
                    Message = "Tạo thành công."
                };
            }
            catch (DbUpdateException)
            {
                return new ResponseDto<CategoryDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status500InternalServerError,
                    Message = "Lỗi khi tạo cơ sở dữ liệu."
                };
            }
            catch (ArgumentException)
            {
                return new ResponseDto<CategoryDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Tham số không hợp lệ."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<CategoryDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Lỗi không xác định: " + ex.Message + "."
                };
            }
        }

        public async Task<ResponseDto<bool>> DeleteCategory(int id, CancellationToken cancellationToken)
        {
            var keyResult = await _appDbContext.Categories.FindAsync(id, cancellationToken);
            if (keyResult != null)
            {
                _appDbContext.Categories.Remove(keyResult);
                _appDbContext.SaveChangesAsync();
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

        public async Task<List<CategoryDto>> GetAllCategory(CancellationToken cancellationToken)
        {
            var result = await _appDbContext.Categories.ToListAsync(cancellationToken);

            return _mapper.Map<List<CategoryDto>>(result);
        }

        public async Task<ResponseDto<CategoryDto>> UpdateCategory(int id, UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            if (request != null)
            {
                return new ResponseDto<CategoryDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Chưa có request."
                };
            }
            try
            {
                var category = await _appDbContext.Categories.FindAsync(id);

                category.CategoryName = request.CategoryName;

                _appDbContext.Categories.Update(category);

                await _appDbContext.SaveChangesAsync(cancellationToken);

                return new ResponseDto<CategoryDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Chưa có request."
                };
            }
            catch (DbUpdateException)
            {
                return new ResponseDto<CategoryDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status500InternalServerError,
                    Message = "Lỗi khi cập nhật cơ sở dữ liệu."
                };
            }
            catch (ArgumentException)
            {
                return new ResponseDto<CategoryDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Tham số không hợp lệ."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<CategoryDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Lỗi không xác định: " + ex.Message + "."
                };
            }
        }

        public async Task<List<CategoryProductDto>> GetCategoryProductById(int id , CancellationToken cancellationToken)
        {
             var category = await _appDbContext.Categories
                .Include(c => c.SubCategories)
                    .ThenInclude(sc => sc.ProductSubCategories)
                        .ThenInclude(psc => psc.Product)
                            .ThenInclude(p => p.MouseDetails)
                .Include(c => c.SubCategories)
                    .ThenInclude(sc => sc.ProductSubCategories)
                        .ThenInclude(psc => psc.Product)
                            .ThenInclude(p => p.KeyboardDetails)
                .Include(c => c.SubCategories)
                    .ThenInclude(sc => sc.ProductSubCategories)
                        .ThenInclude(psc => psc.Product)
                            .ThenInclude(p => p.ImageLists)
                .FirstOrDefaultAsync(c=>c.CategoryID == id, cancellationToken);
            if (category == null)
            {
                return null;
            }

            var products = category.SubCategories.SelectMany(sc => sc.ProductSubCategories).Select(psc => psc.Product).ToList();
            var firstProduct = products.FirstOrDefault(); 
            if (firstProduct == null) 
            { 
                return null; 
            }

            var CategoryProductDtos = products.Select(firstProduct => new CategoryProductDto
            {
                ProductID = firstProduct.ProductID,
                ProductName = firstProduct.ProductName,
                ProductCode = firstProduct.ProductCode,
                AvatarProduct = firstProduct.AvatarProduct,
                ImageProduct = firstProduct.ImageLists?.Select(img => img.ImagePath).ToList() ?? new List<string>(),
                Description = firstProduct.Description,
                SubCategories = firstProduct.ProductSubCategories
                    .Select(psc => psc.SubCategory.SubCategoryName)
                    .ToList(),
                CategoryID = firstProduct.ProductSubCategories
                    .Select(psc => psc.SubCategory.Category.CategoryID)
                    .FirstOrDefault(),
                CategoryName = firstProduct.ProductSubCategories?
                    .Select(psc => psc.SubCategory?.Category?.CategoryName)
                    .FirstOrDefault() ?? "Unknown Category",

                Quantity = firstProduct.ProductSubCategories.Any(psc => psc.SubCategory.Category.CategoryID == 1)
                            ? firstProduct.MouseDetails.Sum(md => md.Quantity)
                            : firstProduct.ProductSubCategories.Any(psc => psc.SubCategory.Category.CategoryID == 2)
                            ? firstProduct.KeyboardDetails.Sum(kd => kd.Quantity)
                            : 0,

                ProductDetail = firstProduct.ProductSubCategories?.Any(psc => psc.SubCategory?.Category?.CategoryID == 1) == true
                    ? (object?)firstProduct.MouseDetails?.Select(md => new MouseDetailsDto
                    {
                        MouseDetailID = md.MouseDetailID,
                        Color = md.Color,
                        Price = md.Price,
                        Quantity = md.Quantity,
                        Switch = md.Switch,
                        Weight = md.Weight,
                        Size = md.Size,
                        DPI = md.DPI ?? 0,
                        Connectivity = md.Connectivity,
                        Dimensions = md.Dimensions,
                        Material = md.Material,
                        EyeReading = md.EyeReading,
                        BatteryCapacity = md.BatteryCapacity,
                        Button = md.Button,
                        LED = md.LED,
                        SS = md.SS
                    }).ToList()
                    : firstProduct.ProductSubCategories?.Any(psc => psc.SubCategory?.Category?.CategoryID == 2) == true
                    ? (object?)firstProduct.KeyboardDetails?.Select(kd => new KeyBoardDetailsDto
                    {
                        KeyboardDetailID = kd.KeyboardDetailID,
                        Color = kd.Color,
                        Price = kd.Price,
                        Quantity = kd.Quantity,
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
            }).ToList();

            return CategoryProductDtos;
        }
    }
}
