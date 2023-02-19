using MediatR;
using WebApi.Handlers;

namespace WebApi.Consumers
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using MassTransit;


    public class CheckInventoryConsumer :
        IConsumer<CheckInventory>
    {
        readonly Token _token;
        private readonly IMediator _mediator;

        public CheckInventoryConsumer(Token token, IMediator mediator)
        {
            _token = token;
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<CheckInventory> context)
        {
            if (string.IsNullOrWhiteSpace(_token.Value))
                throw new InvalidOperationException("The security token was not found");

            var cmd = new CheckInventoryCommand()
            {
                Sku = context.Message.Sku
            };

            var result = await _mediator.Send(cmd);

            await context.RespondAsync(new InventoryStatus
            {
                Sku = result.Sku,
                QuantityOnHand = result.QuantityOnHand
            });
        }
    }
}