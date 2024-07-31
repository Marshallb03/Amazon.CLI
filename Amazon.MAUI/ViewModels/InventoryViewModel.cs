using Amazon.Library.DTO;
using Amazon.Library.Models;
using Amazon.Library.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.MAUI.ViewModels
{
    public class InventoryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public string? Query { get; set; }
        public List<ProductViewModel> Products
        {
            get
            {
                return InventoryServiceProxy.Current.Products.Where(p => p != null).Select(p => new ProductViewModel(p)).ToList() ?? new List<ProductViewModel>();
            }



        }

        public ProductViewModel SelectedProduct { get; set; }

        public InventoryViewModel()
        {
        }



        public async void Refresh()
        {
            await InventoryServiceProxy.Current.Get();
            NotifyPropertyChanged("Products");
        }

        public void Edit()
        {
            Shell.Current.GoToAsync($"//Product?productId={SelectedProduct?.Model?.Id ?? 0}");
        }

        public async void DeleteProduct()
        {
            await InventoryServiceProxy.Current.Delete(SelectedProduct?.Model?.Id ?? 0);
            Refresh();
        }

        public async void SearchProduct()
        {
            await InventoryServiceProxy.Current.Search(new Query(Query));
            NotifyPropertyChanged("Products");

        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
