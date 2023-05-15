using Server.Models;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddControllers();
            builder.Services.AddSingleton<dBContext>();
            builder.Services.AddSingleton<IBrugerRepository, BrugerRepository>();
            builder.Services.AddSingleton<IVagtRepository, VagtRepository>();
            builder.Services.AddSingleton<IBookingRepository, BookingRepository>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("policy",
                                  policy =>
                                  {
                                      policy.AllowAnyOrigin().AllowAnyMethod()
            .AllowAnyHeader();
                                  });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();
            app.UseCors("policy");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}