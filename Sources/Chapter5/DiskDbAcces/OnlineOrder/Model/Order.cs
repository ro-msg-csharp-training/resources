namespace OnlineOrder.Model
{
    using System.ComponentModel.DataAnnotations;
    public class Order
    {
        public int Id { get; set; }
        public Location ShippedFrom { get; set; }
        [Required]
        public Customer Customer { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AddressCountry { get; set; }
        public string AddressCity { get; set; }
        public string AddressCounty { get; set; }
        public string AddressStreet { get; set; }

        // Navigation property
        public ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
