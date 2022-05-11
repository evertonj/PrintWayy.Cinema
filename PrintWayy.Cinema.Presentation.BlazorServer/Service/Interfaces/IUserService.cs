using PrintWayy.Cinema.Domain.Models;
using PrintWayy.Cinema.Presentation.BlazorServer.Models;

namespace PrintWayy.Cinema.Presentation.BlazorServer.Service.Interfaces
{
    public interface IUserService
    {
        AuthenticateData User { get; }
        Task Initialize();
        Task Login(Login model);
        Task Logout();
    }
}
