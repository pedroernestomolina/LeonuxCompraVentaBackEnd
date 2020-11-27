using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Empresa.Fiscal
{
    
    public class Ficha
    {

        public decimal Tasa1 { get; set; }
        public decimal Tasa2 { get; set; }
        public decimal Tasa3 { get; set; }


        public Ficha()
        {
            Tasa1 = 0.0m;
            Tasa2 = 0.0m;
            Tasa3 = 0.0m;
        }
    
    }

}
