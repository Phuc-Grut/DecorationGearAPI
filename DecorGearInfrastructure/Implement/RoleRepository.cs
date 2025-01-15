using AutoMapper;
using DecorGearApplication.DataTransferObj.Role;
using DecorGearApplication.Interface;
using DecorGearApplication.ValueObj.Pagination;
using DecorGearDomain.Data.Entities;
using DecorGearInfrastructure.Database.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DecorGearInfrastructure.Implement
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _map;
        private readonly ILogger<RoleRepository> _logger;

        public RoleRepository(AppDbContext db, IMapper map, ILogger<RoleRepository> logger)
        {
            _db = db;
            _map = map;
            _logger = logger;
        }

        public async Task<bool> CreateAsync(Role request, CancellationToken cancellationToken)
        {
            try
            {
                request.CreatedTime = DateTimeOffset.Now;
                await _db.Roles.AddAsync(request, cancellationToken);
                await _db.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating role");
                return false;
            }
        }

        public async Task<bool> CreateDefaultRolesAsync(CancellationToken cancellationToken)
        {
            try
            {
                var userRole = await _db.Roles
                    .Where(r => r.RoleName == "User")
                    .FirstOrDefaultAsync(cancellationToken);

                if (userRole == null)
                {
                    userRole = new Role
                    {
                        RoleName = "User",
                        CreatedTime = DateTimeOffset.UtcNow
                    };

                    await _db.Roles.AddAsync(userRole, cancellationToken);
                    await _db.SaveChangesAsync(cancellationToken);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating default roles");
                return false;
            }
        }

        private async Task<Role?> GetRoleById(int id, CancellationToken cancellationToken)
        {
            return await _db.Roles.FirstOrDefaultAsync(x => x.RoleID == id, cancellationToken);
        }

        public async Task<bool> DeleteAsync(int roleId, CancellationToken cancellationToken)
        {
            var role = await GetRoleById(roleId, cancellationToken);

            if (role == null)
            {
                return false;
            }

            role.DeletedTime = DateTimeOffset.Now;
            role.Deleted = true;

            _db.Roles.Update(role);
            await _db.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> UpdateAsync(UpdateRoleRequest request, CancellationToken cancellationToken)
        {
            // Tìm người dùng theo UserId
            var user = await _db.Users
                .Where(u => u.UserID == request.UserId)
                .FirstOrDefaultAsync(cancellationToken);

            // Kiểm tra xem người dùng có tồn tại không
            if (user == null)
            {
                return false;
            }

            // Tìm vai trò theo RoleID
            var role = await _db.Roles
                .Where(r => r.RoleID == request.RoleID)
                .FirstOrDefaultAsync(cancellationToken);

            // Kiểm tra xem vai trò có tồn tại không
            if (role == null)
            {
                return false;
            }

            // Cập nhật vai trò cho người dùng
            user.RoleID = request.RoleID;
            user.Role = role; // Cập nhật Role nếu cần thiết (tùy thuộc vào cách bạn xử lý mối quan hệ giữa User và Role)

            _db.Users.Update(user);
            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }



        public async Task<List<RoleDto>> GetAllAsyncs(CancellationToken cancellationToken)
        {
            try
            {
                var roles = await _db.Roles.ToListAsync(cancellationToken);
                var rolesDto = _map.Map<List<RoleDto>>(roles);
                return rolesDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all roles");
                throw;
            }
        }

        public Task<PaginationResponse<RoleDto>> GetAllAsync(ViewRoleRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        
    }
}
