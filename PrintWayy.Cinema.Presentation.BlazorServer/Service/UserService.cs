using Microsoft.AspNetCore.Components;
using PrintWayy.Cinema.Domain.Models;
using PrintWayy.Cinema.Presentation.BlazorServer.Models;
using PrintWayy.Cinema.Presentation.BlazorServer.Service.Interfaces;
using PrintWayy.Cinema.Presentation.BlazorServer.Shared;

namespace PrintWayy.Cinema.Presentation.BlazorServer.Service
{
    public class UserService : IUserService
    {
        private IHttpService _httpService;
        private ILocalStorageService _localStorageService;
        private NavigationManager _navigationManager;
        private string _userKey = "user";

        public AuthenticateData User { get; private set; }

        public UserService(IHttpService httpService, ILocalStorageService localStorageService, NavigationManager navigationManager)
        {
            _httpService = httpService;
            _localStorageService = localStorageService;
            _navigationManager = navigationManager;
        }

        public async Task Initialize()
        {
            User = await _localStorageService.GetItem<AuthenticateData>(_userKey);
        }

        public async Task Login(Login model)
        {
            User = await _httpService.Post<AuthenticateData>("/api/v1/user/login", new User { UserName = model.Username, Password = model.Password, Role = "Admin"});
            await _localStorageService.SetItem(_userKey, User);
        }

        public async Task Logout()
        {
            User = null;
            await _localStorageService.RemoveItem(_userKey);
            _navigationManager.NavigateTo("/user/login");
        }
    }
}
