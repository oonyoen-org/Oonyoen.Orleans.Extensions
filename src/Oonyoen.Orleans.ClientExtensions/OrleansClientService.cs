using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Orleans;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Oonyoen.Orleans.ClientExtensions
{
    public class OrleansClientService : IHostedService, IDisposable
    {
        public IClusterClient Client { get; }

        public OrleansClientService(
            IOptions<OrleansClientServiceOptions> options)
        {
            var builder = new ClientBuilder();
            options.Value.ConfigureClientBuilder?.Invoke(builder);
            Client = builder.Build();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await Client.Connect();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Client.Close();
        }

        public void Dispose()
        {
            Client.Dispose();
        }
    }
}
