using System.Text.Json;
using System.ComponentModel.DataAnnotations;

namespace OnlineOrder.Model
{
    public class Location:LocationDto
    {
        
       public Location()
        {
            Stocks = new HashSet<Stock>();
        }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public virtual ICollection<Stock> Stocks { get; set; }
    }

    public class LocationDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string? County { get; set; }
        public string? Street { get; set; }

        public string? StreetNo { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

    }
}
