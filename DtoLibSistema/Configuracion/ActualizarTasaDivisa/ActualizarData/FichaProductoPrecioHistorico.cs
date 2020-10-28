using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibSistema.Configuracion.ActualizarTasaDivisa.ActualizarData
{
    
    public class FichaProductoPrecioHistorico
    {

        public string autoPrd { get; set; }
        public string nota { get; set; }
        public string idPrecio { get; set; }
        public decimal precio { get; set; }


        public FichaProductoPrecioHistorico()
        {
            autoPrd = "";
            nota = "";
            idPrecio = "";
            precio = 0.0m;
        }

    }

}