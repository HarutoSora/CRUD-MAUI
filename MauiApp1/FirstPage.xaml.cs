namespace MauiApp1;

public partial class FirstPage : ContentPage
{
	public FirstPage()
	{
		InitializeComponent();
	}
    private async void GoToNextPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }


    
}