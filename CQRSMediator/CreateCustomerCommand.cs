using EFCoreWebApp.Models;
using MediatR;

namespace EFCoreWebApp.CQRSMediator
{
    public class CreateCustomerCommand : IRequest<TCustomer>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
