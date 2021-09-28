using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibSistema.ReconversionMonetaria.Actualizar
{
    
    public class ItemProv
    {

        public string autoId { get; set; }
        public string nombre { get; set; }
        public decimal anticipos { get; set; }
        public decimal debitos { get; set; }
        public decimal creditos { get; set; }
        public decimal saldo { get; set; }
        public decimal disponible { get; set; }


        public ItemProv()
        {
            autoId = "";
            nombre = "";
            anticipos = 0.0m;
            debitos = 0.0m;
            creditos = 0.0m;
            saldo = 0.0m;
            disponible = 0.0m;
        }

    }

}