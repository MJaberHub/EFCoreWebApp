using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EFCoreWebApp.Models.DAL.DapperDAL
{
    public class CustomerRepositoryDapper : ICustomerRepositoryDapper
    {
        private readonly IConfiguration _configuration;
        public CustomerRepositoryDapper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Customer>> GetCustomerAsync(Customer CustomerCriteria)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MainDBDapper")))
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@CUST_ID", CustomerCriteria.CustId);

                await connection.OpenAsync();
                var customers = await connection.QueryAsync<Customer>(
                    "S_GET_CUSTOMER_W_S_F",
                    queryParameters,
                    commandType: CommandType.StoredProcedure);

                return customers;
            }
        }
    }
}
