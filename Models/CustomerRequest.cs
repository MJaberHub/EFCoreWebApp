
using System.ComponentModel.DataAnnotations;

namespace EFCoreWebApp.Models
{
    public class CustomerRequest : HttpRequestMessage
    {
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;
    }


    public class CustomerResponse : HttpResponseMessage
    {
        public int CustId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
