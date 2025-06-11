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

builder.Services.AddSingleton(connectionString);
//repo's
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<IRaveRepository, RaveRepository>();
builder.Services.AddScoped<IRecapRepository, RecapRepository>();
builder.Services.AddScoped<ILineUpRepository, LineUpRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IAttendingRaveRepository, AttendingRaveRepository>();
builder.Services.AddScoped<IRaveWishlistRepository, RaveWishlistRepository>();
builder.Services.AddScoped<IFavoriteArtistRepository, FavoriteArtistRepository>();


//Managers
builder.Services.AddScoped<UserManager>();
builder.Services.AddScoped<ArtistManager>();
builder.Services.AddScoped<RaveManager>();
builder.Services.AddScoped<LineUpManager>();
builder.Services.AddScoped<RecapManager>();
builder.Services.AddScoped<TicketManager>();
builder.Services.AddScoped<AttendingRaveManager>();
builder.Services.AddScoped<RaveWishlistManager>();
builder.Services.AddScoped<FavoriteArtistManager>();


builder.Services.AddHttpContextAccessor();



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
