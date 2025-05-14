using Interfaces;
using Logic.Managers;
using Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.AddRazorPages();
builder.Services.AddSession();

//repo's
builder.Services.AddScoped<IArtistRepository>(provider => new ArtistRepository(connectionString));
builder.Services.AddScoped<IRaveRepository>(provider => new RaveRepository(connectionString));
builder.Services.AddScoped<IUserRepository>(provider => new UserRepository(connectionString));
builder.Services.AddScoped<ILineUpRepository>(provider => new LineUpRepository(connectionString));
builder.Services.AddScoped<IRecapRepository>(provider => new RecapRepository(connectionString));
builder.Services.AddScoped<ITicketRepository>(provider => new TicketRepository(connectionString));

//Managers
builder.Services.AddScoped<UserManager>();
builder.Services.AddScoped<ArtistManager>();
builder.Services.AddScoped<RaveManager>();
builder.Services.AddScoped<LineUpManager>();
builder.Services.AddScoped<RecapManager>();
builder.Services.AddScoped<TicketManager>();




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

app.UseSession();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
