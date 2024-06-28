namespace EFCoreWebApp.Models.DAL.Cache
{
    public interface ICachedRepo
    {
        Task<T> GetAsync<T>(string key);
        Task SetAsync(string key, object data, int cacheTimeInMinutes);
        T GetOrSet<T>(string key, Func<T> factory, int cacheTimeInMinutes);
        Task RemoveAsync(string key);
        bool TryGetValue<T>(string key, out T result);
    }
}
