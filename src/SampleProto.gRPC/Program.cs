using SampleProto.gRPC.Services;
using SampleProto.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PersonDbContext>(options =>
    options.UseInMemoryDatabase("PersonDb"));

builder.Services.AddGrpc();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyHeader()
               .AllowAnyMethod()
               .SetIsOriginAllowed(_ => true)
               .AllowCredentials();
    });
});

var app = builder.Build();

app.UseRouting();

app.UseGrpcWeb();

app.MapGrpcService<PersonService>().EnableGrpcWeb();

app.MapGet("/", () => "Use a gRPC client to communicate with this server.");

app.Run();
