using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ECommerce API", Version = "v1" });
});

// Enable CORS for Angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Allow requests from Angular app
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce API v1");
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");

// Serve static files (e.g., product images)
var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
if (!Directory.Exists(imagesPath))
{
    Directory.CreateDirectory(imagesPath);
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(imagesPath),
    RequestPath = "/Images"
});

// Sample product data
var products = new List<Product>
{
new Product { Id = 1, Name = "Laptop", Description = "High-performance laptop", Price = 999.99M, ImageUrl = "/Images/laptop.jpg" },
new Product { Id = 2, Name = "Phone", Description = "Latest smartphone", Price = 699.99M, ImageUrl = "/Images/phone.jpg" },
new Product { Id = 3, Name = "Tablet", Description = "Lightweight and powerful tablet", Price = 499.99M, ImageUrl = "/Images/tablet.jpg" },
new Product { Id = 4, Name = "Smartwatch", Description = "Feature-packed smartwatch", Price = 299.99M, ImageUrl = "/Images/smartwatch.jpg" },
new Product { Id = 5, Name = "Headphones", Description = "Noise-canceling over-ear headphones", Price = 199.99M, ImageUrl = "/Images/headphones.jpg" },
new Product { Id = 6, Name = "Gaming Console", Description = "Next-gen gaming console", Price = 399.99M, ImageUrl = "/Images/console.jpg" }
};

// Endpoint to get all products
app.MapGet("/api/products", () =>
{
    return Results.Ok(products);
})
.WithName("GetProducts");

// Endpoint to get a single product by ID
app.MapGet("/api/products/{id}", (int id) =>
{
    var product = products.FirstOrDefault(p => p.Id == id);
    if (product == null)
    {
        return Results.NotFound("Product not found");
    }
    return Results.Ok(product);
})
.WithName("GetProductById");

app.Run();

// Product Class
class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
}