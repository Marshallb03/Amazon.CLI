﻿using Amazon.Library.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Amazon.Library.Models;
using System.Collections.ObjectModel;
using Amazon.Library.DTO;


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
                return InventoryServiceProxy.Current.Products?.Where(p => p != null).
                    Where(p => p?.Name?.ToUpper()?.Contains(InventoryQuery.ToUpper()) ?? false)
                    .Select(p => new ProductViewModel(p)).ToList() ?? new List<ProductViewModel>();
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
                    productToBuy.Model = new ProductDTO();
                }
                else if (productToBuy != null && productToBuy.Model != null)
                {
                    productToBuy.Model = new ProductDTO(productToBuy.Model);
                }

                NotifyPropertyChanged();

            }
        }

        private ShoppingCart? selectedCart;

        public ShoppingCart? SelectedCart
        {
            get
            {
                return selectedCart;
            }

            set
            {
                selectedCart = value;
                NotifyPropertyChanged(nameof(ProductsInCart));
            }
        }

        public ObservableCollection<ShoppingCart> Carts
        {
            get
            {
                return new ObservableCollection<ShoppingCart>(ShoppingCartServiceProxy.Current.Carts);
            }
        }

        public List<ProductViewModel> ProductsInCart
        {
            get
            {
                return SelectedCart?.Contents?.Where(p => p != null)
                    .Where(p => p?.Name?.ToUpper()?.Contains(InventoryQuery.ToUpper()) ?? false)
                    .Select(p => new ProductViewModel(p)).ToList()
                    ?? new List<ProductViewModel>();
            }
        }

        private decimal total;

        public decimal Total
        {
            get
            {
                return total;
            }

            set
            {
                total = value;
                NotifyPropertyChanged(nameof(TotalDisplay));
            }
        }

        public string TotalDisplay
        {
            get
            {
                return $"{Total:C}";
            }
            
        }

        private void CalculateTotal()
        {
            //Total = ShoppingCartServiceProxy.Current.CalculateTotal();
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
            if (ProductToBuy?.Model == null)
            {
                return;
            }
            //ProductToBuy.Model = new Product(ProductToBuy.Model);
            ProductToBuy.Model.Quantity = 1;
            ShoppingCartServiceProxy.Current.AddProductToCart(ProductToBuy.Model, SelectedCart.Id);

            ProductToBuy = null;
            NotifyPropertyChanged(nameof(ProductsInCart));
            NotifyPropertyChanged(nameof(Products));


            CalculateTotal();


        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

       


    }
}
