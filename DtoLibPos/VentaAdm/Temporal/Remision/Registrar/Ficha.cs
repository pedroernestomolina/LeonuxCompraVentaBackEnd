using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.VentaAdm.Temporal.Remision.Registrar
{
    
    public class Ficha
    {

        public int idTemporal { get; set; }
        public string autoRemision { get; set; }
        public string documentoRemision { get; set; }
        public string tipoRemision { get; set; }
        public decimal monto { get; set; }
        public decimal montoDivisa { get; set; }
        public int renglones { get; set; }
        public List<Item.Registrar.ItemDetalle> item { get; set; }


        public Ficha() 
        {
            idTemporal = -1;
            autoRemision = "";
            documentoRemision = "";
            tipoRemision = "";
            monto = 0m;
            montoDivisa = 0m;
            renglones = 0;
            item = new List<Item.Registrar.ItemDetalle>();
        }

    }

}