using OnlineOrder.Model;

namespace OnlineOrder.Model
{
    public static class Extensions
    {

        public static ProductCategory Create(this ProductCategoryDto productCategoryDto)
        {
            return new ProductCategory()
            {
                Description = productCategoryDto.Description,
                Id = productCategoryDto.Id,
                Name = productCategoryDto.Name
            };
        }

        public static Product Create(this ProductDto product, ProductCategory category)
        {
            return new Product
            {
                CategoryId = product.CategoryId,
                UnitPrice = product.UnitPrice,
                Category = category,
                Description = product.Description,
                Name = product.Name
            };
        }


        public static Stock Create(this StockDto stockDto)
        {
            return new Stock
            {
                LocationId = stockDto.LocationId,
                ProductId = stockDto.ProductId,
                Quantity = stockDto.Quantity
            };
          }
        public static ProductDto ToBase(this Product product)
        {
            return product;
        }

        public static async IAsyncEnumerable<T> WhereAsync<T>(this IEnumerable<T> source, Func<T, Task<bool>> predicate)
        {
            foreach (var item in source)
            {
                if (await (predicate(item)))
                {
                    yield return item;
                }
            }
        }



        public static Location Create(this LocationDto locationDto)
        {
            return new Location
            {
                City = locationDto.City,
                Country = locationDto.Country,
                County = locationDto.County,
                Id = locationDto.Id,
                Name = locationDto.Name,
                StreetNo = locationDto.StreetNo, 
                Street = locationDto.Street
            };
        }
    }
}
