using be.Helpers;
using be.Mappers;
using be.Models;
using be.Repos.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace be.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController(IRepository<Employee> _EmployeeRepo) : ControllerBase
    {
        private readonly IRepository<Employee> EmployeeRepo = _EmployeeRepo;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var Employee = await EmployeeRepo.FindById(id);

            return Ok(new ApiResponse<Employee>
            {
                Message = "get success",
                Data = Employee,
            });
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? query)
        {
            var Employees = await EmployeeRepo.FindAll();

            return Ok(new ApiPaginationResponse<List<Employee>>
            {
                Message = "get success",
                Data = Employees,
            });
        }

        // [HttpPost]
        // public async Task<IActionResult> Create([FromBody] CreateEmployeeDTO dto)
        // {
        //     try
        //     {
        //         var result = await EmployeeRepo.Create(new Employee
        //         {
        //             Name = dto.Name,
        //         });

        //         if (result == null)
        //         {
        //             return Ok(new ApiResponse<Employee>
        //             {
        //                 Message = "create failed",
        //                 Data = null,
        //             });
        //         }

        //         return Ok(new ApiResponse<Employee>
        //         {
        //             Message = "create success",
        //             Data = result,
        //         });
        //     }
        //     catch
        //     {
        //         return BadRequest(new ApiResponse<Employee>
        //         {
        //             Message = "server error",
        //             Data = null,
        //         });
        //     }
        // }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> Put(Guid id, PutEmployeeDTO dto)
        // {
        //     try
        //     {

        //         var Employee = await EmployeeRepo.FindById(id);

        //         if (Employee == null)
        //         {
        //             return NotFound(new ApiResponse<Employee>
        //             {
        //                 Message = "entity not found",
        //                 Data = null
        //             });
        //         }
                
        //         Employee.Name = dto.Name;

        //         if (!ModelState.IsValid)
        //         {
        //             return Ok(new ApiResponse<Employee>
        //             {
        //                 Data = null,
        //                 Message = "invalid params"
        //             });
        //         }

        //         return Ok(new ApiResponse<Employee>
        //         {
        //             Message = "update success",
        //             Data = await EmployeeRepo.Update(Employee),
        //         });
        //     }
        //     catch
        //     {
        //         return BadRequest(new ApiResponse<Employee>
        //         {
        //             Message = "server error",
        //             Data = null,
        //         });
        //     }
        // }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<Employee> jsonPatch)
        {
            try
            {
                var Employee = await EmployeeRepo.FindById(id);
                if (Employee == null || jsonPatch == null)
                {
                    return NotFound(new ApiResponse<Employee>
                    {
                        Message = "entity not found",
                        Data = null
                    });
                }

                jsonPatch.ApplyTo(Employee, ModelState);

                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse<Employee>
                    {
                        Data = null,
                        Message = "invalid params"
                    });
                }

                return Ok(new ApiResponse<Employee>
                {
                    Message = "update success",
                    Data = await EmployeeRepo.Update(Employee),
                });
            }
            catch
            {
                return BadRequest(new ApiResponse<Employee>
                {
                    Message = "server error",
                    Data = null,
                });
            }
        }

        // [HttpDelete("{id}")]
        // public async Task<IActionResult> Delete(Guid id)
        // {
        //     try
        //     {
        //         var result = await EmployeeRepo.Delete(id);

        //         if (!result) return Ok(new ApiResponse<CreateEmployeeDTO>
        //         {
        //             Message = "id not found",
        //             Data = null,
        //         });

        //         return Ok(new ApiResponse<CreateEmployeeDTO>
        //         {
        //             Message = "delete success",
        //             Data = null,
        //         });
        //     }
        //     catch
        //     {
        //         return BadRequest(new ApiResponse<Employee>
        //         {
        //             Message = "server error",
        //             Data = null,
        //         });
        //     }
        // }
    }
}