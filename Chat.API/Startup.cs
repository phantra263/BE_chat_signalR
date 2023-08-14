using Chat.API.BackgroundServices;
using Chat.API.SignalR;
using Chat.Application;
using Chat.Infrastructure.Persistence;
using Lab.SignalR_Chat.BE.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Lab.SignalR_Chat.BE
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options => options.UseMemberCasing());
            services.AddPersistenceLayer(Configuration);
            services.AddApplicationLayer();
            services.AddBackendLayer();

            // signalR
            services.AddSignalR();

            // add worker background service
            services.AddHostedService<Worker>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Chat.WebApi");
            });

            app.UseCors("AllowCors");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<MessageHub>("/hub/chat");
            });
        }
    }
}
