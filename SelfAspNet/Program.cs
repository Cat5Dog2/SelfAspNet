using System;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using SelfAspNet.Helpers;
using SelfAspNet.Models;
using SelfAspNet.CompiledModels;
using SelfAspNet.Lib;
using Microsoft.CodeAnalysis.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ITagHelperComponent, MetaTagHelperComponent>();

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.ValueProviderFactories.Add(new HttpCookieValueProviderFactory());
    //options.ModelBinderProviders.Insert(0, new DateModelBinderProvider());
});
builder.Services.AddDbContext<MyContext>(options =>
    options
        .UseLazyLoadingProxies()
        .UseModel(MyContextModel.Instance)
        .UseSqlServer(
            builder.Configuration.GetConnectionString("MyContext")
        )
);

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var provider = scope.ServiceProvider;
    await Seed.Initialize(provider);
}

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
