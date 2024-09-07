using Microsoft.AspNetCore.Http.Connections;
using StudySignalR.Hubs;
using StudySignalR.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMessageRepository, MessageRepository>();
builder.Services.AddSingleton<IChatRepository, ChatRepository>();

// Add services to the container.
builder.Services.AddCors(options =>
{
    // options.AddPolicy("CorsPolicy", builder => builder
    //     .WithOrigins("https://localhost:44485")
    //     .AllowAnyMethod()
    //     .AllowAnyHeader()
    //     .AllowCredentials()
    //     .SetIsOriginAllowed(_ => true));
});
builder.Services.AddSignalR();

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseWebSockets();

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapHub<ChatHub>("/chatHub", options =>
    {
        options.Transports = HttpTransportType.WebSockets;
    });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();