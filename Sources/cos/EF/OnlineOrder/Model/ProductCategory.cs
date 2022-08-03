
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineOrder.Model


{
    public class ProductCategory : ProductCategoryDto
    {
        
        public ICollection<Product> Products { get; set; }

        public ProductCategory()
        {
            Products = new HashSet<Product>();
        }
    }

    

}