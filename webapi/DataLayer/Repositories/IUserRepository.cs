using webapi.DataLayer.Repositories.Base;
using webapi.Models.BasicModel;
using webapi.Models.RequestModel;

namespace webapi.DataLayer.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByName(LoginRequestModel model);
    }
}
