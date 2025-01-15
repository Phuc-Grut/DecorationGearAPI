using DecorGearApplication.DataTransferObj.Role;
using DecorGearApplication.Interface;
using DecorGearDomain.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DecorGearApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<RoleController> _logger;

        public RoleController(IRoleRepository roleRepository, ILogger<RoleController> logger)
        {
            _roleRepository = roleRepository;
            _logger = logger;
        }

        // POST: api/Role/Create
        [HttpPost("Create")]
        public async Task<IActionResult> CreateRole([FromBody] Role request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return BadRequest("Invalid role data.");
            }

            var result = await _roleRepository.CreateAsync(request, cancellationToken);

            if (result)
            {
                return Ok("Role created successfully.");
            }

            _logger.LogError("Error creating role.");
            return StatusCode(500, "Internal server error while creating role.");
        }

        // POST: api/Role/CreateDefaultRoles
        [HttpPost("CreateDefaultRoles")]
        public async Task<IActionResult> CreateDefaultRoles(CancellationToken cancellationToken)
        {
            var result = await _roleRepository.CreateDefaultRolesAsync(cancellationToken);

            if (result)
            {
                return Ok("Default roles created successfully.");
            }

            _logger.LogError("Error creating default roles.");
            return StatusCode(500, "Internal server error while creating default roles.");
        }

        // PUT: api/Role/Update
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return BadRequest("Invalid role data.");
            }

            var result = await _roleRepository.UpdateAsync(request, cancellationToken);

            if (result)
            {
                return Ok("Role updated successfully.");
            }

            _logger.LogError("Error updating role.");
            return StatusCode(500, "Internal server error while updating role.");
        }

        // DELETE: api/Role/Delete/{roleId}
        [HttpDelete("Delete/{roleId}")]
        public async Task<IActionResult> DeleteRole(int roleId, CancellationToken cancellationToken)
        {
            var result = await _roleRepository.DeleteAsync(roleId, cancellationToken);

            if (result)
            {
                return Ok("Role deleted successfully.");
            }

            _logger.LogError($"Error deleting role with ID {roleId}.");
            return NotFound($"Role with ID {roleId} not found.");
        }

        // GET: api/Role/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
        {
            try
            {
                var roles = await _roleRepository.GetAllAsyncs(cancellationToken);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all roles.");
                return StatusCode(500, "Internal server error while fetching roles.");
            }
        }
    }
}
