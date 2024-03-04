using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Vega.Mapping;
using Vega.Models;
using Vega.Persistance;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://dev-58m8zk08sc8wh047.us.auth0.com/";
        options.Audience = "https://vega.api.vehicles.com";
    });


builder.Services.AddControllers();
builder.Services.AddSingleton(builder.Environment);

builder.Services.AddDbContext<VegaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();
builder.Services.Configure<PhotoSettings>(builder.Configuration.GetSection("PhotoSettings"));

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "wwwroot")),
    RequestPath = "/wwwroot"
});

app.UseRouting();

// Use CORS middleware
app.UseCors("AllowSpecificOrigin");

// Add UseAuthorization after UseRouting
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();
