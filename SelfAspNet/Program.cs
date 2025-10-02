using System;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using SelfAspNet.Helpers;
using SelfAspNet.Models;
using SelfAspNet.CompiledModels;
using SelfAspNet.Lib;
using SelfAspNet.Filters;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddResponseCaching();

builder.Services.AddTransient<ITagHelperComponent, MetaTagHelperComponent>();

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.ValueProviderFactories.Add(new HttpCookieValueProviderFactory());
    //options.ModelBinderProviders.Insert(0, new DateModelBinderProvider());
    options.Filters.Add<MyLogAttribute>();
    options.Filters.Add<MyAppFilterAttribute>(int.MaxValue);
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
    options.CacheProfiles.Add("MyCache", new CacheProfile { Duration = 300 });
});

builder.Services.AddScoped<LogExceptionFilter>();

builder.Services.AddDbContext<MyContext>(options =>
    options
        .UseLazyLoadingProxies()
        .UseModel(MyContextModel.Instance)
        .UseSqlServer(
            builder.Configuration.GetConnectionString("MyContext")
        )
);

builder.Services.AddBookRepository();

builder.Services.AddSingleton<IMyService1, MyService>();
builder.Services.AddScoped<IMyService2, MyService>();
builder.Services.AddTransient<IMyService3, MyService>();

builder.Services.AddSingleton<IMessageService, MorningMessageService>();
builder.Services.AddSingleton<IMessageService, NightMessageService>();

builder.Services.AddOptions<MyAppOptions>()
    .Bind(builder.Configuration.GetSection(nameof(MyAppOptions)))
    .ValidateDataAnnotations();

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

app.UseHttpMethodOverride(new HttpMethodOverrideOptions
{
    FormFieldName = "_method"
});
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseResponseCaching();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
