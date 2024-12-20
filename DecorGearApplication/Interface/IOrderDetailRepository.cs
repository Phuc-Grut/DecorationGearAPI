﻿using DecorGearApplication.DataTransferObj.OrderDetail;
using DecorGearDomain.Enum;

namespace DecorGearApplication.Interface
{
    public interface IOrderDetailRepository
    {
        Task<IEnumerable<OrderDetailDTO>> GetAllOderDetail(CancellationToken cancellationToken);
        Task<OrderDetailDTO> GetByIdOderDetail(OrderDetailDTO request, CancellationToken cancellationToken);
        Task<ErrorMessage> CreateOderDetail(OrderDetailDTO request, CancellationToken cancellationToken);
        Task<ErrorMessage> UpdateOderDetail(OrderDetailDTO request, CancellationToken cancellationToken);
        Task<bool> DeleteOderDetail(OrderDetailDTO request, CancellationToken cancellationToken);
    }
}
