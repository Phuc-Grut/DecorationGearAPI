using DecorGearApplication.DataTransferObj;
using DecorGearApplication.DataTransferObj.Cart;
using DecorGearApplication.DataTransferObj.CartDetail;
using DecorGearApplication.Interface;
using DecorGearDomain.Data.Entities;
using DecorGearDomain.Enum;
using DecorGearInfrastructure.Database.AppDbContext;
using Microsoft.EntityFrameworkCore;


namespace DecorGearInfrastructure.Implement
{
    public class CartRepository : ICartRespository
    {
        private readonly AppDbContext _dbcontext;

        public CartRepository(AppDbContext appDbContext)
        {
            _dbcontext = appDbContext;
        }
        public async Task<ErrorMessage> AddProductToCart(CreateCartDetailRequest request, CancellationToken cancellationToken)
        {
            // Kiểm tra dữ liệu đầu vào
            if (request.Quantity <= 0 || request.UnitPrice <= 0)
            {
                return ErrorMessage.Null; // Trả về lỗi dữ liệu không hợp lệ
            }

            var cart = await _dbcontext.Carts
                .Include(c => c.CartDetails)
                .FirstOrDefaultAsync(c => c.UserID == request.UserID, cancellationToken);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserID = request.UserID,
                    CartDetails = new List<CartDetail>()
                };
                _dbcontext.Carts.Add(cart);
            }

            var existingCartDetail = cart.CartDetails.FirstOrDefault(item => item.ProductID == request.ProductID);
            if (existingCartDetail != null)
            {
                existingCartDetail.Quantity += request.Quantity;
            }
            else
            {
                var newCartDetail = new CartDetail
                {
                    ProductID = request.ProductID,
                    Quantity = request.Quantity,
                    UnitPrice = (double)request.UnitPrice,
                };
                cart.CartDetails.Add(newCartDetail);
            }

            try
            {
                await _dbcontext.SaveChangesAsync(cancellationToken);
                return ErrorMessage.Successfull;
            }
            catch (Exception)
            {
                return ErrorMessage.DatabaseError; // Lỗi khi lưu vào cơ sở dữ liệu
            }
        }

        public Task<bool> DeleteCart(DeleteCartRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<CartDto>> GetAllCart(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Lấy giỏ hàng theo UserID
        /// </summary>


        public Task<CartDto> GetCartById(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
