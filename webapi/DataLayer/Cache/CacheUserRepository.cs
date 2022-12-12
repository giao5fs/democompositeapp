using Microsoft.Extensions.Caching.Memory;
using webapi.DataLayer.Repositories;
using webapi.Models.BasicModel;
using webapi.Models.RequestModel;

namespace webapi.DataLayer.Cache
{
    public class CacheUserRepository : IUserRepository
    {
        private readonly UserRepository _decorated;
        private readonly IMemoryCache _memoryCache;

        public CacheUserRepository(UserRepository decorated, IMemoryCache memoryCache)
        {
            _decorated = decorated;
            _memoryCache = memoryCache;
        }

        public bool Delete(User entity)
        {
            return _decorated.Delete(entity);
        }

        public List<User> GetAll()
        {
            return _decorated.GetAll();
        }

        public User GetById(int id)
        {
            return _decorated.GetById(id);
        }

        public User? GetByName(LoginRequestModel model)
        {
            string key = $"user:{model.UserName}";
            return  _memoryCache.GetOrCreate(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(1));
                    return _decorated.GetByName(model);
                });
        }

        public bool Insert(User entity)
        {
            return _decorated.Insert(entity);
        }

        public bool Update(User entity)
        {
            return _decorated.Update(entity);
        }
    }
}
