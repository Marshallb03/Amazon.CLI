using Amazon.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Amazon.Library.Services
{
    public class InventoryServiceProxy
    {
        private static InventoryServiceProxy? instance;
        public static object InstanceLock = new object();

        private List<Product> products;

        public ReadOnlyCollection<Product> Products
        {
            get
            {
                return products.AsReadOnly();
            }
        }

        private int nextInt
        {
            get
            {
                if(!products.Any())
                {
                    return 1;
                }
                return products.Select(p => p.Id).Max() + 1;

            }
        }

        public Product AddOrUpdateProduct(Product product)
        {
            bool isAdd = false;
            if (product.Id == 0)
            {
                isAdd = true;
                product.Id = nextInt;
            }
            if (isAdd)
            {
                products.Add(product);
            }

            return product;
           
        }

        private InventoryServiceProxy()
        {
            products = new List<Product>
            {
                new Product { Id = 0, Name = "Product 1", Description = "Description 1", Price = 1.00m, Quantity = 33 },
                new Product { Id = 1, Name = "Product 2", Description = "Description 2", Price = 2.11m, Quantity = 2 },
                new Product { Id = 2, Name = "Product 3", Description = "Description 3", Price = 3.99m, Quantity = 99 }
            };
        }

        public static InventoryServiceProxy Current
        {
            get
            {
                lock (InstanceLock)
                {
                    if (instance == null)
                    {
                        instance = new InventoryServiceProxy();
                    }
                }
                return instance;
            }
        }
    }
}
