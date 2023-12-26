using System.Reflection;
using CoursesApi.Repository;
//using CoursesApi.Services;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

//builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddSingleton<ICourseRepository, CourseRepository>();
//builder.Services.AddScoped<ICourseService, CourseService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
//

public partial class Program { }

