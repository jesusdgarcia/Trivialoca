using CommunityToolkit.Maui.Views;

namespace LaTriviaLoca.Views;

public partial class PreguntaPopUpPage : Popup
{
	public PreguntaPopUpPage(string pregunta)
	{
		InitializeComponent();
        lblPregunta.Text = pregunta;
	}

    private void OnClickTrue(object sender, EventArgs e)
    {
        Close(true);
    }

    private void OnClickFalse(object sender, EventArgs e)
    {
        Close(false);
    }
}