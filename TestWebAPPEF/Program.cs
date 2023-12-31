using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestWebAPPEF.Data;
using TestWebAPPEF.Models;
using TestWebAPPEF.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TestEFContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TestEFContext") ?? throw new InvalidOperationException("Connection string 'TestEFContext' not found.")));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IMovieInterface, MovieService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//ConfigMapper();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
//void ConfigMapper()
//{
//    TypeAdapterConfig<Movie, MovieDto>.NewConfig()
//        .Ignore(des => des.Id)
//        .Map(des => des.LanguageYear
//        ,src=>src.Language+" year is "+src.ReleaseYear);
//}
