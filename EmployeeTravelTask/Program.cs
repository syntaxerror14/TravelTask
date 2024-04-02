using EmployeeTravelTask.DAL.Classes;
using EmployeeTravelTask.DAL.Interfaces;
using EmployeeTravelTask.Models;
using EmployeeTravelTask.Services.Classes;
using EmployeeTravelTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//dependency injection containers

builder.Services.AddScoped<ILocationService,  LocationService>();
builder.Services.AddScoped<ILocationRepo, LocationRepo>();
builder.Services.AddScoped<ITravelRequestService, TravelRequestService>();
builder.Services.AddScoped<ITravelRequestRepo, TravelRequestRepo>();
builder.Services.AddScoped<ITPUserRepo, TPUserRepo>();
builder.Services.AddScoped<ITravelBudgetAllocationService, TravelBudgetAllocationService>();
builder.Services.AddScoped<ITravelBudgetAllocationRepo,TravelBudgetAllocationRepo>();

builder.Services.AddDbContext<TravelPlannerContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("dbcs"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
