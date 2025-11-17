using Microsoft.EntityFrameworkCore;
using qnetwork_api.Data;
using qnetwork_api.Services;
using qnetwork_api.Services.IndustrialDevices;

namespace qnetwork_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add DB context (SQL Server)
            builder.Services.AddDbContext<QNetworkContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services
            builder.Services.AddScoped<IIndustrialSensorService, IndustrialSensorService>();
            builder.Services.AddScoped<IIndustrialActuatorService, IndustrialActuatorService>();
            builder.Services.AddScoped<IIndustrialControllerService, IndustrialControllerService>();
            builder.Services.AddScoped<INetworkService, NetworkService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<QNetworkContext>();
                db.Database.EnsureCreated();
                SeedData.Initialize(db);
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
