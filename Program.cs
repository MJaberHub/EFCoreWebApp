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


///adding Db Context injection  //no need for DI since the instance was initiated in the Repository
//var connectionString = "\"Server=E207;Database=MainDB;Trusted_Connection=True;Trust Server Certificate = true\"";
//builder.Services.AddDbContext<MainDbContext>(
//       options => options.UseSqlServer(connectionString));

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
