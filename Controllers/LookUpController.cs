using EFCoreWebApp.Models;
using EFCoreWebApp.Models.DAL.Cache;
using EFCoreWebApp.Models.DAL.Generic;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreWebApp.Controllers
{
    public class LookUpController : ControllerBase
    {
        private readonly IRepository<TBankList> _bankListRepository;
        private readonly ILogger<LookUpController> _logger;
        private readonly ICachedRepo _cache;

        public LookUpController(IRepository<TBankList> bankListRepository, ILogger<LookUpController> logger, ICachedRepo cache)
        {
            _bankListRepository = bankListRepository;
            _logger = logger;
            _cache = cache;
        }


        [HttpGet("api/v1/getAllBanks")]
        public async Task<IActionResult> GetAllBanksV1()
        {
            try
            {
                var value = _cache.GetOrSet("BankList", () => _bankListRepository.GetModel(), 10).ToList();

                if ((value?.Count ?? 0) > 0)
                {

                    return Ok(value);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("api/v2/getAllBanks")]
        public async Task<IActionResult> GetAllBanksV2()
        {
            try
            {
                var value = await _cache.GetAsync<List<TBankList>>("BankList");

                if (value is null)
                {
                    value = _bankListRepository.GetModel().ToList();
                    await _cache.SetAsync("BankList", value, 100);
                }

                return Ok(value);
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
