
using explore_.net.Helpers;
using explore_.net.Models;
using explore_.net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace explore_.net
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            _ = services.AddControllers();
            services.AddHttpClient();
            services.AddScoped<Interfaces.IHotelRepository, HotelCommands>();
            services.AddScoped<Interfaces.IUserRepository, UserCommands>();
            services.AddScoped<JwtService>();
            services.AddSingleton(Configuration.GetSection("ConnectionStrings").Get<Settings>());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hotel booking API", Version = "V1" });
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Lax; // Set the SameSite policy to Lax
                options.Secure = CookieSecurePolicy.Always; // Set the Secure policy to ensure cookies are sent only over HTTPS
                options.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always; // Set the HttpOnly policy to prevent client-side access
            });
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotel booking API"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(options =>
            {
                options.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
            });
            app.UseCookiePolicy();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}