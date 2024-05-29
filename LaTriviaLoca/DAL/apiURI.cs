using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// Llamamos a una de las 3 posibles Uri dependiendo de la dificultad de juego elegida
    /// </summary>
    public class apiURI
    {
        private static string uri_facil = "https://opentdb.com/api.php?amount=35&category=18&type=boolean";
        private static string uri_medio = "https://opentdb.com/api.php?amount=35&difficulty=easy&type=boolean";
        private static string uri_dificil = "https://opentdb.com/api.php?amount=35&difficulty=medium&type=boolean";

        public static string Uri_Facil { get { return uri_facil; } }
        public static string Uri_Medio { get { return uri_medio; } }
        public static string Uri_Dificil { get { return uri_dificil; } }

    }
}
