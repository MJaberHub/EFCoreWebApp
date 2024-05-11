using EFCoreWebApp.Models;
using EFCoreWebApp.Models.DAL;
using EFCoreWebApp.Models.DAL.Generic;
using EFCoreWebApp.Services;
using EFCoreWebApp.Validator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace EFCoreWebApp.Controllers
{
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IRepository<TCustomer> _repository; //generic repo
        private readonly ICustomerRepository _customerRepository; //specific repo
        private readonly IBankLookUpService _bankLookUpService;

        public CustomerController(ILogger<CustomerController> logger, IRepository<TCustomer> repository, ICustomerRepository customerRepository, IBankLookUpService bankLookUpService)
        {
            _logger = logger;
            _repository = repository;
            _customerRepository = customerRepository;
            _bankLookUpService = bankLookUpService;
        }

        [HttpPost("api/addNewCustomer")]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerRequest Customer)
        {
            //here we could have a mapper between Dto and the Entity
            try
            {
                #region ValidateRequest
                var validator = new AddCustomerValidator();

                // Execute the validator
                var result = validator.Validate(Customer);

                // Inspect any validation failures.
                var success = result.IsValid;

                if (!success)
                {
                    var failures = result.Errors;
                    return BadRequest(failures);
                }
                #endregion

                var newCustomer = new TCustomer()
                {
                    FirstName = Customer.FirstName,
                    LastName = Customer.LastName
                };

                //changes the entity state (added, modified, deleted)
                _repository.InsertModel(newCustomer);

                //when calling the savechanges the record will be inserted
                _repository.Save();

                return Ok(new CustomerResponse()
                {
                    CustId = newCustomer.CustId,
                    FirstName = newCustomer.FirstName,
                    LastName = newCustomer.LastName,
                    CreatedBy = newCustomer.CreatedBy,
                    DateCreated = newCustomer.DateCreated,
                    DateModified = newCustomer.DateModifed
                });
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("api/getCustomerInfo/{CustId}")]
        public async Task<IActionResult> GetCustomerInfo(int CustId)
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
        public async Task<IActionResult> GetAllCustomers()
        {
            /* try
             {
                 var customers = _repository.GetModel();

                 if (customers?.Any() ?? false)
                 {
                     return Ok(customers.Select(item => new CustomerResponse()
                     {
                         CustId = item.CustId,
                         FirstName = item.FirstName,
                         LastName = item.LastName,
                         CreatedBy = item.CreatedBy,
                         DateCreated = item.DateCreated,
                         DateModified = item.DateModifed
                     }));
                 }

                 return BadRequest();
             }
             catch (Exception ex)
             {
                 _logger.LogDebug(ex.Message);
                 return BadRequest(ex.Message);
             }*/

            var customers = _repository.GetModel();

            throw new Exception();

            if (customers?.Any() ?? false)
            {
                return Ok(customers.Select(item => new CustomerResponse()
                {
                    CustId = item.CustId,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    CreatedBy = item.CreatedBy,
                    DateCreated = item.DateCreated,
                    DateModified = item.DateModifed
                }));
            }

            return BadRequest();
        }

        [HttpDelete("api/deleteCustomer/{CustId}")]
        public async Task<IActionResult> DeleteCustomer(int CustId)
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

        [HttpGet("api/getBankList")]
        public async Task<IActionResult> GetBankList()
        {
            try
            {
                var response = await _bankLookUpService.GetBankList();

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
