using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;

namespace WebApiCors
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            // Make sure you call this before calling app.UseMvc()
            app.UseCors(
                options => options.WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowCredentials()
            );

            //app.UseMvc();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // enable cors
            services.AddCors(cors =>
            {
                cors.AddPolicy("Allow", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options => 
                options.SerializerSettings.ReferenceLoopHandling 
                = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(options => 
                options.SerializerSettings.ContractResolver 
                = new DefaultContractResolver());
        }
    }
}
