using EFCoreWebApp.Models.DAL.Generic;

namespace EFCoreWebApp.Models.DAL
{
    public interface ICustomerRepository : IRepository<TCustomer>
    {
        IEnumerable<TCustomer> GetCustomerByFirstName(string firstName);
        IEnumerable<TCustomer> GetCustomerByLastName(string lastName);
        TCustomer GetCustomerByFirstAndLastName(string firstName, string lastName);
    }
}
