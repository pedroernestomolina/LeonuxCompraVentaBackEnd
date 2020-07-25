using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCajaBanco.Reporte.Movimiento.ArqueoCajaPos
{
    
    public class Ficha
    {

        public string autoCierre { get; set; }
        public string sucursal { get; set; }
        public string equipo { get; set; }
        public string autoUsuario { get; set; }
        public string codigoUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public DateTime fecha { get; set; }
        public string hora { get; set; }
        public decimal diferencia { get; set; }
        public decimal efectivo { get; set; }
        public decimal divisa { get; set; }
        public decimal cntdivisa { get; set; }
        public decimal tarjeta { get; set; }
        public decimal otros { get; set; }
        public decimal firma { get; set; }
        public decimal devolucion { get; set; }
        public decimal subtotal { get; set; }
        public decimal total { get; set; }
        public decimal mefectivo { get; set; }
        public decimal mdivisa { get; set; }
        public decimal mtarjeta { get; set; }
        public decimal motros { get; set; }
        public decimal msubtotal { get; set; }
        public decimal mtotal { get; set; }

    }

}