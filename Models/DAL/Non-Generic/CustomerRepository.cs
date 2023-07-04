using EFCoreWebApp.Models.DAL.Generic;
using Microsoft.EntityFrameworkCore;

namespace EFCoreWebApp.Models.DAL
{
    public class CustomerRepository : Repository<TCustomer>, ICustomerRepository
    {
        private readonly MainDbContext _dataContext;
        public CustomerRepository(MainDbContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public TCustomer GetCustomerByFirstAndLastName(string firstName, string lastName)
        {
            return _dataContext.TCustomers.FirstOrDefault(item => item.FirstName.ToUpper().Equals(firstName.ToUpper()) && item.LastName.ToUpper().Equals(lastName.ToUpper()));
        }

        public IEnumerable<TCustomer> GetCustomerByFirstName(string firstName)
        {
            return _dataContext.TCustomers.Where(item => item.FirstName.ToUpper().Equals(firstName.ToUpper())).ToList();
        }

        public IEnumerable<TCustomer> GetCustomerByLastName(string lastName)
        {
            return _dataContext.TCustomers.Where(item => item.LastName.ToUpper().Equals(lastName.ToUpper())).ToList();
        }
    }
}
