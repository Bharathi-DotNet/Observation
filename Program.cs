using Enquiry.Web;
using Enquiry.Web.Extensions;
using Enquiry.Web.Models;
using Enquiry.Web.Services;
using Enquiry.Web.Services.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddAsymmetricAuthentication();
builder.Services.AddTransient<IDatabaseInitializer, DBInitializer>();
builder.Services.AddTransient<IClient, Client>();
builder.Services.AddTransient<IEmployee, Employee>();
builder.Services.AddTransient<IProject, Project>();
builder.Services.AddTransient<IPublication, Enquiry.Web.Services.Publication>();
builder.Services.AddTransient<IVisitor, VisitorService>();
builder.Services.AddTransient<AuthenticationService>();
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<TokenService>();
builder.Services.AddTransient<UserRepository>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddSession(opts =>
{
    opts.Cookie.IsEssential = true; // make the session cookie Essential
});

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true; // consent required
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
});

//builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Add services to the container.
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

/*builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto;
}
);*/

//builder.Services.AddAntiforgery(options => options.Cookie.Name = "X-CSRF-TOKEN-COOKIENAME");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var scope = app.Services.CreateScope();
var service = scope.ServiceProvider.GetService<IDatabaseInitializer>();
service.SeedAsync().Wait();

app.UseForwardedHeaders();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=login}/{action=Index}/{id?}");

bool InDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
if (InDocker)
{
    //For Google Cloud Services
    var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
    var url = $"http://0.0.0.0:{port}";
    app.Run(url);
}
else
    app.Run();
