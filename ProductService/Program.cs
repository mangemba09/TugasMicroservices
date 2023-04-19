using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductServices.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opts =>
    opts.JsonSerializerOptions.PropertyNamingPolicy = null);

// service automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// service interface product repo
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// service data client
builder.Services.AddHttpClient<IOrderDataClient, OrderDataClient>();

//see db service
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));

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

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
PrepDb.PrepPopulation(app);

app.Run();
