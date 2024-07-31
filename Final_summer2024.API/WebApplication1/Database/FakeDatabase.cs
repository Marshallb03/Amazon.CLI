using Amazon.Library.Models;

namespace Final_Summer2024.API.Database
{
    public static class FakeDatabase
    {
        public static int NextProductId
        {
            get
            {
                if (!Products.Any())
                {
                    return 1;
                }
                return Products.Select(p => p.Id).Max() + 1;

            }
        }

        public static List<Product> Products { get;}  = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 1.75M, Quantity = 99},
                new Product { Id = 2, Name = "Product 2", Price = 22.2M, Quantity = 5},
                new Product { Id = 3, Name = "Product 3", Price = 35M, Quantity = 15},
                new Product { Id = 4, Name = "Product 4", Price = 4.5M, Quantity = 25},
                new Product { Id = 5, Name = "Product 5", Price = 5.5M, Quantity = 35},
            };

    }
}
