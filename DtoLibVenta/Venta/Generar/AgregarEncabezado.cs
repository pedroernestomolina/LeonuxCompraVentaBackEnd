using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibVenta.Venta
{
    
    public class AgregarEncabezado
    {

        public string AutoRemision { get; set; }

        public string Serie { get; set; }
        public string OrdenCompra { get; set; }
        public string Pedido { get; set; }
        public DateTime FechaPedido { get; set; }
        public string DepachadoPor { get; set; }
        public string DireccionDespacho { get; set; }

        public string TipoRemision { get; set; }
        public string DocumentoRemision { get; set; }

    }

}
