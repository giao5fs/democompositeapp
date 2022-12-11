using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using webapi.DataLayer.Repositories.Base;

namespace webapi.DataLayer.Repositories
{
    public class RepositoryService<T> : RepositoryServiceBase<T>, IRepository<T>
        where T : class
    {
        private object ojLock = new object();

        private DbSet<T> _entities;
        private readonly DbContext Context;

        public RepositoryService(DbContext context) => Context = context;

        private DbSet<T> Entities
        {
            get
            {
                if (_entities != null)
                {
                    return _entities;
                }
                lock (ojLock)
                {
                    if (_entities == null)
                    {
                        _entities = Context.Set<T>();
                    }
                    return _entities;
                }
            }
        }
        public bool Insert(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            PrepareDataUpdateOrInsert(entity);
            Entities.Add(entity);
            return Context.SaveChanges() > 0;
        }

        public List<T> GetAll()
        {
            return Entities.ToList();
        }

        public T GetById(int id)
        {
            return Entities.Find(id);
        }

        public bool Update(T entity)
        {
            Entities.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return Context.SaveChanges() > 0;
        }

        public bool Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            Context.Entry(entity).State = EntityState.Deleted;
            return Context.SaveChanges() > 0;
        }

        protected override void PrepareDataUpdateOrInsert(T entity)
        {
            Console.WriteLine(entity);
            base.PrepareDataUpdateOrInsert(entity);
        }
    }

    public abstract class RepositoryServiceBase<T> where T : class
    {
        protected virtual void PrepareDataUpdateOrInsert(T entity) { }
    }
}
