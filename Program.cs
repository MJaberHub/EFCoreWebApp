using EFCoreWebApp.Models;
using EFCoreWebApp.Models.DAL.Generic;
using Hangfire;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


///Hangfire config
builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireDB")));
builder.Services.AddHangfireServer();  //the server which processes the jobs


///adding Db Context injection
builder.Services.AddDbContext<MainDbContext>(
       options => options.UseSqlServer(builder.Configuration.GetConnectionString("MainDB")));

///DI
builder.Services.AddScoped<IRepository<TCustomer>, Repository<TCustomer>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

///Hangfire config
app.UseHangfireDashboard();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
