using Application.DataTransferObj.User.Request;
using DecorGearApplication.DataTransferObj.Order;
using DecorGearApplication.DataTransferObj.Product;
using DecorGearApplication.ValueObj.Response;
using DecorGearDomain.Enum;
using System.Collections.Generic;

namespace DecorGearApplication.Interface
{
    public interface IOderRespository
    {
        Task<List<OrderDto>> GetAllOder(CancellationToken cancellationToken);

        Task<OrderDto> GetKeyOderById(int id, CancellationToken cancellationToken);

        Task<ResponseDto<OrderDto>> CreateOder(CreateOrderRequest request, CancellationToken cancellationToken);

        Task<ResponseDto<OrderDto>> UpdateOder(int id, UpdateOrderRequest request, CancellationToken cancellationToken);

        Task<ResponseDto<bool>> DeleteOder(int id, CancellationToken cancellationToken);
    }
}
