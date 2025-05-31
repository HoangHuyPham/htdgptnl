// using System.Security.Claims;
// using be.Helpers;
// using be.Models;
// using be.Repos.Interfaces;
// using Microsoft.AspNetCore.JsonPatch;
// using Microsoft.AspNetCore.Mvc;

// namespace be.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class RoleEvaluationScheduleController(IRoleEvaluationScheduleRepository _roleEvaluationScheduleRepo) : ControllerBase
//     {
//         private readonly IRoleEvaluationScheduleRepository roleEvaluationScheduleRepo = _roleEvaluationScheduleRepo;
//         [HttpGet]
//         public  async Task<IActionResult> GetAllByRoleId()
//         {
//             var roleId = HttpContext.User.FindFirstValue("roleId");
            
//             List<RoleEvaluationSchedule> schedules = await roleEvaluationScheduleRepo.FindAllByRoleId(Guid.Parse(roleId!));

//             return Ok(new ApiPaginationResponse<List<RoleEvaluationSchedule>>
//             {
//                 Message = "get success",
//                 Data = schedules,
//             });
//         }

//     }
// }