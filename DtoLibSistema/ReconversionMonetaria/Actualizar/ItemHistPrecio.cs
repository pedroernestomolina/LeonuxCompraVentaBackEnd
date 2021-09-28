using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibSistema.ReconversionMonetaria.Actualizar
{
    
    public class ItemHistPrecio
    {

        public string autoPrd { get; set; }
        public string nota { get; set; }
        public string estacionEquipo { get; set; }
        public string usuario { get; set; }
        public string idPrecio { get; set; }
        public decimal precio { get; set; }


        public ItemHistPrecio()
        {
            autoPrd = "";
            nota = "";
            estacionEquipo = "";
            usuario = "";
            precio = 0.0m;
            idPrecio = "";
        }

    }

}