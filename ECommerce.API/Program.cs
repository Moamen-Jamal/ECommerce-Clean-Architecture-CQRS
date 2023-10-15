using Ecommerce.Application;
using Ecommerce.Application.Infrastructure;
using Ecommerce.Application.Persistence.Abstractions;
using Ecommerce.Infrastructure;
using Ecommerce.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped(typeof(EntitiesContext));
builder.Services.AddDbContext<EntitiesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ECommerceConnectionString")));

var ss = builder.Configuration.GetConnectionString("ECommerceConnectionString");
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

//Configure AutoMapper && MediatR && Fluient Validation
builder.Services.ConfigureApplicationService();
//foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
//{
//    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
//}

builder.Services.AddControllers();
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
