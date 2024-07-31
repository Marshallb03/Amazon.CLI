using Amazon.MAUI.ViewModels;
using System;

namespace Amazon.MAUI.Views;

public partial class ShopView : ContentPage
{
    public ShopView()
    {
        InitializeComponent();
        BindingContext = new ShopViewModel();
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as ShopViewModel).Refresh();
    }

    private void SearchClicked(object sender, EventArgs e)
    {
        (BindingContext as ShopViewModel).Search();
    }

    private void InCartClicked(object sender, EventArgs e)
    {
        (BindingContext as ShopViewModel).PlaceInCart();

    }
    private void AddCartClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Cart");
    }

}



