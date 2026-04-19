using Contacts.Data;
using Contacts.Interfaces;
using Contacts.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if(connectionString == null)
{
    throw new Exception("Connection string not found");
}

// Add services to the container.
builder.Services.AddDbContext<ContactsDbContext>(options =>
    options.UseMySQL(connectionString));

builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<ISubCategoriesRepository, SubCategoriesRepository>();
builder.Services.AddScoped<IContactsRepository, ContactsRepository>();

builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<ISubCategoriesService, SubCategoriesService>();
builder.Services.AddScoped<IContactsService, ContactsService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
