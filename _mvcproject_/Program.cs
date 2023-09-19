using Microsoft.EntityFrameworkCore.SqlServer;
using _mvcproject_.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using _mvcproject_.Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = "414769320393-ivgl8s0dvvmbbktak0nt1a4u2d1g7do9.apps.googleusercontent.com";
    googleOptions.ClientSecret = "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";
})
    .AddFacebook(facebookOPtions =>
    {
        facebookOPtions.AppId = "1012607459633507";
        facebookOPtions.AppSecret = "5e0c6469c369768061faa5640348a9b1";
    });
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IOrderRepository, OrderRepoService>();
builder.Services.AddScoped<IServiceRepository, ServiceRepoService>();

builder.Services.AddScoped<IWorksRepository, WorksRepoService>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<OurContext>();
var conn = builder.Configuration.GetConnectionString("myconn");
builder.Services.AddDbContext<OurContext>(op => op.UseSqlServer(conn));

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Service}/{action=getall}/{id?}");

app.Run();
