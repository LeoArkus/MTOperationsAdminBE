using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using OpAdminDomain.Accounts;
using Swashbuckle.AspNetCore.SwaggerGen;
using OpAdminApi.GrpcConnection;
using OpAdminApiBootstrap;

namespace OpAdminApi
{
    public class Startup
    {
        private const string TitleApi = "Account Api";
        private const string VersionTag = "v1";
        
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
            });
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                    });
            });

            var readAccountsQueryUrl = ConfigurationReader.ReadAccountsCommandUrl(Configuration);
            var readAccountsCommandUrl = ConfigurationReader.ReadAccountsCommandUrl(Configuration);

            services.AddScoped<IBootstrapAccounts, BootstrapAccounts>();
            services.AddScoped<ICommandCreateAccount, GrpcAccountCreate>(provider => new GrpcAccountCreate(readAccountsCommandUrl));
            
            services.AddSwaggerGen(SetSwaggerOption);

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{TitleApi} {VersionTag}"); });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
        
        private void SetSwaggerOption(SwaggerGenOptions swaggerOptions)
        {
            swaggerOptions.SwaggerDoc("v1", new OpenApiInfo { Title = TitleApi, Version = VersionTag });
            swaggerOptions.OrderActionsBy(api => api.HttpMethod);
        }
    }
}
