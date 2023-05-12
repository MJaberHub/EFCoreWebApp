
using System.ComponentModel.DataAnnotations;

namespace EFCoreWebApp.Models
{
    public class CustomerDto
    {
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;
    }
}
