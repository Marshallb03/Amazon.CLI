using Amazon.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Library.Services
{
    internal class ShoppingCartService
    {
        private static ShoppingCartService? instance;
        private static object instanceLock = new object();

        public IReadOnlyCollection<ShoppingCart> carts;

        public ShoppingCart Cart
        {
            get
            {
                if (carts == null || !carts.Any())
                {
                    return new ShoppingCart();
                }
                return carts.FirstOrDefault() ?? new ShoppingCart();
            }
        }

        private ShoppingCartService()
        {
        }

        public static ShoppingCartService Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ShoppingCartService();
                    }
                    return instance;
                }

            }
        }

        public ShoppingCart AddOrUpdate(ShoppingCart c)
        {

        }


        public void AddProductToCart(Product product)
        {
            if(Cart == null)
            {
                return;
            }

            var existingProduct = Cart.Contents?.FirstOrDefault(p => p.Id == product.Id);


            var inventoryProduct = InventoryServiceProxy.Current.Products.FirstOrDefault(p => p.Id == product.Id);
            if(inventoryProduct == null)
            {
                return;
            }
            inventoryProduct.qty -= product.qty;    
            if(existingProduct != null)
            {
                // update
                existingProduct.qty += product.qty;
            } else
            {
                // add
                Cart.Contents?.Add(product);
            }

        }
    }
}
