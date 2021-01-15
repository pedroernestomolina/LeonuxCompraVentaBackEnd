using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCajaBanco.Reporte.Movimiento.CobranzaDiaria
{
    
    public class Data
    {

        public string auto { get; set; }
        public string codSuc { get; set; }
        public string codEstacion { get; set; }
        public DateTime fecha { get; set; }
        public string hora { get; set; }
        public string reciboNro { get; set; }
        public decimal importe { get; set; }
        public string cliente { get; set; }
        public string ciRif { get; set; }
        public decimal cambio { get; set; }
        public string loteNro { get; set; }
        public string refNro { get; set; }
        public decimal montoRecibido { get; set; }
        public string medioPagoDesc { get; set; }
        public string medioPagoCod { get; set; }
        public string tipoDocumento { get; set; }
        public string documentoNro { get; set; }
        public string operacion { get; set; }

    }

}