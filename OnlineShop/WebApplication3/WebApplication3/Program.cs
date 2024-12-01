using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using WebApplication3;
using WebApplication3.Model;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddControllers();

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterType<ApplicationLogService>().As<ILogService>().SingleInstance();
    containerBuilder.RegisterType<ApplicationOrderService>().As<IOrderService>().InstancePerDependency();
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
    try
    {
        dbContext.Database.CanConnect();
        Console.WriteLine("Connected to database");
    } catch (Exception ex)
    {
        Console.WriteLine($"Could not connect to database. Message: {ex.Message}");
    }
}

    // Configure the HTTP request pipeline.

    app.UseAuthorization();

app.MapControllers();

app.Run();
