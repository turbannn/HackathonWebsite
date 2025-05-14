using HackathonWebsite.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FluentValidation;
using HackathonWebsite.BLL.Services;

var builder = WebApplication.CreateBuilder(args);

// Validators
builder.Services.AddValidatorsFromAssembly(Assembly.Load("HackathonWebsite.BLL"));

// Mappers
builder.Services.AddAutoMapper(Assembly.Load("HackathonWebsite.BLL"));

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<HackathonDbContext>(opt => opt.UseSqlite(connectionString));

builder.Services.AddScoped<UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
