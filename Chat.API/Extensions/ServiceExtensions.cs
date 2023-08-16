using Chat.API.BackgroundServices;
using Chat.API.SignalR.PresenceTracker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace Lab.SignalR_Chat.BE.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddBackendLayer(this IServiceCollection services)
        {
            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Chat - WebApi",
                    Description = "This Api will be responsible for overall data distribution and authorization.",
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        }, new List<string>()
                    },
                });
            });
            #endregion

            #region Cors
            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowCors",
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:4200", "https://chat-realtime-signalr.netlify.app")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod()
                                      .AllowCredentials();
                                  });
            });
            #endregion

            services.AddTransient<IPresenceTracker, PresenceTracker>();

            // signalR
            services.AddSignalR();

            // add worker background service
            services.AddHostedService<Worker>();
        }
    }
}
