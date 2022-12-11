using Microsoft.EntityFrameworkCore;

namespace webapi.DataLayer.Repositories.Base
{
    public interface IRepositoryV2<TC, T> : IRepository<T>
        where T : class
        where TC : DbContext
    {
    }
}
