using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibSistema.ReconversionMonetaria.Data
{
    
    public class ItemPrd
    {

        public string autoId { get; set; }
        public string nombre { get; set; }
        public decimal costoDivisa { get; set; }
        public decimal costoPrv { get; set; }
        public decimal costoPrvUnd { get; set; }
        public decimal costo { get; set; }
        public decimal costoUnd { get; set; }
        public decimal costoProm { get; set; }
        public decimal costoPromUnd { get; set; }
        public decimal precio1 { get; set; }
        public decimal precio2 { get; set; }
        public decimal precio3 { get; set; }
        public decimal precio4 { get; set; }
        public decimal precio5 { get; set; }


        public ItemPrd()
        {
            autoId = "";
            nombre = "";
            costoDivisa = 0.0m;
            costo = 0.0m;
            costoUnd = 0.0m;
            costoPrv = 0.0m;
            costoPrvUnd = 0.0m;
            costoProm = 0.0m;
            costoPromUnd=0.0m;
            precio1 = 0.0m;
            precio2 = 0.0m;
            precio3 = 0.0m;
            precio4 = 0.0m;
            precio5 = 0.0m;
        }

    }

}