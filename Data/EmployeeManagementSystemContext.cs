using EMS_Web_API.Models;
using Microsoft.EntityFrameworkCore;

namespace EMS_Web_API.Data
{
    public class EmployeeManagementSystemContext : DbContext
    {
        public EmployeeManagementSystemContext(DbContextOptions<EmployeeManagementSystemContext> options) : base(options)
        {

        }

        public DbSet<EmployeeDetails> employeeDetails { get; set; }

        public DbSet<AdminDetails> adminDetails { get; set; }
    }
}
