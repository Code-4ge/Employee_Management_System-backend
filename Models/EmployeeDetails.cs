using System.ComponentModel.DataAnnotations;

namespace EMS_Web_API.Models
{
    public class EmployeeDetails
    {
        [Key]
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Pass { get; set; }

        public string? Birthdate { get; set; }
        public string? Contact { get; set; }
        public string? Gender { get; set; }
        public string? Education { get; set; }
        public string? Department { get; set; }
        public int? JobExperience { get; set; }
        public int? Salary { get; set; }
        public string? Profile { get; set; }
        public string? Joiningdate { get; set; }
    }
}
