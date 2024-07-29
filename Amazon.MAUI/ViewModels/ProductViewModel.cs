using Amazon.Library.Models;
using Amazon.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.MAUI.ViewModels
{
    public class ProductViewModel
    {

        public override string ToString()
        {
            if (Model == null)
            {
                return string.Empty;
            }
            return $"{Model.Id} - {Model.Name} - {Model.Price} ";
        }
        public Product? Model { get; set; }

        public ProductViewModel()
        {
            Model = new Product();
        }   

        public ProductViewModel(Product model)
        {
            if (model != null)
            {
                Model = model;
            }
            else
            {
                Model = new Product();
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

        public void Add()
        {
            if (Model != null)
            {
                InventoryServiceProxy.Current.AddOrUpdateProduct(Model);
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
