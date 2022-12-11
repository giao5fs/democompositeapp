using webapi.DataLayer.Repositories.Base;
using webapi.Models.BasicModel;

namespace webapi.DataLayer.Repositories
{
    public class UserSessionRepository : RepositoryService<UserSession> ,IUserSessionRepository
    {
        public UserSessionRepository(AppDbContext context) : base(context)
        {

        }
    }
}
