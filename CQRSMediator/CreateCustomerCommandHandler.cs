using EFCoreWebApp.Models;
using EFCoreWebApp.Models.DAL.Generic;
using MediatR;

namespace EFCoreWebApp.CQRSMediator
{
    public class CreateCustomerCommandHandler(IRepository<TCustomer> customerRepository) : IRequestHandler<CreateCustomerCommand, TCustomer>
    {
        public async Task<TCustomer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new TCustomer()
            {
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            customerRepository.InsertModel(customer);
            customerRepository.Save();

            return customer;
        }
    }
}
