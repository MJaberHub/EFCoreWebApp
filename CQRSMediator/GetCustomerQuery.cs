using EFCoreWebApp.Models;
using MediatR;

namespace EFCoreWebApp.CQRSMediator
{
    public class GetCustomerQuery : IRequest<TCustomer>
    {
        public int CustomerId { get; set; }
    }
}
