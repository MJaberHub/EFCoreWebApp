using EFCoreWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreWebApp.Controllers
{
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;

        private readonly MainDbContext _context;

        public CustomerController(ILogger<CustomerController> logger, MainDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost("api/addNewCustomer")]
        public HttpResponseMessage AddCustomer([FromBody] CustomerDto Customer)
        {

            //here we could have a mapper between Dto and the Entity
            //TODO: MJaber: Add DAL 

            _context.Add<TCustomer>(new TCustomer()
            {
                FirstName = Customer.FirstName,
                LastName = Customer.LastName
            });

            _context.SaveChanges();

            return new HttpResponseMessage();
        }

        [HttpGet("api/getCustomerInfo/{CustId}")]
        public ActionResult<CustomerDto> GetCustomerInfo(int CustId)
        {
            var customer = _context.TCustomers.Find(CustId);

            if (customer?.CustId != 0)
            {
                return Ok(customer);
            }

            return BadRequest();
        }
    }
}
