using Microsoft.EntityFrameworkCore;

namespace EFCoreWebApp.Models.DAL.Generic
{
    public class AllRepository<T> : IAllRepository<T> where T : class
    {
        private readonly static MainDbContext _dataContext = new (); //one single instance of Dbcontext for all the controllers
        private readonly DbSet<T> _dbEntity;
        
        public AllRepository()
        {
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
