namespace EFCoreWebApp.Models.DAL.DapperDAL
{
    public interface ICustomerRepositoryDapper
    {
        Task<IEnumerable<Customer>> GetCustomerAsync(Customer CustomerCriteria);
    }
}
