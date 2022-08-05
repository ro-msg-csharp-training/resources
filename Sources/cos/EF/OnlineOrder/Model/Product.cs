using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineOrder.Model
{
    public class Product : ProductDto
    {
        [JsonIgnore]
        public virtual ProductCategory Category { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Stock> Stocks { get; set; }

        public  void Edit(Product product)
        {
            base.Edit(product);
            Category = product.Category;
        }

        
       

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}