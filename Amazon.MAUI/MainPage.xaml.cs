namespace Amazon.MAUI
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void ManageInventory_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Inventory");
        }

        private void Shop_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Shop");
        }

            
    }

}
