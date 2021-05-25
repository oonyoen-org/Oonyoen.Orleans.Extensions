using Orleans;
using System;

namespace Oonyoen.Orleans.ClientExtensions
{
    public class OrleansClientServiceOptions
    {
        public Action<ClientBuilder> ConfigureClientBuilder { get; set; }
    }
}
