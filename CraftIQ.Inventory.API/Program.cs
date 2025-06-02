
using CraftIQ.Inventory.Core.Interfaces;
using CraftIQ.Inventory.Infrastructure;
using CraftIQ.Inventory.Services;
using CraftIQ.Inventory.Services.CategoriesImplementations;
using huzcodes.Extensions.Exceptions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

////// adding Db Context Registration
var inventoryDbConnectionString = builder.Configuration.GetSection("ConnectionStrings:InventoryDbConnection");
builder.Services.AddInventoryDbContext(inventoryDbConnectionString.Value!);

builder.Services.AddInfrastructureRegistrations();

builder.Services.AddServicesRegistrations();



var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.AddExceptionHandlerExtension();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
