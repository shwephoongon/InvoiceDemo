

using InvoiceDemo.Api;
using InvoiceDemo.DbService.Data;
using InvoiceDemo.DbService.Models;
using InvoiceDemo.Dtos;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.  
builder.Services.AddControllers()
     .PartManager
    .ApplicationParts
    .Add(new AssemblyPart(typeof(InvoiceController).Assembly));
builder.Services.AddDbContext<InvoiceDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 36)) // Use your actual MySQL version
    ));
builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<CreateInvoiceDto, Invoice>();
    // Add more mappings here
}, typeof(Program).Assembly);
builder.Services.AddScoped<InvoiceRepository>();
builder.Services.AddScoped<InvoiceService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle  
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
