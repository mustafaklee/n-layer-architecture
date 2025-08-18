using App.Application.Contracts.Caching;
using App.Application.Extensions;
using App.Caching;
using App.Persistence.Extensions;
using CleanApp.API.ExceptionHandler;
using CleanApp.API.Filters;
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
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);


//sira onemlidir, cunku criticalexception'da hatay� ele alm�yoruz.sadece hata geldi�inde 
//gerekli i�lemleri yap�p geri d�n�yoruz false ile.
//her seferinde criticalexception'a girer, daha sonra critical'in return tipi false oldugu �c�n globalexception'a gider. 
builder.Services.AddExceptionHandler<CriticalExceptionHandler>();
//burda ise hatay� i�leme evresine denk geliyoruz.bu b�l�m true d�nd��� i�in exception ba�ka bir yere tak�lmaz.
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddMemoryCache();
builder.Services.AddSingleton<ICacheService,CacheService>();


//Options Pattern �zerinden gitti�imiz i�in bu �ekilde yapmad�k
//Ancak bu �ekilde de yapabilirdik..
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseMySql(
//        builder.Configuration.GetConnectionString("MySqlDbContext"),
//        new MySqlServerVersion(new Version(8, 0, 0))
//));

builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration);




var app = builder.Build();

app.UseExceptionHandler(x => { });


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
