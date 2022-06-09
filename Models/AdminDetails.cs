using System.ComponentModel.DataAnnotations;

namespace EMS_Web_API.Models
{
    public class AdminDetails
    {
        [Key]
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Pass { get; set; }
    }
}
