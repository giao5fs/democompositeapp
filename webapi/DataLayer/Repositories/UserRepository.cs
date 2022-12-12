using webapi.DataLayer.Repositories.Base;
using webapi.Models.BasicModel;
using webapi.Models.RequestModel;

namespace webapi.DataLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext context;

        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }
        public bool Delete(User entity)
        {
            var user = context.Users.Find(entity.Id);
            if (user != null)
            {
                context.Users.Remove(user);
                return context.SaveChanges() > 0;
            }
            else return false;
        }

        public List<User> GetAll()
        {
            return context.Set<User>().ToList();
        }

        public User GetById(int id)
        {
            return context.Set<User>().Find(id);
        }

        public User GetByName(LoginRequestModel model)
        {
            var user = (from u in context.Set<User>()
                        where u.UserName == model.UserName
                                  && u.Password == model.Password
                        select u).FirstOrDefault();
            if (user is null)
            {
                return null;
            }
            return user;
        }

        public bool Insert(User entity)
        {
            context.Set<User>().Add(entity);
            return context.SaveChanges() > 0;
        }

        public bool Update(User entity)
        {
            var user = context.Set<User>().Attach(entity);
            user.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return context.SaveChanges() > 0;
        }
    }

}
