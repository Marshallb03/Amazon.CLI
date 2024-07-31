using Amazon.Library.DTO;
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
        public List<ShoppingCart> Carts
        {
            get
            {
                return carts;
            }
        }

        //public ShoppingCart Cart
        //{
        //    get
        //    {
        //        if (!carts.Any())
        //        {
        //            var newCart = new ShoppingCart();
        //            carts.Add(newCart);
        //            return newCart;
        //        }
        //        return carts?.FirstOrDefault() ?? new ShoppingCart();
        //    }
        //}

        public ShoppingCart AddCart(ShoppingCart cart)
        {
            if (cart.Id == 0)
            {
                cart.Id = carts.Select(c => c.Id).Max() + 1;
            }

            carts.Add(cart);

            return cart;
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


        public void AddProductToCart(ProductDTO newProduct, int id)
        {
            var cartToUse = Carts.FirstOrDefault(c => c.Id == id);
            if (cartToUse == null || cartToUse.Contents == null)
            {
                return;
            }

            var existingProduct = cartToUse?.Contents?
                .FirstOrDefault(existingProducts => existingProducts.Id == newProduct.Id);

            var inventoryProduct = InventoryServiceProxy.Current.Products.FirstOrDefault(invProd => invProd.Id == newProduct.Id);
            if (inventoryProduct == null)
            {
                return;
            }

            inventoryProduct.Quantity -= newProduct.Quantity;

            if (existingProduct != null)
            {
                // update
                existingProduct.Quantity += newProduct.Quantity;
            }
            else
            {
                //add
                cartToUse?.Contents?.Add(newProduct);
            }

        }

        //public void CalculateTotal()
        //{
        //    if(Cart == null || Cart.Contents == null)
        //    {
        //        return;
        //    }

        //    var Total = Cart.Contents.Sum(p => p.Price * p.Quantity);
        //}
    }
}
