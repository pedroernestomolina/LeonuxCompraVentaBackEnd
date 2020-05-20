using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Producto
{
    
    public class Resumen
    {

        public string Auto { get; set; }
        public string CodigoPrd { get; set; }
        public string NombrePrd { get; set; }
        public bool IsActivo { get; set; }
        public string CodigoPlu { get; set; }
        public decimal PrecioOferta { get; set; }
        public decimal PrecioRegular { get; set; }
        public DateTime? OfertaDesde { get; set; }
        public DateTime? OfertaHasta { get; set; }
        public DateTime FechaServidor { get; set; }
        public int DiasEmpaqueGarantia { get; set; }

    }

}