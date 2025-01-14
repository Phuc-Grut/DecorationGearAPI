using AutoMapper;
using DecorGearApplication.DataTransferObj.ImageList;
using DecorGearApplication.DataTransferObj.Order;
using DecorGearApplication.DataTransferObj.OrderDetail;
using DecorGearApplication.DataTransferObj.Product;
using DecorGearApplication.Interface;
using DecorGearApplication.ValueObj.Response;
using DecorGearDomain.Data.Entities;
using DecorGearDomain.Enum;
using DecorGearInfrastructure.Database.AppDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DecorGearInfrastructure.implement
{
    public class OrderRepository : IOderRespository
    {
        private readonly AppDbContext _dbcontext;
        private readonly IMapper _mapper;

        public OrderRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbcontext = dbContext;
            _mapper = mapper;
        }
        public async Task<ResponseDto<OrderDto>> CreateOder(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return new ResponseDto<OrderDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Chưa có request."
                };
            }
            if (request.completeDate < DateTime.Now)
            {
                return new ResponseDto<OrderDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "ngày hoàn thành phải sau hoặc cùng thời gian tạo hóa đơn."
                };
            }

            try
            {
                var createOrder = _mapper.Map<Order>(request);

                await _dbcontext.Orders.AddAsync(createOrder, cancellationToken);
                await _dbcontext.SaveChangesAsync(cancellationToken);

                var orderDto = _mapper.Map<OrderDto>(createOrder);

                return new ResponseDto<OrderDto>
                {
                    DataResponse = orderDto,
                    Status = StatusCodes.Status201Created,
                    Message = "Tạo sản phẩm thành công."
                };
            }
            catch (DbUpdateException)
            {
                return new ResponseDto<OrderDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status500InternalServerError,
                    Message = "Lỗi khi tạo cơ sở dữ liệu."
                };
            }
            catch (ArgumentException)
            {
                return new ResponseDto<OrderDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Tham số không hợp lệ."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<OrderDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Lỗi không xác định: " + ex.Message + "."
                };
            }
        }

        public async Task<ResponseDto<bool>> DeleteOder(int id, CancellationToken cancellationToken)
        {
            var deleteOrder = await _dbcontext.Orders.FindAsync(id, cancellationToken);
            if (deleteOrder != null)
            {
                _dbcontext.Orders.Remove(deleteOrder);
                _dbcontext.SaveChanges();
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

        public async Task<List<OrderDto>> GetAllOder(CancellationToken cancellationToken)
        {
            var order = await _dbcontext.Orders
                .Include(o => o.OrderDetails)
                .ToListAsync(cancellationToken);
            return order.Select(o => new OrderDto
            {
                OrderID = o.OrderID,
                UserID = o.UserID,
                VoucherID = o.VoucherID,
                totalQuantity = o.OrderDetails.Sum(od => od.Quantity),
                totalPrice = o.OrderDetails.Sum(od => od.UnitPrice * od.Quantity),
                paymentMethod = o.paymentMethod,
                completeDate = o.OrderDate,
                OrderStatus = o.OrderStatus,

                orderDetailDTOs = o.OrderDetails.Select(od => new OrderDetailDTO
                {
                    OrderDetailId = od.OrderDetailId,
                    ProductID = od.ProductID,
                    UnitPrice = od.UnitPrice,
                    Quantity = od.Quantity
                }).ToList(),

            }).ToList();
        }

        public async Task<OrderDto> GetKeyOderById(int id, CancellationToken cancellationToken)
        {
            var order = await _dbcontext.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.OrderID == id, cancellationToken);

            if (order == null)
            {
                return null;
            }

            return new OrderDto
            {
                OrderID = order.OrderID,
                UserID = order.UserID,
                totalQuantity = order.OrderDetails.Sum(od => od.Quantity),
                totalPrice = order.OrderDetails.Sum(od => od.UnitPrice * od.Quantity),
                paymentMethod = order.paymentMethod,
                completeDate = order.OrderDate,
                OrderStatus = order.OrderStatus,

                orderDetailDTOs = order.OrderDetails.Select(od => new OrderDetailDTO
                {
                    OrderDetailId = od.OrderDetailId,
                    ProductID = od.ProductID,
                    UnitPrice = od.UnitPrice,
                    Quantity = od.Quantity,
                    size = od.size, // Nếu có trường Size, bạn có thể thêm
                    weight = od.weight // Nếu có trường Weight, bạn có thể thêm
                }).ToList(),
            };
        }

        public async Task<ResponseDto<OrderDto>> UpdateOder(int id, UpdateOrderRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return new ResponseDto<OrderDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Chưa có request."
                };
            }

            try
            {
                var updateOrder = await _dbcontext.Orders.FindAsync(id, cancellationToken);
                if (updateOrder == null)
                {
                    return new ResponseDto<OrderDto>
                    {
                        DataResponse = null,
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Chưa có request."
                    };
                }
                else
                {
                    updateOrder.UserID = request.UserID;
                    updateOrder.VoucherID = request.VoucherID;
                    updateOrder.paymentMethod = request.paymentMethod;
                    updateOrder.OrderDate = request.OrderDate;
                    updateOrder.OrderStatus = request.OrderStatus;

                    // Cập nhật thông tin sản phẩm
                    _dbcontext.Orders.Update(updateOrder);

                    // Lưu các thay đổi vào cơ sở dữ liệu
                    await _dbcontext.SaveChangesAsync(cancellationToken);

                    // Trả về kết quả thành công
                    return new ResponseDto<OrderDto>
                    {
                        DataResponse = null,
                        Status = StatusCodes.Status200OK,
                        Message = "Cập nhật sản phẩm thành công."
                    };
                }
            }
            catch (DbUpdateException)
            {
                return new ResponseDto<OrderDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status500InternalServerError,
                    Message = "Lỗi khi cập nhật cơ sở dữ liệu."
                };
            }
            catch (ArgumentException)
            {
                return new ResponseDto<OrderDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Tham số không hợp lệ."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<OrderDto>
                {
                    DataResponse = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Lỗi không xác định: " + ex.Message + "."
                };
            }

        }
    }
}
