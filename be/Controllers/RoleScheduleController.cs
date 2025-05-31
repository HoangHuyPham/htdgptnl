using System.Security.Claims;
using be.Helpers;
using be.Models;
using be.Repos.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace be.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleScheduleController(IRoleScheduleRepository _RoleScheduleRepo) : ControllerBase
    {
        private readonly IRoleScheduleRepository RoleScheduleRepo = _RoleScheduleRepo;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? query)
        {
            var RoleSchedules = await RoleScheduleRepo.FindAll();

            return Ok(new ApiPaginationResponse<List<RoleSchedule>>
            {
                Message = "get success",
                Data = RoleSchedules,
            });
        }

        [HttpGet("self")]
        public async Task<IActionResult> GetBySelf()
        {
            var roleId = HttpContext.User.FindFirstValue("roleId");

            if (roleId == null)
            {
                return Ok(new ApiPaginationResponse<RoleSchedule>
                {
                    Message = "get failed (jwt claims)",
                    Data = null,
                });
            }

            var RoleSchedules = await RoleScheduleRepo.FindAllByRoleId(Guid.Parse(roleId));

            return Ok(new ApiPaginationResponse<List<RoleSchedule>>
            {
                Message = "get success",
                Data = RoleSchedules,
            });
        }
    }
}