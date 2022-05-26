using System.Text.Json;

namespace OnlineOrder.Model
{
    using System.ComponentModel.DataAnnotations;
    public class Product
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Weight { get; set; }

        //public Supplier Supplier { get; set; }
        public ProductCategory Category { get; set; }

        public override string ToString()
        {
            return  JsonSerializer.Serialize(this);
        }

        public void Edit(Product product)
        {
            if (product == null) return;
            Name = product.Name;    
            Description = product.Description;
            UnitPrice = product.UnitPrice;
            Weight = product.Weight;
               
            
        }

    }
}