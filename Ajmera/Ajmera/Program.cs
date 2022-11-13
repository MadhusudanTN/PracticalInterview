using Ajmera.Filters;
using Ajmera.IRepository;
using Ajmera.IServices;
using Ajmera.Models;
using Ajmera.Repository;
using Ajmera.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Log.Logger = new LoggerConfiguration()
      .Enrich.FromLogContext()
      .Enrich.WithExceptionDetails()
      .WriteTo.Console()
      .CreateLogger();

var connectionString = Environment.GetEnvironmentVariable("PostGreSqlConnection");
builder.Services.AddDbContext<AjmeraContext>(options => options.UseNpgsql(connectionString), ServiceLifetime.Scoped);
builder.Services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddControllers(options =>
{
    options.OutputFormatters.RemoveType<Microsoft.AspNetCore.Mvc.Formatters.HttpNoContentOutputFormatter>();
    options.Filters.Add(typeof(AjmeraExceptionFilter));
    options.Filters.Add(typeof(AjmeraModelAttribute));
}).AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Practial Interview", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();