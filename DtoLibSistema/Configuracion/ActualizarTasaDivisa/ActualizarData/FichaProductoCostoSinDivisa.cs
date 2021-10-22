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
        public decimal precioMonedaEnDivisaFull_1 { get; set; }
        public decimal precioMonedaEnDivisaFull_2 { get; set; }
        public decimal precioMonedaEnDivisaFull_3 { get; set; }
        public decimal precioMonedaEnDivisaFull_4 { get; set; }
        public decimal precioMonedaEnDivisaFull_5 { get; set; }
        public decimal precioMonedaEnDivisaFull_May_1 { get; set; }
        public decimal precioMonedaEnDivisaFull_May_2 { get; set; }


        public FichaProductoCostoSinDivisa()
        {
            autoPrd = "";
            costoDivisa = 0.0m;
            precioMonedaEnDivisaFull_1 = 0.0m;
            precioMonedaEnDivisaFull_2 = 0.0m;
            precioMonedaEnDivisaFull_3 = 0.0m;
            precioMonedaEnDivisaFull_4 = 0.0m;
            precioMonedaEnDivisaFull_5 = 0.0m;
            precioMonedaEnDivisaFull_May_1 = 0.0m;
            precioMonedaEnDivisaFull_May_2 = 0.0m;
        }

    }

}