using CommunityToolkit.Maui.Views;

namespace LaTriviaLoca.Views;

public partial class AlertaPopUpPage : Popup
{
	public AlertaPopUpPage(string mensaje)
	{
		InitializeComponent();
        lblAlerta.Text = mensaje;
	}

    private void OnClickOk(object sender, EventArgs e)
    {
        Close();
    }
}