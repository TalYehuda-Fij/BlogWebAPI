using BlogWebAPI.BlogAPI.DataContext;
using BlogWebAPI.BlogAPI.Service;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBlogPostService, BlogPostService>();



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapControllers();
app.UseHttpsRedirection();


app.Run();
