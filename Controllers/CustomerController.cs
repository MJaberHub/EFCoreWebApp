using EFCoreWebApp.CQRSMediator;
using EFCoreWebApp.Models;
using EFCoreWebApp.Models.DAL;
using EFCoreWebApp.Models.DAL.DapperDAL;
using EFCoreWebApp.Models.DAL.Generic;
using EFCoreWebApp.Validator;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreWebApp.Controllers
{
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IRepository<TCustomer> _repository; //generic repo
        private readonly ICustomerRepository _customerRepository; //specific repo
        private readonly ICustomerRepositoryDapper _customerRepositoryDapper;
        private readonly IMediator _mediator;

        public CustomerController(ILogger<CustomerController> logger, IRepository<TCustomer> repository, ICustomerRepository customerRepository, ICustomerRepositoryDapper customerRepositoryDapper, IMediator mediator)
        {
            _logger = logger;
            _repository = repository;
            _customerRepository = customerRepository;
            _customerRepositoryDapper = customerRepositoryDapper;
            _mediator = mediator;
        }

        [HttpPost("api/v1/addNewCustomer")]
        public async Task<IActionResult> AddCustomerV1([FromBody] CustomerRequest Customer)
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

                //when calling the savechanges the recod will be inserted
                _repository.Save();

                return Ok(new CustomerResponse()
                {
                    CustId = newCustomer.CustId,
                    FirstName = newCustomer.FirstName,
                    LastName = newCustomer.LastName,
                    CreatedBy = newCustomer.CreatedBy,
                    DateCreated = newCustomer.DateCreated,
                    DateModified = newCustomer.DateModified
                });
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("api/v2/addNewCustomer")]
        public async Task<IActionResult> AddCustomerV2([FromBody] CustomerRequest Customer)
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

                var newCustomer = await _mediator.Send(new CreateCustomerCommand()
                {
                    FirstName = Customer.FirstName,
                    LastName = Customer.LastName
                });

                return Ok(new CustomerResponse()
                {
                    CustId = newCustomer.CustId,
                    FirstName = newCustomer.FirstName,
                    LastName = newCustomer.LastName,
                    CreatedBy = newCustomer.CreatedBy,
                    DateCreated = newCustomer.DateCreated,
                    DateModified = newCustomer.DateModified
                });
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("api/v1/getCustomerInfo/{CustId}")]
        public async Task<IActionResult> GetCustomerInfoV1(int CustId)
        {
            try
            {
                var customer = await _customerRepositoryDapper.GetCustomerAsync(new()
                {
                    CustId = CustId
                });

                if ((customer?.FirstOrDefault()?.CustId ?? 0) > 0)
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

        [HttpGet("api/v2/getCustomerInfo/{CustId}")]
        public async Task<IActionResult> GetCustomerInfoV2(int CustId)
        {
            try
            {
                var customer = await _mediator.Send(new GetCustomerQuery() { CustomerId = CustId });

                if ((customer?.CustId ?? 0) > 0)
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
            try
            {
                var customers = _repository.GetModel();

                if (customers?.Any() ?? false)
                {
                    return Ok(customers.Select(item => new CustomerRequest()
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
    }
}
