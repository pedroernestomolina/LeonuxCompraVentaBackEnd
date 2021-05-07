using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento.Lista
{
    
    public class Resumen
    {

        public string autoId { get; set; }
        public DateTime fecha { get; set; }
        public string hora { get; set; }
        public string usuario { get; set; }
        public string estacion { get; set; }
        public string docNro { get; set; }
        public enumerados.EnumTipoDocumento docTipo { get; set; }
        public int docRenglones { get; set; }
        public decimal docMonto { get; set; }
        public string docSituacion { get; set; }
        public string docSucursal { get; set; }
        public string docConcepto { get; set; }
        public string docMotivo { get; set; }
        public bool isDocAnulado { get; set; }
        public string depositoOrigen { get; set; }
        public string depositoDestino { get; set; }

    }

}