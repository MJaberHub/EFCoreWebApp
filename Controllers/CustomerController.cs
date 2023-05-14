using EFCoreWebApp.Models;
using EFCoreWebApp.Models.DAL.Generic;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreWebApp.Controllers
{
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IRepository<TCustomer> _repository;

        public CustomerController(ILogger<CustomerController> logger, IRepository<TCustomer> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPost("api/addNewCustomer")]
        public ActionResult AddCustomer([FromBody] CustomerDto Customer)
        {
            //here we could have a mapper between Dto and the Entity
            try
            {
                //changes the entity state (added, modified, deleted)
                _repository.InsertModel(new TCustomer()
                {
                    FirstName = Customer.FirstName,
                    LastName = Customer.LastName
                });

                //when calling the savechanges the recod will be inserted
                _repository.Save();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("api/addNewCustomerAsync")]
        public ActionResult AddCustomerAsync([FromBody] CustomerDto Customer)
        {
            //here we could have a mapper between Dto and the Entity
            try
            {
                var jobId = BackgroundJob.Enqueue(() => _repository.InsertModel(new TCustomer()
                {
                    FirstName = Customer.FirstName,
                    LastName = Customer.LastName
                }));

                BackgroundJob.ContinueJobWith(jobId, () => _repository.Save());

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("api/getCustomerInfo/{CustId}")]
        public ActionResult<CustomerDto> GetCustomerInfo(int CustId)
        {
            try
            {
                var customer = _repository.GetModelById(CustId);

                if (customer?.CustId != 0)
                {
                    return Ok(customer);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("api/getCustomers")]
        public ActionResult<List<CustomerDto>> GetAllCustomers()
        {
            try
            {
                var customers = _repository.GetModel();

                if (customers?.Any() ?? false)
                {
                    return Ok(customers.Select(item => new CustomerDto()
                    {
                        FirstName = item.FirstName,
                        LastName = item.LastName
                    }));
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("api/deleteCustomer/{CustId}")]
        public ActionResult DeleteCustomer(int CustId)
        {
            try
            {
                _repository.DeleteModel(CustId);

                _repository.Save();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
