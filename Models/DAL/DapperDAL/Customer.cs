namespace EFCoreWebApp.Models.DAL.DapperDAL
{
    public class Customer
    {
        public int CustId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }
    }
}
