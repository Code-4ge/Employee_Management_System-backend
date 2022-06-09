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
    public class AdminDetailController : ControllerBase
    {
        private readonly EmployeeManagementSystemContext _context;

        public AdminDetailController(EmployeeManagementSystemContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAdminDetails()
        {
            var admin = await _context.adminDetails.ToListAsync();
            return Ok(admin);

        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminDetails admin)
        {
            if (_context.adminDetails == null)
            {
                return Problem("Admin Entity is Empty.");
            }
            _context.adminDetails.Add(admin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdminDetails", new { id = admin.Id }, admin);
        }


        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetAdminDetailsById([FromRoute] Guid id)
        {
            var admin = await _context.adminDetails.FirstOrDefaultAsync(x => x.Id == id);

            if (admin != null)
            {
                var employeeDetails = await _context.employeeDetails.ToListAsync();
                return Ok(employeeDetails);
            }
            return NotFound("Admin Not Found");
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AdminDetails>> Login([FromBody] LoginDTO admin)
        {
            if (_context.adminDetails == null)
            {
                return NotFound("Invalid credentials not found");
            }
            var A_Details = await _context.adminDetails
                .Where(x => x.Email == admin.Email && x.Pass == admin.Pass)
                .Select(x => new AdminDetails
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Pass = x.Pass,

                })
                .FirstOrDefaultAsync();

            if (A_Details == null)
            {
                return NotFound("Invalid credentials not found");
            }
            return Ok(A_Details);
        }


        [HttpDelete]
        [Route("deleteEmployee/{id:guid}/{employeeId:guid}")]
        // GET: AdminDetail
        public async Task<IActionResult> DeleteEmployeeById([FromRoute] Guid id, [FromRoute] Guid employeeId)
        {

            if (id == null || _context.adminDetails == null)
            {
                return NotFound("Admin Token Not Found");
            }

            var adminDetails = await _context.adminDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adminDetails != null)
            {
                var deleteEmployee = await _context.employeeDetails.FirstOrDefaultAsync(m => m.Id == employeeId);
                if (deleteEmployee != null)
                {
                    _context.Remove(deleteEmployee);
                    await _context.SaveChangesAsync();
                    return Ok("Deleted Sucessfully");
                }
                else
                {
                    return NotFound("Employee Not Found");
                }
            }
            return NotFound("Admin User Not Found");
        }


        // GET: AdminDetail/Delete/5
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (id == null || _context.adminDetails == null)
            {
                return NotFound();
            }

            var adminDetails = await _context.adminDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adminDetails == null)
            {
                return NotFound();
            }
            _context.Remove(adminDetails);
            await _context.SaveChangesAsync();
            return Ok(adminDetails);
        }

    }
}
