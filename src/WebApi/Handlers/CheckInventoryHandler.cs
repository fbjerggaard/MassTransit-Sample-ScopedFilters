using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using WebApi.Contracts;

namespace WebApi.Handlers
{
    public class CheckInventoryCommand : IRequest<CheckInventoryResponse>
    {
        public string Sku { get; init; }
    }

    public class CheckInventoryResponse
    {
        public string Sku { get; set; }
        public int QuantityOnHand { get; set; }
    }

    public class CheckInventoryHandler : IRequestHandler<CheckInventoryCommand, CheckInventoryResponse>
    {
        private readonly Token _token;
        private readonly IBus _bus;
        private readonly ILogger<CheckInventoryHandler> _logger;

        public CheckInventoryHandler(Token token, IBus bus, ILogger<CheckInventoryHandler> logger)
        {
            _token = token;
            _bus = bus;
            _logger = logger;
        }

        public async Task<CheckInventoryResponse> Handle(CheckInventoryCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Token in handler: {Token}", _token.Value);
            
            await _bus.Publish(new InventoryStatusRequested()
            {
                Sku = request.Sku,
                RequestedAt = DateTime.UtcNow
            }, cancellationToken);

            return new CheckInventoryResponse()
            {
                Sku = request.Sku,
                QuantityOnHand = new Random().Next(100)
            };
        }
    }
}