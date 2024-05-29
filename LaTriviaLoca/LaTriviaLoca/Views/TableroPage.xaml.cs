using LaTriviaLoca.Model;

namespace LaTriviaLoca.Views;

public partial class TableroPage : ContentPage
{
	public TableroPage(int dificultad)
	{
		InitializeComponent();
        this.BindingContext = new clsTableroVM(dificultad);
    }
}