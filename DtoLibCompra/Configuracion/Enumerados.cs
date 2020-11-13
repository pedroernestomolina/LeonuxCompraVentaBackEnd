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

    }

}