﻿using AutoMapper;
using DecorGearApplication.DataTransferObj.Category;
using DecorGearApplication.DataTransferObj.ImageList;
using DecorGearApplication.Interface;
using DecorGearApplication.ValueObj.Response;
using DecorGearDomain.Data.Entities;
using DecorGearDomain.Enum;
using DecorGearInfrastructure.Database.AppDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DecorGearInfrastructure.Implement
{
    public class ImageListRespository : IImageListRespository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public ImageListRespository(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<ResponseDto<ImageListDto>> CreateImage(CreateImageRequest request, CancellationToken cancellationToken)
        {
            // Kiểm Tra Tính Hợp Lệ của Dữ Liệu
            if (request == null)
            {
                return new ResponseDto<ImageListDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Chưa có request."
                };
            }
            // Thêm Sản Phẩm Mới
            try
            {
                if (!IsValidImageFormat(request.ImagePath))
                {
                    return new ResponseDto<ImageListDto>
                    {
                        DataResponse = null,
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Sai định dạng."
                    };
                }

                var createImageList = _mapper.Map<ImageList>(request);

                await _appDbContext.ImageLists.AddAsync(createImageList, cancellationToken);

                await _appDbContext.SaveChangesAsync(cancellationToken);

                return new ResponseDto<ImageListDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status200OK,
                    Message = "Tạo thành công."
                };
            }
            catch (DbUpdateException)
            {
                return new ResponseDto<ImageListDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status500InternalServerError,
                    Message = "Lỗi khi tạo cơ sở dữ liệu."
                };
            }
            catch (ArgumentException)
            {
                return new ResponseDto<ImageListDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Tham số không hợp lệ."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<ImageListDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Lỗi không xác định: " + ex.Message + "."
                };
            }
        }

        public async Task<ResponseDto<bool>> DeleteImage(int id, CancellationToken cancellationToken)
        {
            var deleteImageList = await _appDbContext.ImageLists.FindAsync(id, cancellationToken);
            if (deleteImageList != null)
            {
                _appDbContext.ImageLists.Remove(deleteImageList);
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

        public async Task<List<ImageListDto>> GetAllImage(CancellationToken cancellationToken)
        {
            var imageLists = await _appDbContext.ImageLists.ToListAsync(cancellationToken);

            return _mapper.Map<List<ImageListDto>>(imageLists);
        }

        public async Task<ImageListDto> GetImageById(int id, CancellationToken cancellationToken)
        {
            var imageLists = await _appDbContext.ImageLists.FindAsync(id, cancellationToken);

            return _mapper.Map<ImageListDto>(imageLists);
        }

        public bool IsValidImageFormat(string imagePath)
        {
            // Thư mục chứa ảnh trong server
            var rootDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

            // Kiểm tra nếu đường dẫn nằm trong thư mục "wwwroot/images"
            var fullImagePath = Path.GetFullPath(imagePath);

            if (!fullImagePath.StartsWith(rootDirectory))
            {
                return false; // Đường dẫn không hợp lệ
            }

            // Các định dạng hợp lệ
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
                return false; // Tệp không tồn tại
            }

            return true; // Tệp hợp lệ
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

        public async Task<ResponseDto<ImageListDto>> UpdateImage(int id, UpdateImageListRequest request, CancellationToken cancellationToken)
        {
            // Kiểm Tra Tính Hợp Lệ của Dữ Liệu
            if (request == null)
            {
                return new ResponseDto<ImageListDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Chưa có request."
                };
            }

            // Cập Nhật Sản Phẩm 
            try
            {
                var imageList = await _appDbContext.ImageLists.FindAsync(id);

                imageList.ImagePath = request.ImagePath;
                //imageList.MouseDetailID = request.MouseDetailID;
                //imageList.KeyboardDetailID = request.KeyboardDetailID;

                if (!IsValidImageFormat(request.ImagePath))
                {
                    return new ResponseDto<ImageListDto>
                    {
                        DataResponse = null,
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Sai định dạng."
                    };
                }

                _appDbContext.ImageLists.Update(imageList);

                await _appDbContext.SaveChangesAsync(cancellationToken);

                return new ResponseDto<ImageListDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status200OK,
                    Message = "Cật nhật thành công."
                };
            }
            catch (DbUpdateException)
            {
                return new ResponseDto<ImageListDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status500InternalServerError,
                    Message = "Lỗi khi cập nhật cơ sở dữ liệu."
                };
            }
            catch (ArgumentException)
            {
                return new ResponseDto<ImageListDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Tham số không hợp lệ."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<ImageListDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Lỗi không xác định: " + ex.Message + "."
                };
            }
        }
    }
}
