using ENTITIES;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAL
{
    public class clsListadoCasillas
    {
        /// <summary>
        /// Método que recibe por param la dificultad seleccionada en el juego.
        /// Pedimos la cadena de la Uri dependiendo de la dificultad elegida.
        /// Devolvemos un listado de preguntas/respuestas que representan cada casilla. 
        /// </summary>
        /// <param name="dificultad"></param>
        /// <returns>listdo de casillas completo con la dificultad elegida</returns>
        public static async Task<List<clsCasilla>> getListadoCasillas(int dificultad)
        {
            //Pido la cadena de la Uri al método estático
            string miCadenaUrl = "";
            //Pedimos una de las tres opciones dependiendo de la dificultad
            switch (dificultad)
            {
                case 0:
                    miCadenaUrl = apiURI.Uri_Facil;
                    break;
                case 1:
                    miCadenaUrl = apiURI.Uri_Medio;
                    break;
                case 2:
                    miCadenaUrl = apiURI.Uri_Medio;
                    break;
            }
            Uri miUri = new Uri(miCadenaUrl);
            //Creamos el listado de casillas 
            List<clsCasilla> listaCasillas = new List<clsCasilla>();
            HttpClient miHttpClient;
            HttpResponseMessage miCodigoRespuesta;
            string textoJsonRespuesta;
            //Instanciamos el cliente Http
            miHttpClient = new HttpClient();
            try
            {
                miCodigoRespuesta = await miHttpClient.GetAsync(miUri);
                if (miCodigoRespuesta.IsSuccessStatusCode)
                {
                    textoJsonRespuesta = await miHttpClient.GetStringAsync(miUri);
                    miHttpClient.Dispose();
                    //JsonConvert necesita using Newtonsoft.Json;
                    //Es el paquete Nuget de Newtonsoft
                    Welcome pregunta = JsonConvert.DeserializeObject<Welcome>(textoJsonRespuesta);
                    List<Result> resultadoLista = pregunta.Results;
                    //Creamos una nueva casilla por cada resultado recibido y la añadimos al listado de casillas
                    foreach (Result resultado in resultadoLista)
                    {
                        clsCasilla casilla = new clsCasilla();
                        casilla.Pregunta = HttpUtility.HtmlDecode(resultado.Question);
                        casilla.EsCorrecta = Boolean.Parse(resultado.CorrectAnswer);
                        listaCasillas.Add(casilla);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listaCasillas;
        }
    }
}
