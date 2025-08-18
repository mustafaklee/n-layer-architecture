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
    //referans tipliler için(örneðin string) nullable kontrolü yapma.custom validation için gerekli.
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
}
);


//fluent validation kullanacaðýmýz için default microsoft tarafýndan gelen hata mesajlarýný kapatýyoruz.
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);


//sira onemlidir, cunku criticalexception'da hatayý ele almýyoruz.sadece hata geldiðinde 
//gerekli iþlemleri yapýp geri dönüyoruz false ile.
//her seferinde criticalexception'a girer, daha sonra critical'in return tipi false oldugu ýcýn globalexception'a gider. 
builder.Services.AddExceptionHandler<CriticalExceptionHandler>();
//burda ise hatayý iþleme evresine denk geliyoruz.bu bölüm true döndüðü için exception baþka bir yere takýlmaz.
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddMemoryCache();
builder.Services.AddSingleton<ICacheService,CacheService>();


//Options Pattern üzerinden gittiðimiz için bu þekilde yapmadýk
//Ancak bu þekilde de yapabilirdik..
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
