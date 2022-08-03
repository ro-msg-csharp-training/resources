using System.ComponentModel.DataAnnotations;

namespace OnlineOrder.Model
{
    public class Stock
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int LocationId { get; set; }
        public decimal Quantity { get; set; }

        public virtual Location Location { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
