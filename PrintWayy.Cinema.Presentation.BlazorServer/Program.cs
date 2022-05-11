using PrintWayy.Cinema.Presentation.BlazorServer.Service;
using PrintWayy.Cinema.Presentation.BlazorServer.Service.Interfaces;
using PrintWayy.Cinema.Presentation.BlazorServer.Shared;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<IFilmService, FilmService>();
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped<IAlertService, AlertService>();
builder.Services.AddScoped(x =>
{
    var apiUrl = new Uri("https://localhost:7084");
    return new HttpClient() { BaseAddress = apiUrl };
});
builder.Services.AddSingleton<PageHistoryState>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
