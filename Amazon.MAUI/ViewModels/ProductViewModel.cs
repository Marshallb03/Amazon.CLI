using Amazon.Library.DTO;
using Amazon.Library.Models;
using Amazon.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Amazon.MAUI.ViewModels
{
    public class ProductViewModel
    {

        public ICommand? EditCommand { get; private set; }
        public ICommand? DeleteCommand { get; private set; }

        // public Product? Product;
        public ProductDTO? Model { get; set; }


        private void ExecuteEdit(ProductViewModel? p)
        {
            if (p?.Model == null)
            {
                return;
            }
            Shell.Current.GoToAsync($"//Product?productId={p.Model.Id}");
        }

        private void ExecuteDelete(int? id)
        {
            if (id == null)
            {
                return;
            }

            InventoryServiceProxy.Current.Delete(id ?? 0);
        }

        public override string ToString()
        {
            if (Model == null)
            {
                return string.Empty;
            }
            return $"{Model.Id} - {Model.Name} - {Model.Price} ";
        }
        public ProductViewModel(int productId = 0)
        {
            if (productId > 0)
            {
                Model = new ProductDTO();
            }
            else
            {
               Model = InventoryServiceProxy.Current.Products
                    .FirstOrDefault(p => p.Id == productId) ?? new ProductDTO();
            }
            
        }   

        public ProductViewModel(ProductDTO model)
        {
            if (model != null)
            {
                Model = model;
            }
            else
            {
                Model = new ProductDTO();
            }
        }

        public string PriceAsString
        {
            set
            {
                if (Model == null)
                {
                    return;
                }
                if (decimal.TryParse(value, out var price))                 {
                    Model.Price = price;
                } else
                {
                    
                }

            }
        }

        public async void Add()
        {
            if (Model != null)
            {
                 Model = await InventoryServiceProxy.Current.AddOrUpdateProduct(Model);
            }
        }
        public string DisplayPrice
        {
            get
            {
                if (Model == null) { return string.Empty; }
                return $"{Model.Price:C}";
            }
        }





    }
}
