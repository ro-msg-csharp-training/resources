using OnlineOrder.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineOrder.Data;
using OnlineOrder.Controllers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<OnlineOrderContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OnlineOrderContext") ?? throw new InvalidOperationException("Connection string 'OnlineOrderContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IProductCollection, ProductCollection>(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.MapProductEndpoints();

app.Run();
