using LaTriviaLoca.Views;

namespace LaTriviaLoca;

public partial class MainPage : ContentPage
{
	

	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnClick(object sender, EventArgs e)
	{
		string text = (sender as Button).Text;
		switch (text)
		{
			case "Easy":
				await Navigation.PushAsync(new TableroPage(0));
				break;
			case "Medium":
				await Navigation.PushAsync(new TableroPage(1));
				break;
			case "Hard":
				await Navigation.PushAsync(new TableroPage(2));
				break;
		}
	}
}

