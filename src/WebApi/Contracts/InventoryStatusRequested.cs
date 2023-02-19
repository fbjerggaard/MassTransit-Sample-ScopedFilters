using System;

namespace WebApi.Contracts
{
    public record InventoryStatusRequested
    {
        public string Sku { get; set; }
        public DateTime RequestedAt { get; set; }
    }
}