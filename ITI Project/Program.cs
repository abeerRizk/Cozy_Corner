using ITI_Project.DAL.DB.ApplicationDB;
using ITI_Project.BLL.Services.Implementation;
using ITI_Project.BLL.Services.Interface;
using ITI_Project.BLL.Mapping;
using ITI_Project.DAL.Repo.Impelemntation;
using ITI_Project.DAL.Repo.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using ITI_Project.BLL.Services.Impelemntation;
using ITI_Project.DAL.Entites;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer("name=DefaultConnection"));
builder.Services.AddScoped<IVendorRepo, VendorRepo>();
builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IVendorService, VendorService>();
builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepo, OrderRepo>();
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderItemRepo, OrderItemRepo>();
builder.Services.AddScoped<IOrderItemsService, OrderItemsService>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IRatingRepo, RatingRepo>();
builder.Services.AddScoped<IINvoiceRepo, InvoiceRepo>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IFavoriteRepo, FavoriteRepo>();
builder.Services.AddScoped<IFavoriteService, FavoriteService>();

builder.Services.AddScoped<IFollowRepo, FollowRepo>();
builder.Services.AddScoped<IFollowService, FollowService>();

builder.Services.AddScoped<INotificationRepo, NotificationRepo>();
builder.Services.AddScoped<INotificationService, NotificationService>();


//use configuration
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    // Identity options here
    options.SignIn.RequireConfirmedAccount = false; // Adjust as per your needs
})
           .AddEntityFrameworkStores<ApplicationDbContext>()
           .AddDefaultTokenProviders();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
   .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
       options =>
       {
           options.LoginPath = new PathString("/Account/Register");
           options.AccessDeniedPath = new PathString("/Account/Register");
       });




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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
