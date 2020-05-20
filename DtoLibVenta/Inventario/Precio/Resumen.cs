using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibVenta.Inventario.Precio
{

    public class Resumen
    {

        public string Id { get; set; }
        public decimal PrecioNeto { get; set; }
        public decimal UtilidadMargen { get; set; }
        public int ContEmpVenta { get; set; }
        public string DescEmpVenta { get; set; }
        public string Decimales { get; set; }

    }

}
