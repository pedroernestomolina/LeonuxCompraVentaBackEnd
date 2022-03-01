using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibSistema.Configuracion.Modulo.Capturar
{
    
    public class Ficha
    {

        public string claveNivMaximo { get; set; }
        public string claveNivMedio { get; set; }
        public string claveNivMinimo { get; set; }
        public string visualizarPrdInactivos { get; set; }
        public string cantDocVisualizar { get; set; }


        public Ficha() 
        {
            claveNivMaximo = "";
            claveNivMedio = "";
            claveNivMinimo = "";
            visualizarPrdInactivos = "";
            cantDocVisualizar = "";
        }

    }

}