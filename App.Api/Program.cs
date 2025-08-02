using App.Repositories;
using App.Repositories.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//Options Pattern üzerinden gittiðimiz için bu þekilde yapmadýk
//Ancak bu þekilde de yapabilirdik..
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseMySql(
//        builder.Configuration.GetConnectionString("MySqlDbContext"),
//        new MySqlServerVersion(new Version(8, 0, 0))
//));

builder.Services.AddRepositories(builder.Configuration);





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
