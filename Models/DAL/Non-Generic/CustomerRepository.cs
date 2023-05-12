using Microsoft.EntityFrameworkCore;

namespace EFCoreWebApp.Models.DAL
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MainDbContext _dataContext;
        public CustomerRepository(MainDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void DeleteCustomer(int customerId)
        {
            var customer = _dataContext.TCustomers.Find(customerId);

            if (customer?.CustId != 0)
            {
                _dataContext.TCustomers.Remove(customer);
            }
        }

        public TCustomer GetCustomerById(int customerId)
        {
            return _dataContext.TCustomers.Find(customerId);
        }

        public IEnumerable<TCustomer> GetCustomers()
        {
            return _dataContext.TCustomers.ToList();
        }

        public void InsertCustomer(TCustomer customer)
        {
            _dataContext.TCustomers.Add(customer);
        }

        public void SaveChanges()
        {
            _dataContext.SaveChanges();
        }

        public void UpdateCustomer(TCustomer customer)
        {
            _dataContext.Entry(customer).State = EntityState.Modified;
        }
    }
}
