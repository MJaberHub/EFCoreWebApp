using EFCoreWebApp.Models;

namespace EFCoreWebApp.Services
{
    public interface IBankLookUpService
    {
        public Task<List<TBankList>> GetBankList();
    }
}
