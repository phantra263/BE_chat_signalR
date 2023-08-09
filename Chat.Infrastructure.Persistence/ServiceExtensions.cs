using Chat.Application.Interfaces.IRepositories;
using Chat.Application.Logging;
using Chat.Infrastructure.Persistence.MongoDBSetting;
using Chat.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Chat.Infrastructure.Persistence
{
    public static class ServiceExtensions
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            // config mongodb
            services.Configure<MongoDBSettings>(configuration.GetSection(nameof(MongoDBSettings)));

            // dependency
            services.AddSingleton<IMongoDBSettings>(sp => sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);

            services.AddTransient<IMemoriesLog, MemoriesLog>();
            services.AddTransient<IBookRepositoryAsync, BookRepositoryAsync>();
            services.AddTransient<IMessageRepositoryAsync, MessageRepositoryAsync>();
            services.AddTransient<IBoxRepositoryAsync, BoxRepositoryAsync>();
        }
    }
}
