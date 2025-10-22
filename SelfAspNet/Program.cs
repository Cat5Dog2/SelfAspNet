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
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.StaticFiles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddResponseCaching();

//builder.Services.AddTransient<ITagHelperComponent, MetaTagHelperComponent>();

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.ValueProviderFactories.Add(new HttpCookieValueProviderFactory());
    //options.ModelBinderProviders.Insert(0, new DateModelBinderProvider());
    // options.Filters.Add<MyLogAttribute>();
    // options.Filters.Add<MyAppFilterAttribute>(int.MaxValue);
    // options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
    // options.CacheProfiles.Add("MyCache", new CacheProfile { Duration = 300 });
}).AddSessionStateTempDataProvider();

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

//builder.Services.AddDistributedMemoryCache();
builder.Services.AddDistributedSqlServerCache(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("MyContext");
    options.SchemaName = "dbo";
    options.TableName = "MyCache";
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// builder.Services.AddSingleton<IMyService1, MyService>();
// builder.Services.AddScoped<IMyService2, MyService>();
// builder.Services.AddTransient<IMyService3, MyService>();

// builder.Services.AddSingleton<IMessageService, MorningMessageService>();
// builder.Services.AddSingleton<IMessageService, NightMessageService>();

builder.Services.AddOptions<MyAppOptions>()
    .Bind(builder.Configuration.GetSection(nameof(MyAppOptions)))
    .ValidateDataAnnotations();

builder.Services.AddOptions<ApiInfoOptions>(ApiInfoOptions.SlideShow)
    .Bind(builder.Configuration.GetSection(
        $"{nameof(ApiInfoOptions)}:{ApiInfoOptions.SlideShow}"
    ));

builder.Services.AddOptions<ApiInfoOptions>(ApiInfoOptions.OpenWeather)
    .Bind(builder.Configuration.GetSection(
        $"{nameof(ApiInfoOptions)}:{ApiInfoOptions.OpenWeather}"
    ));

// builder.Logging.ClearProviders();
// builder.Logging.AddSimpleConsole(option =>
// {
//     option.IncludeScopes = true;
//     option.TimestampFormat = "F";
//     option.ColorBehavior = LoggerColorBehavior.Enabled;
// });
// builder.Logging.AddFile(Path.Combine(builder.Environment.ContentRootPath, "Logs"));

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap.Add("isbn", typeof(IsbnRouteConstraint));
});

var app = builder.Build();

// using (var scope = app.Services.CreateScope())
// {
//     var provider = scope.ServiceProvider;
//     await Seed.Initialize(provider);
// }

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/error/catch/{0}");

app.UseHttpMethodOverride(new HttpMethodOverrideOptions
{
    FormFieldName = "_method"
});
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "MyStorage")
    ),
    RequestPath = "/storage"
});

var provider = new FileExtensionContentTypeProvider();
provider.Mappings.Add(".ace", "application/octet-stream");
provider.Mappings.Remove(".gif");

app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = staticContent =>
    {
        staticContent.Context.Response.Headers.Append(
            "Cache-Control", $"public, max-age={60 * 60 * 24 * 3}"
        );
    },
    ContentTypeProvider = provider
});

app.UseRouting();

app.UseSession();

app.UseAuthorization();
app.UseResponseCaching();

app.UseCookiePolicy(new CookiePolicyOptions
{
    CheckConsentNeeded = _ => true,
    MinimumSameSitePolicy = SameSiteMode.Lax,
    Secure = CookieSecurePolicy.Always
});

app.Use(async (context, next) =>
{
    context.Items["current"] = DateTime.Now;
    await next.Invoke();
});

// app.MapControllerRoute(
//     name: "article",
//     pattern: @"article/{aid:regex(^\d{{1,3}}$)}",
//     defaults: new
//     {
//         controller = "Route",
//         action = "Param"
//     }
// );

// app.MapControllerRoute(
//     name: "content",
//     pattern: "content/{code:isbn}",
//     defaults: new
//     {
//         controller = "Route",
//         action = "Constraint"
//     }
// );

// app.MapControllerRoute(
//     name: "limit",
//     pattern: "campaign",
//     defaults: new
//     {
//         controller = "Route",
//         action = "Limit",
//         limit = true
//     },
//     constraints: new
//     {
//         limit = new TimeLimitRouteConstraint("2025-10-01", "2025-10-10")
//     }
// );

// app.MapControllerRoute(
//     name: "area-default",
//     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
// );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
