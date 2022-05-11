using PrintWayy.Cinema.Domain.Models;

namespace PrintWayy.Cinema.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User Authenticate(User user);
    }
}
