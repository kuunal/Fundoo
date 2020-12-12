using AutoMapper;
using BusinessLayer.Concrete;
using BusinessLayer.ImagesCloud;
using BusinessLayer.Interface;
using BusinessLayer.MSMQ;
using Caching;
using EmailService;
using Fundoo.Utilities;
using Greeting.TokenAuthentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModelLayer;
using RepositoryLayer.Concrete;
using RepositoryLayer.Interface;
using TokenAuthentication;

namespace Fundoo
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
            services.AddDbContextPool<FundooDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("ConnectionString"))
            );
            EmailConfiguration emailConfig = Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            var cacheSettings = Configuration.GetSection("CacheSettings").Get<CacheSettings>();
            services.AddSingleton(cacheSettings);
            if (cacheSettings.IsEnabled)
            {
                services.AddStackExchangeRedisCache(options => options.Configuration = cacheSettings.ConnectionString);
                services.AddSingleton<IResponseCacheService, ResponseCacheService>(); 
            }
            var cloudConfigurations = Configuration.GetSection("Cloudinary").Get<CloudConfiguration>();
            services.AddSingleton(cloudConfigurations);
            services.AddSingleton(emailConfig);
            services.AddScoped<ICloudService, CloudService>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddSingleton<ITokenManager, TokenManager>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<INotesRepository, NotesRepository>();
            services.AddScoped<INotesService, NotesService>();
            services.AddScoped<ICollaboratorRepository, CollaboratorRepository>();
            services.AddScoped<ICollaboratorService, CollaboratorService>();
            services.AddScoped<ILabelRepository, LabelRepository>();
            services.AddScoped<ILabelService, LabelService>();
            services.AddScoped<IMqServices, MsmqService>();
            services.AddControllers();
            services.AddSwagger();
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/error");

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCustomSwagger();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
