using Microsoft.EntityFrameworkCore;

namespace EFCoreWebApp.Models.DAL.Generic
{
    public class AllRepository<T> : IAllRepository<T> where T : class
    {
        private readonly MainDbContext _dataContext;
        private readonly DbSet<T> _dbEntity;

        public AllRepository(MainDbContext dataContext)
        {
            _dataContext = dataContext;
            _dbEntity = _dataContext.Set<T>();
        }

        public void DeleteModel(T modelId)
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
