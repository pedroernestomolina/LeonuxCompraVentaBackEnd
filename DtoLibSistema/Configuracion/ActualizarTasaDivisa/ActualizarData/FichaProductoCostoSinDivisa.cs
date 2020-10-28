using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibSistema.Configuracion.ActualizarTasaDivisa.ActualizarData
{
    
    public class FichaProductoCostoSinDivisa
    {

        public string autoPrd { get; set; }
        public decimal costoDivisa { get; set; }


        public FichaProductoCostoSinDivisa()
        {
            autoPrd = "";
            costoDivisa = 0.0m;
        }

    }

}
