using System.ComponentModel.DataAnnotations.Schema;

namespace StoreDatabase.Entities
{
    [Table("OrderLineItems")]
    public class OrderLineItem
    {
        public int OrderLineItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public virtual Product? Product { get; set; } = null;
    }
}
