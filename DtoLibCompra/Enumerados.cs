using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra
{

    public class Enumerados
    {

        public enum enumTipoDocumento { SinDefinir=-1, Factura=1, NotaDebito, NotaCredito, OrdenCompra, Recepcion };
        public enum enumSituacionDocumento { SinDefinir=-1, Procesado = 1, Transito };

    }

}