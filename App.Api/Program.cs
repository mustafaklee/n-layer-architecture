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
    //referans tipliler için(örneðin string) nullable kontrolü yapma.custom validation için gerekli.
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
}
);


//fluent validation kullanacaðýmýz için default microsoft tarafýndan gelen hata mesajlarýný kapatýyoruz.
builder.Services.Configure<ApiBehaviorOptions>(options=>options.SuppressModelStateInvalidFilter= true);



//Options Pattern üzerinden gittiðimiz için bu þekilde yapmadýk
//Ancak bu þekilde de yapabilirdik..
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
