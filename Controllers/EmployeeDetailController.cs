using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EMS_Web_API.Data;
using EMS_Web_API.Models;
using EMS_Web_API.ModelDTO;

namespace EMS_Web_API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class EmployeeDetailController : ControllerBase
    {
        private readonly EmployeeManagementSystemContext _context;

        public EmployeeDetailController(EmployeeManagementSystemContext context)
        {
            _context = context;
        }

        // GET: EmployeeDetail
        [HttpGet]
        public async Task<IActionResult> GetEmployeeDetails()
        {
            var employee = await _context.employeeDetails.ToListAsync();
            return Ok(employee);
            //return _context.employeeDetails != null ? 
            //            View(await _context.employeeDetails.ToListAsync()) :
            //            Problem("Entity set 'EmployeeDetailsContext.employeeDetails'  is null.");
        }

        // GET: EmployeeDetail
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetEmployeeDetailsById([FromRoute] Guid id)
        {
            var employee = await _context.employeeDetails.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {
                return Ok(employee);
            }
            return NotFound("Employee Not Found");
        }

        [HttpPut]
        [Route("addEmployeeDetails/{id:guid}")]
        public async Task<IActionResult> AddEmployeeDetails([FromRoute] Guid id, [FromBody] EmployeeDetails employee)
        {
            var existingDetails = await _context.employeeDetails.FirstOrDefaultAsync(_ => _.Id == id);
            if (existingDetails != null)
            {
                existingDetails.Name = employee.Name;
                existingDetails.Birthdate = employee.Birthdate;
                existingDetails.Contact = employee.Contact;
                existingDetails.Gender = employee.Gender;
                existingDetails.Education = employee.Education;
                existingDetails.Department = employee.Department;
                existingDetails.JobExperience = employee.JobExperience;
                existingDetails.Salary = employee.Salary;
                existingDetails.Profile = employee.Profile;
                existingDetails.Joiningdate = employee.Joiningdate;

                await _context.SaveChangesAsync();
                return Ok("Updated Sucessfully");
            }
            return NotFound("Employee Not Found");
            //return _context.employeeDetails != null ? 
            //            View(await _context.employeeDetails.ToListAsync()) :
            //            Problem("Entity set 'EmployeeDetailsContext.employeeDetails'  is null.");
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] EmployeeDetails employee)
        {
            if (_context.employeeDetails == null)
            {
                return Problem("Employee Entity is Empty.");
            }
            _context.employeeDetails.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeDetails", new { id = employee.Id }, employee);
            //return Ok("You are registered successfully");
            //return new JsonResult(new { message = "You are registered successfully" });
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<EmployeeDetails>> Login([FromBody] LoginDTO employee)
        {
            if (_context.employeeDetails == null)
            {
                return NotFound("Invalid credentials not found");
            }
            var E_Details = await _context.employeeDetails
                .Where(x => x.Email == employee.Email && x.Pass == employee.Pass)
                .Select(x => new EmployeeDetails
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Pass = x.Pass,

                })
                .FirstOrDefaultAsync();

            if (E_Details == null)
            {
                return NotFound("Invalid credentials not found");
            }
            return Ok(E_Details);
        }
    }
}
