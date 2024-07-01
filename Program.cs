using EFCoreWebApp.Models;
using EFCoreWebApp.Models.DAL;
using EFCoreWebApp.Models.DAL.Cache;
using EFCoreWebApp.Models.DAL.DapperDAL;
using EFCoreWebApp.Models.DAL.Generic;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


///Hangfire config
builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireDB")));
//builder.Services.AddHangfireServer();  //the server which processes the jobs


///adding Db Context injection
builder.Services.AddDbContext<MainDbContext>(
       options => options.UseSqlServer(builder.Configuration.GetConnectionString("MainDB")));

//Redis connection string
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["RedisCache:Connection"];
    options.InstanceName = builder.Configuration["RedisCache:InstanceName"];
});

//in memory cache
builder.Services.AddMemoryCache();

///DI
builder.Services.AddScoped<IRepository<TCustomer>, Repository<TCustomer>>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerRepositoryDapper, CustomerRepositoryDapper>();
builder.Services.AddScoped<IRepository<TBankList>, Repository<TBankList>>();
//builder.Services.AddScoped<ICachedRepo, CachedRepo>();
builder.Services.AddScoped<ICachedRepo, CachedRepoMemory>();


///Serilog Config ///Serilog implements the ILogger interface existing in the Microsoft.Extension by this Serilog was injected
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//implementing minimal APIs // should come after building the services to be able to use them
app.MapGet("/AllCustomer", (ICustomerRepository customerRepo) =>
{
    try
    {
        return Results.Ok(customerRepo.GetModel()); //handles the status code
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex);
    }
});

app.MapGet("/AllCustomer/{id:int}", (int id, ICustomerRepository customerRepo) =>
{
    return customerRepo.GetModelById(id);
});

///Hangfire config
app.UseHangfireDashboard();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
