﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TrackingFrontEnd.Data;
using TrackingFrontEnd.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TrackingFrontEndContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TrackingFrontEndContext") ?? throw new InvalidOperationException("Connection string 'TrackingFrontEndContext' not found.")));

builder.Services.AddHttpClient<ITrackingAccountService, TrackingAccountService>(c=>
{
    c.BaseAddress = new Uri("https://localhost:7072");
});
// Add services to the container.
builder.Services.AddControllersWithViews();

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
