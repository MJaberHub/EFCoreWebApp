namespace EFCoreWebApp.Models.DAL.Generic
{
    public interface IAllRepository<T> where T : class
    {
        IEnumerable<T> GetModel();
        T GetModelById(int modelId);
        void UpdateModel (T model);
        void DeleteModel (T modelId);
        void Save();
    }
}
