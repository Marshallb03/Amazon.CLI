using Amazon.MAUI.ViewModels;

namespace Amazon.MAUI.Views;

public partial class ProductView : ContentPage
{
	public ProductView()
	{
		InitializeComponent();
		BindingContext = new ProductViewModel();
	}

    private void Cancel(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//Inventory");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        (BindingContext as ProductViewModel).Add();
        Shell.Current.GoToAsync("//Inventory");


    }
}