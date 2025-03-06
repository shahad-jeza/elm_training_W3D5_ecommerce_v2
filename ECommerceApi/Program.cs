using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configure the port
builder.WebHost.UseUrls("http://localhost:5295", "https://localhost:7295");

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
            policy.WithOrigins("http://localhost:4200", "https://localhost:4200") // Allow both HTTP and HTTPS
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials(); // Allow credentials if needed
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

//app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");

// Sample product data with realistic images
var products = new List<Product>
{
    new Product { 
        Id = 1, 
        Name = "Laptop", 
        Description = "High-performance laptop with 16GB RAM and 512GB SSD.", 
        Price = 999.99M, 
        ImageUrl = "https://encrypted-tbn1.gstatic.com/shopping?q=tbn:ANd9GcRiUk6M1QhNchTPwh0I0phYBuEyZawiBNoWLy_CicoxS7W4AT8Zlp4ihAfmSMaWdE_hG_iz9kz4eQ&usqp=CAE" // Laptop image
    },
    new Product { 
        Id = 2, 
        Name = "Phone", 
        Description = "Latest smartphone with 128GB storage and 48MP camera.", 
        Price = 699.99M, 
        ImageUrl = "https://encrypted-tbn3.gstatic.com/shopping?q=tbn:ANd9GcR-Sm7fIYYWgL1tQ8VyeVKHpT5zqwIqziURo3QA0N1LQXyEY0EBrvGroMaaPDSHlfKmeKqdH3c&usqp=CAE" // Phone image
    },
    new Product { 
        Id = 3, 
        Name = "Tablet", 
        Description = "Lightweight and powerful tablet with 10-inch display.", 
        Price = 499.99M, 
        ImageUrl = "https://encrypted-tbn0.gstatic.com/shopping?q=tbn:ANd9GcSNX_NnVCWB5dJBDZYBGdO_sLXT2m4Ie7KD2Soyv293N1Y8Vshlb_h1LKWeS9pnPEeN5_lvU-Y&usqp=CAE" // Tablet image
    },
    new Product { 
        Id = 4, 
        Name = "Smartwatch", 
        Description = "Feature-packed smartwatch with heart rate monitoring.", 
        Price = 299.99M, 
        ImageUrl = "https://encrypted-tbn2.gstatic.com/shopping?q=tbn:ANd9GcT7qqewfLOSbXTzc6qGiK7XJeVYbe3E4lU9lvRKiMpu7UDJar8Xk75lTHCoCSFKQ_Cv8kKF-Q&usqp=CAE" // Smartwatch image
    },
    new Product { 
        Id = 5, 
        Name = "Headphones", 
        Description = "Noise-canceling over-ear headphones with 20-hour battery life.", 
        Price = 199.99M, 
        ImageUrl = "https://encrypted-tbn0.gstatic.com/shopping?q=tbn:ANd9GcQlZlcVwLRuYhNJbAx-yiRlAJCv4gdUctkUEGeZ3rn1p6uO4UhOLvLIpHrTcXk8wsDDLfljmsx7&usqp=CAE" // Headphones image
    },
    new Product { 
        Id = 6, 
        Name = "Gaming Console", 
        Description = "Next-gen gaming console with 4K gaming support.", 
        Price = 399.99M, 
        ImageUrl = "https://encrypted-tbn3.gstatic.com/shopping?q=tbn:ANd9GcSAUAbXSNsV1A2Xm_M4GlS1s8a3s_H3RcFlBnqjDcgZHIIZcxTt_5WBq_jg2cP5Ile20yTGJSOaIw&usqp=CAE" // Gaming console image
    }
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