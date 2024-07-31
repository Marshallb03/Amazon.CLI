using Amazon.MAUI.ViewModels;

namespace Amazon.MAUI.Views;
[QueryProperty(nameof(ProductId), "productId")]




public partial class ProductView : ContentPage
{

    public int ProductId { get; set; }

    public ProductView()
	{
		InitializeComponent();
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


    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ProductViewModel(ProductId);

    }
}