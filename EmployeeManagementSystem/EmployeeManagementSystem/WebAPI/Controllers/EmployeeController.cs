// Controllers/EmployeeController.cs
using EmployeeManagementSystem.Models;
using WebAPI.Models;
using EmployeeManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        /*  [Authorize]
           [HttpGet]
           [Route("GetData")]
           public string GetData()
           {
               return "Authenticated with JWT ";
           }

           [HttpGet]
           [Route("Details")]
           public string Details()
           {
               return "Authenticated with JWT ";
           }

           [Authorize]
           [HttpPost]
           public string AddUser(Users user)
           {
               return "User added with Username" + user.Username;
           }
        */
        [HttpGet]
        [Route("EmployeeList")]
        public IActionResult GetAll()
        {
            var employees = _service.GetAll();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var employee = _service.GetById(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Employee employee)
        {
            _service.Add(employee);
            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Employee employee)
        {
            if (id != employee.Id) return BadRequest();
            _service.Update(employee);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return NoContent();
        }
        [HttpPost("deleteMultiple")]
        public IActionResult DeleteMultiple([FromBody] List<int> ids)
        {
            foreach (var id in ids)
            {
                _service.Delete(id); // Call your existing delete service for each ID
            }
            return NoContent(); // Return 204 No Content status
        }

    }
}
