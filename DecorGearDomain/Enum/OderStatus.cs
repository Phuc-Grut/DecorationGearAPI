    namespace DecorGearDomain.Enum
{
    public enum OrderStatus
    {

        Pending = 1,         // Đơn hàng đang chờ xử lý
        Confirmed = 2,       // Đơn hàng đã được xác nhận
        Shipped = 3,         // Đơn hàng đã được giao cho đơn vị vận chuyển
        Delivered = 4,       // Đơn hàng đã được giao
        Cancelled = 5,       // Đơn hàng đã bị hủy
        Returned = 6,        // Đơn hàng đã được trả lại
        Refunded = 7         // Đơn hàng đã được hoàn tiền
    }
}
