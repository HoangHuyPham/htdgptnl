using Microsoft.AspNetCore.Mvc;

namespace be.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CriteriaController : ControllerBase
    {
        public async Task<IActionResult> GetAll([FromQuery] string query){
            return Ok();
        }
    }
}