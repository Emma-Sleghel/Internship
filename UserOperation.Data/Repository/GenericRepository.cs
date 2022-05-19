using Microsoft.EntityFrameworkCore;


namespace UserOperation.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        private DbSet<T> entities;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            entities = _context.Set<T>();
        }
        public T GetById(object id)
        {
            return entities.Find(id);
        }

        public ICollection<T> GetAll()
        {
            return entities.AsNoTracking().ToList();   
        }

        public void Create(T entity)
        {
            entities.Add(entity);
            Save();
        }

        public void Update(T entity)
        {
            entities.Update(entity);
            Save();
        }

        public void Delete(T entity)
        {
            entities.Remove(entity);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        
        public IEnumerable<T> Query(Func<T, bool> expression)
        {
            return entities.Where(expression);
        }
        public IQueryable<T> Query()
        {
            return entities.AsQueryable();
        }

       
    }
}
