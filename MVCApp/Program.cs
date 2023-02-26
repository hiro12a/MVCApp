using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MVCApp.Map;
using QuickKartDataAccessLayer.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Using HTTPContext
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

// Mapping
builder.Services.AddAutoMapper(x => x.AddProfile(new QuickKartMapper()));

// Connect to database
builder.Services.AddDbContext<QuickKartContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnections")));

var app = builder.Build();
app.UseSession();

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
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
