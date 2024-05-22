using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SettlementBookingAgent_NET6._0.API;
using SettlementBookingAgent_NET6._0.API.Data;
using SettlementBookingAgent_NET6._0.API.Filters;
using SettlementBookingAgent_NET6._0.API.Interfaces;
using SettlementBookingAgent_NET6._0.API.Middlewares;
using SettlementBookingAgent_NET6._0.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<SBADbContext>(options =>
                options.UseInMemoryDatabase("SettlementBookingDatabase"));
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.Decorate<IBookingRepository, BookingServiceProxy>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SchemaFilter<NameFormattingSchemaFilter>();
});
builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMiddleware<CustomExceptionHandler>();
    
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
