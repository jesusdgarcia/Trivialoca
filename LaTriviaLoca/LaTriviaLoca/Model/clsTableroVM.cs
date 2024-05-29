using BL;
using CommunityToolkit.Maui.Views;
using ENTITIES;
using LaTriviaLoca.Model.Utilidades;
using LaTriviaLoca.Views;
using Microsoft.Maui.Controls.Platform;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTriviaLoca.Model
{
    /// <summary>
    /// VM para mostrar el tablero de juego, el dado y darle funcionalidad a este.
    /// </summary>
    public class clsTableroVM : clsBaseVM
    {
        #region Atributos
        private ObservableCollection<clsCasilla> listadoCasillas;
        private ObservableCollection<clsCasilla> listadoCasillasMostrado;
        private DelegateCommand tirarDadoCommand;
        private string[] srcImgButtomArray = {"dado1.png", "dado2.png", "dado3.png", "dado4.png", "dado5.png", "dado6.png"};
        private string srcImgButtom;
        private int posicionActual = 0; //la posición inicial del jugador siempre será 0 
        private int score = 0; //la puntuación inicial del usuario
        #endregion

        #region Propiedades

        public ObservableCollection<clsCasilla> ListadoCasillaMostrado
        {
            get
            {
                return listadoCasillasMostrado;
            }
            set
            {
                listadoCasillasMostrado = value;
            }
        }
        public DelegateCommand TirarDadoCommand { 
            get
            { 
                return tirarDadoCommand; 
            }
            set 
            { 
                tirarDadoCommand = value;
            }
        }

        public string SrcImgButtom {
            get
            {
                return srcImgButtom;
            }
            set
            {
                srcImgButtom = value;
            }
        }

        public int Score {
            get
            {
                return score;
            }
            set
            {
                score = value;
            }
        }
        #endregion

        #region Constructores
        public clsTableroVM()
        {

        }

        public clsTableroVM(int dificultad)
        {
            cargarDatos(dificultad);
            tirarDadoCommand = new DelegateCommand(TirarDadoCommand_execute);
        }
        #endregion

        /// <summary>
        /// Método que muestra el tablero de juego inicial, cuyas preguntas dependen de la dificultad recibida por parámetro
        /// pre: dificultad
        /// post: creación del tablero
        /// </summary>
        /// <param name="dificultad"></param>
        private async void cargarDatos(int dificultad) {
            //pedimos el listado de casillas según la dificultad elegida
            listadoCasillas = new ObservableCollection<clsCasilla>(await clsListadoCasillasBL.getListadoCasillasCompleto(dificultad));
            //lo guardamos
            listadoCasillasMostrado = new ObservableCollection<clsCasilla>(listadoCasillas);
            //mostramos la imagen del jugador en su posición inicial
            listadoCasillasMostrado[posicionActual].SrcImgJugador = "icono_jugador.png";
            //le damos imagen a dado
            srcImgButtom = srcImgButtomArray[0];
            //notificamos los cambios
            NotifyPropertyChanged(nameof(SrcImgButtom));
            NotifyPropertyChanged(nameof(ListadoCasillaMostrado));
            //mostramos una alerta que nos indica que hay que pulsar el dado para moverte por el tablero
            await Application.Current.MainPage.ShowPopupAsync(new AlertaPopUpPage("Throw the dice to move the goose through the board."));
            await Application.Current.MainPage.ShowPopupAsync(new AlertaPopUpPage("You will gain or lose points depending on your answers!"));

        }

        #region Comandos
        /// <summary>
        /// Command que le da la funcionalidad de mover al jugador al dado
        /// Pre: ninguna
        /// Post: el jugador se mueve por el tablero según el número que salga en el dado
        /// </summary>
        private async void TirarDadoCommand_execute()
        {
            //creamos la tirada del dado
            Random rPosicion = new Random();
            int posicion = rPosicion.Next(0, 6);
            //quitamos la imagen del jugador en su posición actual
            listadoCasillasMostrado[posicionActual].SrcImgJugador = null;
            //mostramos el número que ha salido en el dado
            srcImgButtom = srcImgButtomArray[posicion];
            //le sumamos el número mostrado en el dado a la posición actual del jugadr 
            posicionActual = posicionActual + (posicion + 1);

            //si nos salimos del tablero
            if (posicionActual >= listadoCasillasMostrado.Count)
            {
                //nos quedamos en el mismo sitio
                posicionActual = posicionActual - (posicion + 1);
                listadoCasillasMostrado[posicionActual].SrcImgJugador = "icono_jugador.png";
                //mostramos una alerta que nos insta a volver a tirar
                await Application.Current.MainPage.ShowPopupAsync(new AlertaPopUpPage("You're getting out of the board! Throw the dice again to stay inside."));
                //Actualizamos el tablero y notificamos los cambios
                ActualizacionTablero(listadoCasillasMostrado);

            }
            //si llegamos a la última casilla
            else if (posicionActual == listadoCasillasMostrado.Count - 1) {
                //movemos la imagen a esa casilla
                listadoCasillasMostrado[posicionActual].SrcImgJugador = "icono_jugador.png";
                //mostramos una alerta avisando de que hemos llegado a la última casilla
                await Application.Current.MainPage.ShowPopupAsync(new AlertaPopUpPage("Last square!"));
                //Actualizamos el tablero y notificamos los cambios
                ActualizacionTablero(listadoCasillasMostrado);
                //Mostramos la pregunta
                await ShowPopupPregunta();
            }
            //en cualquier otro caso
            else
            {
                //Movemos al jugador con normalidad
                listadoCasillasMostrado[posicionActual].SrcImgJugador = "icono_jugador.png";
                //Actualizamos el tablero y notificamos los cambios
                ActualizacionTablero(listadoCasillasMostrado);
                //Mostramos la pregunta
                await ShowPopupPregunta();
            }
        }
        /// <summary>
        /// Metodo que recibe por parametro un listado de casillas
        /// Este notifica su cambio en el tablero para ver la actualizacion en vivo
        /// </summary>
        /// <param name="listadoCasillas"></param>
        private void ActualizacionTablero(ObservableCollection<clsCasilla> listadoCasillas) {
            listadoCasillasMostrado = new ObservableCollection<clsCasilla>(listadoCasillas);
            NotifyPropertyChanged(nameof(ListadoCasillaMostrado));
            NotifyPropertyChanged(nameof(SrcImgButtom));
        }

        /// <summary>
        /// Método que muestra un texto en el popup tras responder a la pregunta dependiendo de si ha acertado con su respuesta y la posición del usuario
        /// Si el usuario ha respondido a la última pregunta del tablero, muestra un mensaje avisando de ello y dando las gracias por jugar, además de su puntuación
        /// Si ha terminado el juego, vuelve a la pantalla de inicio
        /// pre: ninguna
        /// post: popup personalizado 
        /// </summary>
        /// <returns>popup personalizado</returns>
        private async Task ShowPopupPregunta()
        {
            var result = await Application.Current.MainPage.ShowPopupAsync(new PreguntaPopUpPage(listadoCasillasMostrado[posicionActual].Pregunta));
            if (result is bool boolResultado) {
                if (listadoCasillasMostrado[posicionActual].EsCorrecta == boolResultado && posicionActual != listadoCasillasMostrado.Count - 1) {
                    await Application.Current.MainPage.ShowPopupAsync(new AlertaPopUpPage("You're right! Keep going!"));
                    score++;
                } else if (listadoCasillasMostrado[posicionActual].EsCorrecta == boolResultado && posicionActual == listadoCasillasMostrado.Count - 1) {
                    await Application.Current.MainPage.ShowPopupAsync(new AlertaPopUpPage("You're right and that was the last one! Thanks for playing :)"));
                    score++;
                    await Application.Current.MainPage.ShowPopupAsync(new AlertaPopUpPage("Total Score: " + score));
                    await Shell.Current.Navigation.PopToRootAsync();
                } else if (listadoCasillasMostrado[posicionActual].EsCorrecta != boolResultado && posicionActual == listadoCasillasMostrado.Count - 1) {
                    await Application.Current.MainPage.ShowPopupAsync(new AlertaPopUpPage("Wrong, and that was the last one! Thanks for playing :)"));
                    score--;
                    await Application.Current.MainPage.ShowPopupAsync(new AlertaPopUpPage("Total Score: " + score));
                    await Shell.Current.Navigation.PopToRootAsync();
                }
                else
                {
                    await Application.Current.MainPage.ShowPopupAsync(new AlertaPopUpPage("Wrong! Don't worry, keep trying!"));
                    score--;
                }
            }
            NotifyPropertyChanged(nameof(Score));
        }
        #endregion


    }
}
