using Microsoft.Extensions.Configuration;
using PrintWayy.Cinema.Domain.Interfaces;
using PrintWayy.Cinema.Domain.Models;

namespace PrintWayy.Cinema.Infra.Data
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IConfiguration? configuration) : base(configuration)
        {
        }

        public User? Authenticate(User user)
        {
            if (user.UserName == "admin" && user.Password == "admin")
            {
                return user;
            }
            return null;
        }
    }
}
