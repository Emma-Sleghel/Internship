using System.Linq.Expressions;

namespace UserOperation.Data.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(int id);
        ICollection<T> GetAll();
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
        IEnumerable<T> Query(Func<T, bool> expression);
        IQueryable<T> Query();
    }
}
