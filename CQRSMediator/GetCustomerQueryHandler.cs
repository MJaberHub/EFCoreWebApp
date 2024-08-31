using EFCoreWebApp.Models;
using EFCoreWebApp.Models.DAL.Generic;
using MediatR;

namespace EFCoreWebApp.CQRSMediator
{
    public class GetCustomerQueryHandler(IRepository<TCustomer> customerRepository) : IRequestHandler<GetCustomerQuery, TCustomer>
    {
        public async Task<TCustomer> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            return customerRepository.GetModelById(request.CustomerId);
        }
    }
}
