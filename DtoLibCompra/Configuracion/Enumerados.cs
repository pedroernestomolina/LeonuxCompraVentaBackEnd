using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Configuracion
{
    
    public class Enumerados
    {

        public enum EnumPreferenciaBusquedaProveedor { SinDefinir = -1, PorCodigo = 1, PorNombre, Rif };
        public enum EnumPreferenciaBusquedaProducto { SinDefinir = -1, PorCodigo = 1, PorNombre, Referencia };
        public enum EnumMetodoCalculoUtilidad { SinDefinir = -1, Lineal = 1, Financiero };
        public enum EnumForzarRedondeoPrecioVenta { SinDefinir = -1, SinRedeondeo = 1, Unidad, Decena };
        public enum EnumPreferenciaRegistroPrecio { SinDefinir = -1, Neto = 1, Full };

    }

}