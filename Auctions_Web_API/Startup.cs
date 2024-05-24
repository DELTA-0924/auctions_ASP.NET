using Auction.Application.Services;
using Auction_DataAcces;
using Auction_DataAcces.Repository;
using Auctuons_core.Abstractions;
using Auctuons_core.Infastructure;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Auctions_Web_API.Extensions;
using Auctions_Web_API.Endpoints;
using Auctuons_core.Enums;
using Microsoft.AspNetCore.Builder;
using Serilog;
namespace Auctions_Web_API
{
    public class Startup
    {
        IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services) {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowEverything", // This is the open house we talked about!
                    builder =>
                    {
                        builder.AllowAnyOrigin() // Any origin is welcome...
                            .AllowAnyHeader() // With any type of headers...
                            .AllowAnyMethod(); // And any HTTP methods. Such a jolly party indeed!
                       //     .AllowCredentials();                           
                    });
            });
            services.AddMvc();
            services.AddEndpointsApiExplorer();
            var configuration = Configuration;
            services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
            services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
            services.Configure<AuthorizationOptions>(configuration.GetSection(nameof(AuthorizationOptions)));
            services.AddDbContext<AuctionDbContext>(
                options =>
                {
                    options.UseMySql(Configuration.GetConnectionString("MysqlConnection"), new MySqlServerVersion(new Version(8, 3, 0)), b => b.MigrationsAssembly("Auctions_Web_API"));
                }
                );
            services.AddApiAuthentication(services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>());
            services.AddScoped<IAuctionRepository, AuctionRepository>();
            services.AddScoped<IAuctionService, AuctionService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ImageService>();
            services.AddScoped<ITokenProvider, JwtProvider>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IRatingRepository, RatingRespository>();
            services.AddScoped<IRatingService, RatingService>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseRouting();
            app.UseCors("AllowEverything");
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always
            });
            app.UseAuthentication();
            app.UseAuthorization();                                                
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.Auth_userConfigureEndpoints();
            });            
        }
    }
}
