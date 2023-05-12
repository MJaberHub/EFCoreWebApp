namespace EFCoreWebApp.Models.DAL
{
    public interface ICustomerRepository
    {
        IEnumerable<TCustomer> GetCustomers();
        TCustomer GetCustomerById(int customerId);
        void InsertCustomer(TCustomer customer);
        void UpdateCustomer(TCustomer customer);
        void DeleteCustomer(int customerId);
        void SaveChanges();
    }
}
