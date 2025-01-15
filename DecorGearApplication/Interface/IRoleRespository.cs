using DecorGearApplication.DataTransferObj.Role;
using DecorGearApplication.ValueObj.Pagination;
using DecorGearDomain.Data.Entities;

namespace DecorGearApplication.Interface
{
    public interface IRoleRepository
    {
        /// <summary>
        /// Lấy danh sách các vai trò với phân trang.
        /// </summary>
        /// <param name="request">Yêu cầu phân trang, bao gồm các tham số như số trang và số lượng trên mỗi trang.</param>
        /// <param name="cancellationToken">Token huỷ tác vụ.</param>
        /// <returns>Danh sách các vai trò dưới dạng phân trang.</returns>
        Task<List<RoleDto>> GetAllAsyncs(CancellationToken cancellationToken);

        /// <summary>
        /// Tạo một vai trò mới.
        /// </summary>
        /// <param name="request">Thông tin vai trò cần tạo.</param>
        /// <param name="cancellationToken">Token huỷ tác vụ.</param>
        /// <returns>Trả về true nếu thành công, false nếu thất bại.</returns>
        Task<bool> CreateAsync(Role request, CancellationToken cancellationToken);

        /// <summary>
        /// Cập nhật thông tin vai trò.
        /// </summary>
        /// <param name="request">Thông tin vai trò cần cập nhật.</param>
        /// <param name="cancellationToken">Token huỷ tác vụ.</param>
        /// <returns>Trả về true nếu thành công, false nếu thất bại.</returns>
        Task<bool> UpdateAsync(UpdateRoleRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Xóa vai trò theo ID.
        /// </summary>
        /// <param name="roleId">ID của vai trò cần xóa.</param>
        /// <param name="cancellationToken">Token huỷ tác vụ.</param>
        /// <returns>Trả về true nếu thành công, false nếu thất bại.</returns>
        Task<bool> DeleteAsync(int roleId, CancellationToken cancellationToken);

        /// <summary>
        /// Tạo vai trò mặc định, nếu chưa tồn tại.
        /// </summary>
        /// <param name="cancellationToken">Token huỷ tác vụ.</param>
        /// <returns>Trả về true nếu thành công, false nếu thất bại.</returns>
        Task<bool> CreateDefaultRolesAsync(CancellationToken cancellationToken);
    }
}
