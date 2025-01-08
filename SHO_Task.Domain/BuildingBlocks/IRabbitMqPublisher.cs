using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHO_Task.Domain.BuildingBlocks
{
    public interface IRabbitMqPublisher
    {
        void Publish<T>(T message, string exchangeName, string routingKey);
    }
}
