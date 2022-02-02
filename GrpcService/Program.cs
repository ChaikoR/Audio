using GrpcService.Data;
using GrpcService.Interface;
using GrpcService.Services;
using GrpcService.ServicesDB;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();

builder.WebHost.UseKestrel(options =>
{
    // configure Kestrel with our HTTPS certificate
    //options.ConfigureHttpsDefaults(ConfigureHttps);
    //options.Listen(IPAddress.Loopback, 7298);
    //// gRPC
    //options.ListenAnyIP(7298, o => o.Protocols = HttpProtocols.Http2);
    //// HTTP
    //options.ListenAnyIP(5298, o => o.Protocols = HttpProtocols.Http1);
});

var server = builder.Configuration["DBServer"] ?? "localhost";
var port = builder.Configuration["DBPort"] ?? "1433";
var user = builder.Configuration["DBUser"] ?? "SA";
var password = builder.Configuration["DBPassword"] ?? "pa55w0rd!";
var database = builder.Configuration["Database"] ?? "Audio";

builder.Services.AddDbContext<MessagesDBConext>(opt => 
    opt.UseSqlServer($"Server = {server}, {port}; Database = {database}; User Id = {user}; Password = {password};")
);

builder.Services.AddTransient<IMessagesServices, MessagesServices>();

var app = builder.Build();

//Запуск миграции + добавление данных в бд
MigrationManager.PrepPopulation(app);

app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
app.MapGrpcService<MessagesService>();



app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
