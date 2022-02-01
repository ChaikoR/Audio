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

builder.Services.AddDbContext<MessagesDBConext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IMessagesServices, MessagesServices>();

var app = builder.Build();

app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
app.MapGrpcService<MessagesService>();



app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
app.MigrateDatabase();
app.Run();
