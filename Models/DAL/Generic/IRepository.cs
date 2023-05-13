namespace EFCoreWebApp.Models.DAL.Generic
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetModel();
        T GetModelById(int modelId);
        void InsertModel(T model);
        void UpdateModel (T model);
        void DeleteModel (int modelId);
        void Save();
    }
}
