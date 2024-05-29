using System.Drawing;

namespace ENTITIES
{
    public class clsCasilla
    {
        #region Propiedades
        private static int id_Auto = 0; //autoincrementamos el id desde 0
        private int id;
        private string pregunta;
        private string colorCasilla; //color como string, usaremos un converter más adelante
        private string srcImgJugador;
        private bool esCorrecta;
        #endregion

        #region Atributos
        public int Id
        {
            get { return id; }
            set { id = value;}
        }

        public string Pregunta
        {
            get { return pregunta; }    
            set { pregunta = value; }
        }

        public string ColorCasilla
        {
            get { return colorCasilla; }
            set { colorCasilla = value; }
        }

        public string SrcImgJugador
        {
            get { return srcImgJugador; }
            set
            {
                srcImgJugador = value;
            }
        }

        public bool EsCorrecta
        {
            get { return esCorrecta; }
            set
            {
                esCorrecta = value;
            }
        }

        #endregion

        #region Constructores
        public clsCasilla()
        {
            this.id = id_Auto++;           
            //Creamos un color random como string en formato hex           
            Random r = new Random();            
            var color = String.Format("#{0:X6}", r.Next(0x1000000));            
            this.colorCasilla = color;        
        }

            #endregion


        }
}
