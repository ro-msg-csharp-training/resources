using System.Text.Json.Serialization;

namespace OnlineOrder.Model
{
    public class StockDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int LocationId { get; set; }
        public decimal Quantity { get; set; }

        


    }
}
