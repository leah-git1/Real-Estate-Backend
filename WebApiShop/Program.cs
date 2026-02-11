using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using Repository;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IUsersServices, UsersServices>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoriesServies, CategoriesServies>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();

builder.Host.UseNLog();
builder.Services.AddDbContext<ShopContext>(option => option.UseSqlServer("Server = srv2\\pupils; Database = RealEstate_DB; Trusted_Connection = True; TrustServerCertificate = True;"));
//builder.Configuration.GetConnectionString("DefaultConnection")
// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
//builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//    app.UseSwaggerUI(options =>
//    {
//        options.SwaggerEndpoint("/openapi/v1.json", "My API V1");
//    });
//}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
