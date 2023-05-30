using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplicationProject.Data;
using WebApplicationProject.Hubs;
using WebApplicationProject.Interfaces;
using WebApplicationProject.Services;
using System.Linq;
using SignalRChat.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddSignalR();
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

//roles
//builder.Services.AddDefaultIdentity<IdentityUser>();

//File Uploading service. - Buffering mode.
//builder.Services.AddTransient<IBufferedFileUploadService, BufferedFileUploadLocalService>();

/*Got a better one_.
                  \
 Bigger file uploading service - Streaming mode.*/
builder.Services.AddTransient<IStreamFileUploadService, StreamFileUploadLocalService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

//app.MapHub<Likes>("/likes");

app.MapHub<ChatHub>("/chathub");


app.Run();


