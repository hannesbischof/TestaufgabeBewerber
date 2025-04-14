using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Backend;
using Backend.Repositories;
using Backend.Services;
using Backend.Mediator;
using Backend.Features.Products.Requests;
using Backend.Features.Categories.Requests;
using Backend.Features.Manufacturers.Requests;
using Backend.Models.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Entity Framework Core with SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Register Swagger for OpenAPI specification
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TestaufgabeBewerber API",
        Version = "v1",
        Description = "API documentation for TestaufgabeBewerber project"
    });
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IManufacturerRepository, ManufacturerRepository>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IManufacturerService, ManufacturerService>();

// Register Mediator
builder.Services.AddScoped<IMediator, Mediator>();

// Register Request Handlers for Products
builder.Services.AddScoped<IRequestHandler<GetProductsRequest, IEnumerable<DomainProduct>>, GetProductsRequestHandler>();
builder.Services.AddScoped<IRequestHandler<GetProductByIdRequest, DomainProduct?>, GetProductByIdRequestHandler>();
builder.Services.AddScoped<IRequestHandler<AddProductRequest, DomainProduct>, AddProductRequestHandler>();
builder.Services.AddScoped<IRequestHandler<UpdateProductRequest, DomainProduct>, UpdateProductRequestHandler>();
builder.Services.AddScoped<IRequestHandler<DeleteProductRequest, Unit>, DeleteProductRequestHandler>();

// Register Request Handlers for Categories
builder.Services.AddScoped<IRequestHandler<GetCategoriesRequest, IEnumerable<DomainCategory>>, GetCategoriesRequestHandler>();
builder.Services.AddScoped<IRequestHandler<GetCategoryByIdRequest, DomainCategory?>, GetCategoryByIdRequestHandler>();
builder.Services.AddScoped<IRequestHandler<AddCategoryRequest, DomainCategory>, AddCategoryRequestHandler>();
builder.Services.AddScoped<IRequestHandler<UpdateCategoryRequest, DomainCategory>, UpdateCategoryRequestHandler>();
builder.Services.AddScoped<IRequestHandler<DeleteCategoryRequest, Unit>, DeleteCategoryRequestHandler>();

// Register Request Handlers for Manufacturers
builder.Services.AddScoped<IRequestHandler<GetManufacturersRequest, IEnumerable<DomainManufacturer>>, GetManufacturersRequestHandler>();
builder.Services.AddScoped<IRequestHandler<GetManufacturerByIdRequest, DomainManufacturer?>, GetManufacturerByIdRequestHandler>();
builder.Services.AddScoped<IRequestHandler<AddManufacturerRequest, DomainManufacturer>, AddManufacturerRequestHandler>();
builder.Services.AddScoped<IRequestHandler<UpdateManufacturerRequest, DomainManufacturer>, UpdateManufacturerRequestHandler>();
builder.Services.AddScoped<IRequestHandler<DeleteManufacturerRequest, Unit>, DeleteManufacturerRequestHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestaufgabeBewerber API v1"));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();