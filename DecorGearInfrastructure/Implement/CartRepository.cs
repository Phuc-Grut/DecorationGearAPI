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

            var existingCartDetail = cart.CartDetails.FirstOrDefault(item => item.ProductID == request.ProductID && item.CartID == cart.CartID);
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

     

        public Task<List<CartDto>> GetAllCart(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ErrorMessage> DeleteProductFromCart(int userId, int productId, CancellationToken cancellationToken)
        {
            var cart = await _dbcontext.Carts
                .Include(c => c.CartDetails)
                .FirstOrDefaultAsync(c => c.UserID == userId, cancellationToken);

            if (cart == null)
            {
                return ErrorMessage.NotFound; // Giỏ hàng không tồn tại
            }

            var cartDetail = cart.CartDetails.FirstOrDefault(cd => cd.ProductID == productId);
            if (cartDetail == null)
            {
                return ErrorMessage.NotFound; // Sản phẩm không tồn tại trong giỏ hàng
            }

            // Xóa sản phẩm khỏi giỏ hàng
            _dbcontext.CartDetails.Remove(cartDetail);

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


        public async Task<ErrorMessage> ClearCart(int userId, CancellationToken cancellationToken)
        {
            var cart = await _dbcontext.Carts
                .Include(c => c.CartDetails)
                .FirstOrDefaultAsync(c => c.UserID == userId, cancellationToken);

            if (cart == null)
            {
                return ErrorMessage.NotFound; // Giỏ hàng không tồn tại
            }

            // Xóa toàn bộ sản phẩm trong giỏ hàng
            _dbcontext.CartDetails.RemoveRange(cart.CartDetails);

            // Xóa giỏ hàng sau khi đã xóa chi tiết
            _dbcontext.Carts.Remove(cart);

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




        public Task<CartDto> GetCartById(int id, CancellationToken cancellationToken)
        {
            var cart = _dbcontext.Carts
                .Include(c => c.CartDetails)
                .Where(c => c.UserID == id)
                .Select(c => new CartDto
                {
                    UserID = c.UserID,
                    CartDetails = c.CartDetails.Select(cd => new CartDetailDto
                    {
                        ProductName = cd.Product.ProductName,
                        Quantity = cd.Quantity,
                        UnitPrice = cd.UnitPrice,
                        CartDetailID = cd.CartDetailID,
                        ProductID = cd.ProductID,
                        ProductCode = cd.Product.ProductCode,
                        TotalPrice = cd.Quantity * cd.UnitPrice  // Tính tổng giá trị cho từng sản phẩm
                    }).ToList(),
                    // Tính tổng số lượng và tổng tiền cho giỏ hàng
                    TotalQuantity = c.CartDetails.Sum(cd => cd.Quantity),
                    TotalAmount = c.CartDetails.Sum(cd => cd.Quantity * cd.UnitPrice)
                })
                .FirstOrDefault();

            if (cart == null)
            {
                return Task.FromResult(new CartDto());
            }

            return Task.FromResult(cart);
        }

        public Task<bool> DeleteCart(DeleteCartRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
