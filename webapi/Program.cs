using Microsoft.EntityFrameworkCore;
using webapi.Commons;
using webapi.DataLayer.Cache;
using webapi.DataLayer.Repositories;
using webapi.DataLayer.Repositories.Base;

var builder = WebApplication.CreateBuilder(args);

var conStr = builder.Configuration.GetConnectionString(Environment.MachineName);

builder.Services.AddScoped<UserRepository>();

builder.Services.AddScoped<IUserRepository, CacheUserRepository>();

builder.Services.AddScoped<IUserSessionRepository, UserSessionRepository>();

builder.Services.AddMemoryCache();

builder.Services.AddControllers();

builder.Services.AddCors(action =>
{
    action.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("*");
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(conStr);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
