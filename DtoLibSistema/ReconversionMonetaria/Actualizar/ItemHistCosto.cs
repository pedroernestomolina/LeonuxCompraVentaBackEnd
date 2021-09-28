using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibSistema.ReconversionMonetaria.Actualizar
{
    
    public class ItemHistCosto
    {

        public string autoPrd { get; set; }
        public string nota { get; set; }
        public string estacionEquipo { get; set; }
        public string usuario { get; set; }
        public decimal costo { get; set; }
        public decimal costoDivisa { get; set; }
        public decimal tasaDivisa { get; set; }
        public string serie { get; set; }
        public string documento { get; set; }


        public ItemHistCosto()
        {
            autoPrd = "";
            nota = "";
            estacionEquipo = "";
            usuario = "";
            costo = 0.0m;
            costoDivisa = 0.0m;
            tasaDivisa = 0.0m;
            serie = "";
            documento = "";
        }

    }

}