using Amazon.MAUI.ViewModels;
namespace Amazon.MAUI.Views;

public partial class InventoryView : ContentPage
{
	public InventoryView()
	{
		InitializeComponent();
        BindingContext = new InventoryViewModel();
	}

    private void Cancel(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
    private void AddProduct(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Product");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as InventoryViewModel)?.Refresh();
    }

    private void EditClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryViewModel)?.Edit();
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryViewModel)?.DeleteProduct();
    }

    private void SearchClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryViewModel)?.SearchProduct();
    }

    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {

    }
}