using AutoMapper;
using DecorGearApplication.DataTransferObj.Brand;
using DecorGearApplication.DataTransferObj.Category;
using DecorGearApplication.DataTransferObj.KeyBoardDetails;
using DecorGearApplication.DataTransferObj.MouseDetails;
using DecorGearApplication.DataTransferObj.Product;
using DecorGearApplication.DataTransferObj.SubCategory;
using DecorGearApplication.Interface;
using DecorGearApplication.ValueObj.Response;
using DecorGearDomain.Data.Entities;
using DecorGearDomain.Enum;
using DecorGearInfrastructure.Database.AppDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecorGearInfrastructure.Implement
{
    public class SubCategoryRespository : ISubCategoryRespository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public SubCategoryRespository(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<ResponseDto<SubCategoryDto>> CreateSubCategory(CreateSubCategoryRequest request, CancellationToken cancellationToken)
        {
            // Kiểm Tra Tính Hợp Lệ của Dữ Liệu
            if (request == null)
            {
                return new ResponseDto<SubCategoryDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Chưa có request."
                };
            }
            // Thêm Sản Phẩm Mới
            try
            {
                var createSubCategory = _mapper.Map<SubCategory>(request);

                await _appDbContext.SubCategories.AddAsync(createSubCategory, cancellationToken);

                await _appDbContext.SaveChangesAsync(cancellationToken);

                return new ResponseDto<SubCategoryDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Tạo thành công."
                };
            }
            catch (DbUpdateException)
            {
                return new ResponseDto<SubCategoryDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status500InternalServerError,
                    Message = "Lỗi khi tạo cơ sở dữ liệu."
                };
            }
            catch (ArgumentException)
            {
                return new ResponseDto<SubCategoryDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Tham số không hợp lệ."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<SubCategoryDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Lỗi không xác định: " + ex.Message + "."
                };
            }
        }

        public async Task<ResponseDto<bool>> DeleteSubCategory(int id, CancellationToken cancellationToken)
        {
            var deleteSubCategory = await _appDbContext.SubCategories.FindAsync(id, cancellationToken);
            if (deleteSubCategory != null)
            {
                _appDbContext.SubCategories.Remove(deleteSubCategory);
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

        public async Task<List<SubCategoryDto>> GetAllSubCategory(CancellationToken cancellationToken)
        {
            var subCategory = await _appDbContext.SubCategories.ToListAsync(cancellationToken);

            return _mapper.Map<List<SubCategoryDto>>(subCategory);
        }

        public async Task<List<SubCategoryProductDto>> GetSubCategoryProductById(int id, CancellationToken cancellationToken)
        {
            var subCategory = await _appDbContext.SubCategories
                .Include(sc => sc.ProductSubCategories)
                    .ThenInclude(sc => sc.Product)
                            .ThenInclude(p => p.MouseDetails)
                .Include(sc => sc.ProductSubCategories)
                    .ThenInclude(psc => psc.Product)
                            .ThenInclude(p => p.KeyboardDetails)
                .Include(sc => sc.ProductSubCategories)
                        .ThenInclude(psc => psc.Product)
                            .ThenInclude(p => p.ImageLists)
                .Include(sc => sc.Category)
                .FirstOrDefaultAsync(sc => sc.SubCategoryID == id, cancellationToken);
            if (subCategory == null)
            {
                return null;
            }

            var subCategoryProductDtos = subCategory.ProductSubCategories.Select(psc => new SubCategoryProductDto 
            { 
                ProductID = psc.Product.ProductID, 
                ProductName = psc.Product.ProductName, 
                ProductCode = psc.Product.ProductCode, 
                View = psc.Product.View, 
                AvatarProduct = psc.Product.AvatarProduct, 
                ImageProduct = psc.Product.ImageLists?.Select(img => img.ImagePath).ToList() ?? new List<string>(), 
                Description = psc.Product.Description, 
                CategoryID = psc.SubCategory.Category.CategoryID, 
                SubCategoryID = psc.SubCategory.SubCategoryID, 
                SubCategoryName = psc.SubCategory?.SubCategoryName ?? "Unknown SubCategory", 
                ProductDetail = psc.SubCategory.Category.CategoryID switch 
                { 
                    1 => (object?)psc.Product.MouseDetails?.Select(md => new MouseDetailsDto 
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
                    }).ToList(), 
                    2 => (object?)psc.Product.KeyboardDetails?.Select(kd => new KeyBoardDetailsDto 
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
                    }).ToList(), 
                    _ => null 
                } 
            }).ToList();

            return subCategoryProductDtos;
        }

            public async Task<ResponseDto<SubCategoryDto>> UpdateSubCategory(int id,UpdateSubCategoryRequest request, CancellationToken cancellationToken)
        {
            // Kiểm Tra Tính Hợp Lệ của Dữ Liệu
            if (request == null)
            {
                return new ResponseDto<SubCategoryDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Chưa có request."
                };
            }

            // Cập Nhật Sản Phẩm 
            try
            {
                var subCategory = await _appDbContext.SubCategories.FindAsync(id,cancellationToken);

                subCategory.SubCategoryName = request.SubCategoryName;
                subCategory.CategoryID = request.CategoryID;

                _appDbContext.SubCategories.Update(subCategory);

                await _appDbContext.SaveChangesAsync(cancellationToken);

                return new ResponseDto<SubCategoryDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Cập nhật thành công."
                };
            }
            catch (DbUpdateException)
            {
                return new ResponseDto<SubCategoryDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status500InternalServerError,
                    Message = "Lỗi khi cập nhật cơ sở dữ liệu."
                };
            }
            catch (ArgumentException)
            {
                return new ResponseDto<SubCategoryDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Tham số không hợp lệ."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<SubCategoryDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Lỗi không xác định: " + ex.Message + "."
                };
            }
        }
    }
}
