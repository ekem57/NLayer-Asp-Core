using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.EntityFrameworkCore;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository;
using NLayer.Repository.UnitOfWorks;
using NLayer.Service.Mapping;
using System.Reflection;
using NLayer.Web.Modules;
using FluentValidation.AspNetCore;
using NLayer.Service.Validations;
using NLayer.Web;
using NLayer.Web.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>()); 
builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddScoped(typeof(NotFoundFilter<>));

builder.Services.AddHttpClient<ProductApiService>(options =>
{
    options.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
});

builder.Services.AddHttpClient<CategoryApiService>(options =>
{
    options.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
});

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});

builder.Host.UseServiceProviderFactory
    (new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));



var app = builder.Build();
app.UseExceptionHandler("/Home/Error");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  
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
