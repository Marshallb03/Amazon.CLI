using Amazon.Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Library.Services
{
    public class ShoppingCartServiceProxy
    {
        private static ShoppingCartServiceProxy? instance;
        private static object instanceLock = new object();
        private List<ShoppingCart> carts;
        public ReadOnlyCollection<ShoppingCart> Carts
        {
            get
            {
                return carts.AsReadOnly();
            }
        }

        public ShoppingCart Cart
        {
            get
            {
                if (!carts.Any())
                {
                    var newCart = new ShoppingCart();
                    carts.Add(newCart);
                    return newCart;
                }
                return carts?.FirstOrDefault() ?? new ShoppingCart();
            }
        }

        private ShoppingCartServiceProxy()
        {
            carts = new List<ShoppingCart>();

        }

        public static ShoppingCartServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ShoppingCartServiceProxy();
                    }
                    return instance;
                }

            }
        }

        //public ShoppingCart AddOrUpdate(ShoppingCart c)
        //{
        //}


        public void AddProductToCart(Product Newproduct)
        {
            if(Cart == null || Cart.Contents == null)
            {
                return;
            }

            var existingProduct = Cart.Contents?.FirstOrDefault(existingProduct => existingProduct.Id == Newproduct.Id);


            var inventoryProduct = InventoryServiceProxy.Current.Products.FirstOrDefault(p => p.Id == Newproduct.Id);
            if(inventoryProduct == null)
            {
                return;
            }
            inventoryProduct.Quantity -= Newproduct.Quantity;    
            if(existingProduct != null)
            {
                existingProduct.Quantity += Newproduct.Quantity;
            } else
            {
                Cart?.Contents?.Add(Newproduct);
            }

        }
    }
}
