using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibSistema.ReconversionMonetaria.Actualizar
{

    public class ItemSaldoPorPagar
    {

        public string autoDoc { get; set; }
        public string docNumero { get; set; }
        public decimal importe { get; set; }
        public decimal acumulado { get; set; }
        public decimal resta { get; set; }


        public ItemSaldoPorPagar()
        {
            autoDoc = "";
            docNumero = "";
            importe = 0.0m;
            acumulado = 0.0m;
            resta = 0.0m;
        }

    }

}