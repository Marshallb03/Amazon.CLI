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
            products = new List<Product>();
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
