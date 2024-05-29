using DAL;
using ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class clsListadoCasillasBL
    {
        /// <summary>
        /// Conectamos con la DAL y pedimos un listado de casillas completo dependiendo de la dificultad
        /// </summary>
        /// <param name="dificultad"></param>
        /// <returns>listado casillas completo por dificultad</returns>
        public static async Task<List<clsCasilla>> getListadoCasillasCompleto(int dificultad) {
            return await clsListadoCasillas.getListadoCasillas(dificultad);        
        }
    }
}
