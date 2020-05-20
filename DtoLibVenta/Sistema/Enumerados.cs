using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibVenta.Sistema
{
    
    public class Enumerados
    {

        public enum enumMetodoCalculoUtilidad { SinDefinir = -1, Lineal = 1, Financiero };
        public enum enumPreferenciaBusquedaCliente { SinDefinir = -1, Codigo = 1, Nombre, CiRif };
        public enum enumPreferenciaBusquedaProducto { SinDefinir = -1, Codigo = 1, Nombre, Referencia };

    }

}