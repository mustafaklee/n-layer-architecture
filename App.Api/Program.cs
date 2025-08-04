using App.Repositories.Extensions;
using App.Services;
using App.Services.Extensions;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<FluentValidationFilter>();
    //referans tipliler i�in(�rne�in string) nullable kontrol� yapma.custom validation i�in gerekli.
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
}
);


//fluent validation kullanaca��m�z i�in default microsoft taraf�ndan gelen hata mesajlar�n� kapat�yoruz.
builder.Services.Configure<ApiBehaviorOptions>(options=>options.SuppressModelStateInvalidFilter= true);



//Options Pattern �zerinden gitti�imiz i�in bu �ekilde yapmad�k
//Ancak bu �ekilde de yapabilirdik..
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseMySql(
//        builder.Configuration.GetConnectionString("MySqlDbContext"),
//        new MySqlServerVersion(new Version(8, 0, 0))
//));

builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration);





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
