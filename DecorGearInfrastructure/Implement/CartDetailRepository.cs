


using DecorGearDomain.Data.Entities;
using DecorGearInfrastructure.Database.AppDbContext;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DecorGearInfrastructure.Implement
{
    public class CartDetailRepository
    {
        private readonly AppDbContext _context;

        public CartDetailRepository(AppDbContext context)
        {
            _context = context;
        }

        // Lấy chi tiết sản phẩm trong giỏ hàng
        public async Task<CartDetail> GetCartDetailByCartIdAndProductId(int cartId, int productId)
        {
            return await _context.CartDetails
                .Where(cd => cd.CartID == cartId && cd.ProductID == productId && !cd.Deleted)
                .FirstOrDefaultAsync();
        }

        // Tạo mới chi tiết giỏ hàng
        public async Task CreateAsync(CartDetail cartDetail)
        {
            await _context.CartDetails.AddAsync(cartDetail);
            await _context.SaveChangesAsync();
        }

        // Cập nhật chi tiết giỏ hàng
        public async Task UpdateAsync(CartDetail cartDetail)
        {
            _context.CartDetails.Update(cartDetail);
            await _context.SaveChangesAsync();
        }

        // Lấy tổng số lượng sản phẩm và tổng tiền trong giỏ hàng
        public async Task<(int TotalQuantity, double TotalAmount)> GetTotalQuantityAndAmount(int cartId)
        {
            var cartDetails = await _context.CartDetails
                .Where(cd => cd.CartID == cartId && !cd.Deleted)
                .Join(_context.Carts,
                    cd => cd.CartID,
                    c => c.CartID,
                    (cd, c) => new
                    {
                        cd.Quantity,
                        cd.UnitPrice
                    })
                .ToListAsync();

            // Tính tổng số lượng và tổng tiền
            var totalQuantity = cartDetails.Sum(cd => cd.Quantity);
            var totalAmount = cartDetails.Sum(cd => cd.Quantity * cd.UnitPrice);

            return (totalQuantity, totalAmount);
        }
    }
}

