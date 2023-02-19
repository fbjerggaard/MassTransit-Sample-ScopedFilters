using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using WebApi.Contracts;

namespace WebApi.Consumers
{
    public class InventoryStatusRequestedConsumer : IConsumer<InventoryStatusRequested>
    {
        private readonly Token _token;
        private readonly ILogger<InventoryStatusRequestedConsumer> _logger;

        public InventoryStatusRequestedConsumer(Token token, ILogger<InventoryStatusRequestedConsumer> logger)
        {
            _token = token;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<InventoryStatusRequested> context)
        {
            if (_token.Value is null)
            {
                _logger.LogCritical("Token in consumer is null!");
            }
            else
            {
                _logger.LogInformation("Got token in consumer: {Token}", _token.Value);
            }
        }
    }
}