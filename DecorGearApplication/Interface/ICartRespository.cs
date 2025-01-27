﻿using DecorGearApplication.DataTransferObj;
using DecorGearApplication.DataTransferObj.Cart;
using DecorGearApplication.DataTransferObj.CartDetail;
using DecorGearDomain.Enum;

namespace DecorGearApplication.Interface
{
    public interface ICartRespository
    {
        Task<List<CartDto>> GetAllCart(CancellationToken cancellationToken);
        Task<CartDto> GetCartById(int id, CancellationToken cancellationToken);
        Task<ErrorMessage> AddProductToCart(CreateCartDetailRequest request, CancellationToken cancellationToken);
        Task<bool> DeleteCart(DeleteCartRequest request, CancellationToken cancellationToken);
        Task<ErrorMessage> DeleteProductFromCart(int userId, int productId, CancellationToken cancellationToken);
        Task<ErrorMessage> ClearCart(int userId, CancellationToken cancellationToken);

    }
}
