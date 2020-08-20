using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Configuracion
{
    
    public class Enumerados
    {

        public enum EnumPreferenciaBusqueda { SinDefinir = -1, PorCodigo = 1, PorNombre, PorReferencia };
        public enum EnumMetodoCalculoUtilidad { SinDefinir = -1, Lineal= 1, Financiero };

    }

}