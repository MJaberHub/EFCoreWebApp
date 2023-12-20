using EFCoreWebApp.Models;
using EFCoreWebApp.Models.DAL.Generic;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace EFCoreWebApp.Services
{
    public class BankLookUpService : IBankLookUpService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IRepository<TBankList> _bankListRepository;
        private readonly string cacheKey = "BankLookUpService";

        public BankLookUpService(IMemoryCache memoryCache, IRepository<TBankList> bankListRepository)
        {
            _memoryCache = memoryCache;
            _bankListRepository = bankListRepository;
        }

        public async Task<List<TBankList>> GetBankList()
        {
            try
            {
                if (!_memoryCache.TryGetValue(cacheKey, out List<TBankList> bankList))
                {
                    bankList = _bankListRepository.GetModel().ToList();

                    _memoryCache.Set(cacheKey, bankList,
                        new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(1000)));
                }
                return bankList;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
