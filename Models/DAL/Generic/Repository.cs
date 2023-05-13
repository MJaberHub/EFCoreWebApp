using Microsoft.EntityFrameworkCore;

namespace EFCoreWebApp.Models.DAL.Generic
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly MainDbContext _dataContext;
        private readonly DbSet<T> _dbEntity;

        public Repository(MainDbContext dataContext)
        {
            _dataContext = dataContext;
            _dbEntity = _dataContext.Set<T>();
        }

        public void DeleteModel(int modelId)
        {
            var model = _dbEntity.Find(modelId);
            _dbEntity.Remove(model);
        }

        public IEnumerable<T> GetModel()
        {
            return _dbEntity.ToList();
        }

        public T GetModelById(int modelId)
        {
            return _dbEntity.Find(modelId);
        }

        public void InsertModel(T model)
        {
            _dbEntity.Add(model);
        }

        public void Save()
        {
            _dataContext.SaveChanges();
        }

        public void UpdateModel(T model)
        {
            _dataContext.Entry(model).State = EntityState.Modified;
        }
    }
}
