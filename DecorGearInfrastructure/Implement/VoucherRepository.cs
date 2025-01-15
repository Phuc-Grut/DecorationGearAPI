using DecorGearApplication.DataTransferObj.User;
using DecorGearApplication.DataTransferObj.Voucher;
using DecorGearApplication.Interface;
using DecorGearDomain.Data.Entities;
using DecorGearDomain.Enum;
using DecorGearInfrastructure.Database.AppDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DecorGearInfrastructure.implement
{
    public class VoucherRepository : IVoucherRespository
    {
        private readonly AppDbContext _dbContext;
        public VoucherRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public async Task<ResponseDto<Voucher>> CreateVoucher(CreateVoucherRequest request, CancellationToken cancellationToken)
        {
            // Kiểm tra xem yêu cầu có null hay không
            if (request == null)
            {
                return new ResponseDto<Voucher>
                {
                    Data = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Request is null"
                };
            }

            // Kiểm tra xem tên voucher đã tồn tại chưa
            var checkNameVoucher = await _dbContext.Vouchers.AnyAsync(x => x.VoucherName == request.VoucherName, cancellationToken);
            if (checkNameVoucher)
            {
                return new ResponseDto<Voucher>
                {
                    Data = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Voucher name already exists"
                };
            }

            // Tạo voucher mới
            var newVoucher = new Voucher
            {
                VoucherName = request.VoucherName,
                VoucherPercent = (double)request.VoucherPercent / 100, // Chuyển đổi phần trăm
                Status = request.Status,
                expiry = request.expiry, // Giả định rằng bạn đã có trường Expiry trong Voucher
                CreatedTime = DateTime.Now
            };

            // Lưu voucher vào cơ sở dữ liệu
            await _dbContext.Vouchers.AddAsync(newVoucher, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto<Voucher>
            {
                Data = newVoucher,
                Status = StatusCodes.Status200OK,
                Message = "Success"
            };
        }

        public async Task<ResponseDto<Voucher>> DeleteVoucher(int id, DeleteVoucherRequest request, CancellationToken cancellationToken)
        {
            // Kiểm tra yêu cầu null
            if (request == null)
            {
                return new ResponseDto<Voucher>
                {
                    Data = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Request is null"
                };
            }

            // Tìm voucher trong cơ sở dữ liệu
            var delVoucher = await _dbContext.Vouchers.FirstOrDefaultAsync(v => v.VoucherID == request.VoucherID, cancellationToken);

            // Kiểm tra voucher có tồn tại hay không
            if (delVoucher == null)
            {
                return new ResponseDto<Voucher>
                {
                    Data = null,
                    Status = StatusCodes.Status404NotFound,
                    Message = "Voucher not found"
                };
            }

            // Kiểm tra trạng thái voucher
            if (delVoucher.Status == EntityStatus.Active)
            {
                return new ResponseDto<Voucher>
                {
                    Data = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Voucher is currently active"
                };
            }

            // Kiểm tra xem voucher có đang được sử dụng trong đơn hàng hay không
            var isVoucherUsed = await _dbContext.Orders.AnyAsync(o => o.VoucherID == request.VoucherID);
            if (isVoucherUsed)
            {
                return new ResponseDto<Voucher>
                {
                    Data = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Voucher is currently in use"
                };
            }

            // Xóa voucher
            _dbContext.Vouchers.Remove(delVoucher);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto<Voucher>
            {
                Data = delVoucher,
                Status = StatusCodes.Status200OK,
                Message = "Voucher deleted successfully"
            };
        }

        public Task<ResponseDto<Voucher>> DeleteVoucher(DeleteVoucherRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<VoucherDto>> GetAllVoucher(VoucherSearch? voucherSearch, CancellationToken cancellationToken)
        {
            var voucher = _dbContext.Vouchers.Select(x => new VoucherDto
            {
                VoucherID = x.VoucherID,
                VoucherName = x.VoucherName,
                VoucherPercent = x.VoucherPercent,
                expiry = x.expiry,
                CreatedTime = x.CreatedTime,
                Status = x.Status
            }).AsQueryable();
            if (voucherSearch != null)
            {
                if (!string.IsNullOrEmpty(voucherSearch.VoucherName))
                {
                    voucher = voucher.Where(v => v.VoucherName.Contains(voucherSearch.VoucherName));
                }
                if (voucherSearch.Status.HasValue)
                {
                    voucher = voucher.Where(v => v.Status == voucherSearch.Status);
                }
                if (voucherSearch.VoucherPercent.HasValue)
                {
                    voucher = voucher.Where(v => v.VoucherPercent == voucherSearch.VoucherPercent);
                }
            }
            return await voucher.ToListAsync(cancellationToken);
        }

        public async Task<ResponseDto<VoucherDto>> GetKeyVoucherById(ViewVoucherRequest request, CancellationToken cancellationToken)
        {
            var voucher = await _dbContext.Vouchers.FirstOrDefaultAsync(v => v.VoucherID == request.VoucherID, cancellationToken);
            if (voucher == null)
            {
                return new ResponseDto<VoucherDto>
                {
                    Data = null,
                    Status = StatusCodes.Status404NotFound,
                    Message = "Voucher not found"
                };
            }
            else
            {
                return new ResponseDto<VoucherDto>
                {
                    Data = new VoucherDto
                    {
                        VoucherID = voucher.VoucherID,
                        VoucherName = voucher.VoucherName,
                        VoucherPercent = voucher.VoucherPercent,
                        Status = voucher.Status,
                        expiry = voucher.expiry,
                        CreatedTime = voucher.CreatedTime
                    },
                    Status = StatusCodes.Status200OK,
                    Message = "Success"
                };
            }
        }

        public async Task<ResponseDto<Voucher>> UpdateVoucher(int id, UpdateVoucherRequest request, CancellationToken cancellationToken)
        {
            // Tìm voucher theo ID
            var udVoucher = await _dbContext.Vouchers.FirstOrDefaultAsync(v => v.VoucherID == id, cancellationToken);

            // Kiểm tra xem voucher có tồn tại không
            if (udVoucher == null)
            {
                return new ResponseDto<Voucher>
                {
                    Data = null,
                    Status = StatusCodes.Status404NotFound,
                    Message = "Voucher not found"
                };
            }

            // Kiểm tra ngày hết hạn
            if (request.expiry < udVoucher.CreatedTime)
            {
                return new ResponseDto<Voucher>
                {
                    Data = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Expiry date must be greater than created date"
                };
            }
            if (request.expiry > DateTime.Now.AddMonths(3))
            {
                return new ResponseDto<Voucher>
                {
                    Data = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Expiry date must be less than 3 months from now"
                };
            }

            // Kiểm tra tên voucher đã tồn tại
            var voucherNameCheck = await _dbContext.Vouchers.AnyAsync(x => x.VoucherName == request.VoucherName && x.VoucherID != id);
            if (voucherNameCheck)
            {
                return new ResponseDto<Voucher>
                {
                    Data = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Voucher name already exists"
                };
            }

            // Cập nhật thông tin voucher
            udVoucher.VoucherName = request.VoucherName;
            udVoucher.VoucherPercent = request.VoucherPercent / 100.0; // Chia cho 100.0 để đảm bảo kiểu double
            udVoucher.expiry = request.expiry;
            udVoucher.Status = request.Status;
            udVoucher.ModifiedTime = DateTimeOffset.Now;

            // Lưu thay đổi vào cơ sở dữ liệu
            _dbContext.Vouchers.Update(udVoucher);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto<Voucher>
            {
                Data = udVoucher,
                Status = StatusCodes.Status200OK,
                Message = "Voucher updated successfully"
            };
        }
    }
}