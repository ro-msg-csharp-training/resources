using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OnlineOrder.Model
{
    public class ProductDto
    { 

        [Key]
        public int Id { get; }


        public string Name { get; set; }

        public string? Description { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Weight { get; set; }

        public int CategoryId { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public virtual void Edit(ProductDto product)
        {
            if (product == null) return;
            Name = product.Name;
            Description = product.Description;
            UnitPrice = product.UnitPrice;
            Weight = product.Weight;
            

        }

    }
  
}