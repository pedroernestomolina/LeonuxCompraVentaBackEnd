using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibVenta.Cliente
{
    
    public class Enumerados
    {

        public enum enumPreferenciaBusqueda { SinDefinir = -1, Codigo = 1, Nombre, CiRif } ;
        public enum enumTarifaPrecio { SinDefinir = -1, Tarifa_1=1, Tarifa_2, Tarifa_3, Tarifa_4 } ;
        public enum enumCategoria { SinDefinir = -1, Eventual=1, Administrativo } ;
        public enum enumDenominacionFiscal { SinDefinir = -1, NoContribuyente = 1, Contribuyente } ;
        public enum enumTipoDocumentoPorCobrar { SinDefinir=-1, Factura = 1, NotaCredito, NotaDebito } ;

    }

}