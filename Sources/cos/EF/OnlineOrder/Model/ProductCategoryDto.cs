using System.ComponentModel.DataAnnotations;

namespace OnlineOrder.Model
{
    public class ProductCategoryDto
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}