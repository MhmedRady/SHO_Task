using Microsoft.Extensions.DependencyInjection;
using SHO_Task.Domain.BuildingBlocks;
using SHO_Task.Domain.ShippingOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHO_Task.Application.ShippingOrders
{
    public class SHONumberGeneratorFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public SHONumberGeneratorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ISHONumberGenerator GetGenerator(SHONumberGeneratorType generatorType)
        {
            return generatorType switch
            {
                SHONumberGeneratorType.OldSHONumberGenerator => _serviceProvider.GetRequiredService<OldSHONumberGenerator>(),
                SHONumberGeneratorType.NewSHONumberGenerator => _serviceProvider.GetRequiredService<NewSHONumberGenerator>(),
                _ => throw new ArgumentException($"Unknown generator type: {generatorType}")
            };
        }
    }

}
