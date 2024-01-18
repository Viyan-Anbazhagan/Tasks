// Project title : Connecteam
// Author : Viyan A
// Created at : 15/02/2023
// Modified at : 25/03/2023
// Reviewed by : Anitha manogaran
// Reviewed at : 15/03/2023

using Microsoft.AspNetCore.Authentication.Cookies;
using EmployeeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Data;
using Swashbuckle.AspNetCore.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddDbContext<AppDbContext>(options => {options.UseSqlServer(builder.Configuration.GetConnectionString("DB1"));});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(3);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddAuthentication(option =>{
    option.DefaultAuthenticateScheme= CookieAuthenticationDefaults.AuthenticationScheme;
    option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie();

builder.Services.AddTransient<IRepository,Repository>();

builder.Services.AddAuthorization(option =>{
    option.AddPolicy("Admin",policy => policy.RequireRole("Admin"));
    option.AddPolicy("Manager",policy => policy.RequireRole("Manager"));
    option.AddPolicy("Employee",policy => policy.RequireRole("Employee"));
    option.AddPolicy("SystemAdmin",policy => policy.RequireRole("SystemAdmin"));
});
builder.Services.AddMvc();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseSession();

app.UseSwagger();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
