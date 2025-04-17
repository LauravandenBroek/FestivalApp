using FestivalApp.Data;
using FestivalApp.Interfaces;
using FestivalApp.Managers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.AddRazorPages();
builder.Services.AddScoped<IArtistRepository>(provider => new ArtistRepository(connectionString));
builder.Services.AddScoped<RaveRepository>(provider => new RaveRepository(connectionString));
builder.Services.AddScoped<ArtistManager>();
builder.Services.AddScoped<RaveManager>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
