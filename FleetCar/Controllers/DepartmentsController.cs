using FleetCar.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FleetCar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentsController : Controller
    {
        private readonly IDepartment _service;

        public DepartmentsController(IDepartment service)
        {
            _service = service;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            var departments = _service.Get();

            return Ok(departments);
        }
    }
}
