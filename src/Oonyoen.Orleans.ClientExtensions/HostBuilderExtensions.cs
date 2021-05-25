using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans;
using System;
using System.Linq;

namespace Oonyoen.Orleans.ClientExtensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder UseOrleansClient(this IHostBuilder builder, Action<ClientBuilder> configure = null)
        {
            builder.ConfigureServices((context, services) =>
            {
                if (configure != null)
                {
                    services.Configure<OrleansClientServiceOptions>(options =>
                    {
                        options.ConfigureClientBuilder += configure;
                    });
                }

                if (!services.Any(service => service.ServiceType == typeof(OrleansClientService)))
                {
                    services.AddSingleton<OrleansClientService>();
                    services.AddSingleton<IHostedService>(sp => sp.GetRequiredService<OrleansClientService>());
                    services.AddSingleton<IClusterClient>(sp => sp.GetRequiredService<OrleansClientService>().Client);
                    services.AddSingleton<IGrainFactory>(sp => sp.GetRequiredService<OrleansClientService>().Client);
                }
            });
            return builder;
        }
    }
}
