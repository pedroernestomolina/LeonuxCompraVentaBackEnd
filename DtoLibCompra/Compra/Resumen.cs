using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Compra
{

    public class Resumen
    {

        public string Auto { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaRecepcion { get; set; }
        public string DocumentoNro { get; set; }
        public string ControlNro { get; set; }
        public string EntidadNombre { get; set; }
        public string EntidadRif { get; set; }
        public decimal Total { get; set; }
        public bool IsAnulado { get; set; }
        public bool IsMercancia { get; set; }
        public bool IsCredito { get; set; }
        public bool IsCerradoContable { get; set; }
        public Enumerados.enumTipoDocumento TipoDocumento { get; set; }
        public Enumerados.enumSituacionDocumento Situacion { get; set; }
        public string CodigoSucursal { get; set; }
        public string Notas { get; set; }
        public int Signo { get; set; }

    }

}