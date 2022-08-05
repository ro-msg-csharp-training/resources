using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineOrder.Model
{
    public class Stock: StockDto
    {




        [JsonIgnore]
        public virtual Location Location { get; set; } = null!;

        [JsonIgnore]
        public virtual Product Product { get; set; } = null!;
    }

}
