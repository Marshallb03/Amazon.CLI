using Amazon.Library.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Amazon.Library.Models;


namespace Amazon.MAUI.ViewModels
{
    public class ShopViewModel : INotifyPropertyChanged
    {

        public ShopViewModel()
        {
            InventoryQuery = string.Empty;
        }

        private string inventoryQuery;
        public string InventoryQuery
        {
            set
            {
                inventoryQuery = value;
                NotifyPropertyChanged();
            }
            get { return inventoryQuery; }
        }
        public List<ProductViewModel> Products
        {
            get
            {
                return InventoryServiceProxy.Current.Products.Where(p => p != null).
                    Where(p => p?.Name?.ToUpper()?.Contains(InventoryQuery.ToUpper()) ?? false).Select(p => new ProductViewModel(p)).ToList() ?? new List<ProductViewModel>();
            }
        }
        private ProductViewModel? productToBuy;

        public ProductViewModel? ProductToBuy
        {
            get => productToBuy;

            set
            {

                productToBuy = value;

                if (productToBuy != null && productToBuy.Model == null)
                {
                    productToBuy.Model = new Product();
                }
                else if (productToBuy != null && productToBuy.Model != null)
                {
                    productToBuy.Model = new Product(productToBuy.Model);
                }

                NotifyPropertyChanged();

            }
        }

        public ShoppingCart Cart {
            get
            {
                return ShoppingCartServiceProxy.Current.Cart;
            }
        }

        public List<ProductViewModel> ProductInCart
        {
            get
            {
                return ShoppingCartServiceProxy.Current?.Cart?.Contents?.Where(p => p != null).
                    Select(p => new ProductViewModel(p)).ToList() ?? new List<ProductViewModel>();
            }
        }

        public void Search()
        {
            NotifyPropertyChanged(nameof(Products));
        }
        public void Refresh()
        {
            InventoryQuery = string.Empty;
            NotifyPropertyChanged(nameof(Products));
            NotifyPropertyChanged(nameof(ProductToBuy));

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void PlaceInCart()
        {
            if ( ProductToBuy?.Model == null)
            {
                return;
            }
            //ProductToBuy.Model = new Product(ProductToBuy.Model);
            ProductToBuy.Model.Quantity = 1;
            ShoppingCartServiceProxy.Current.AddProductToCart(ProductToBuy.Model);

            ProductToBuy = null;
            NotifyPropertyChanged(nameof(ProductInCart));
            NotifyPropertyChanged(nameof(Products));

        }
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
