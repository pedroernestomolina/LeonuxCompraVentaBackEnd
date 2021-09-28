using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibSistema.ReconversionMonetaria.Data
{
    
    public class Ficha
    {

        public List<ItemPrd> Producto { get; set; }
        public List<ItemProv> Proveedor { get; set; }
        public List<ItemSaldoPorPagar> SaldoPorPagar { get; set; }


        public Ficha() 
        {
            Producto = new List<ItemPrd>();
            Proveedor = new List<ItemProv>();
            SaldoPorPagar = new List<ItemSaldoPorPagar>();
        }

    }

}