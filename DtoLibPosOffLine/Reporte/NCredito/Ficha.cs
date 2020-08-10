using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Reporte.NCredito
{
    
    public class Ficha
    {

        public int id { get; set; }
        public string documento { get; set; }
        public DateTime fecha { get; set; }
        public string hora { get; set; }
        public string nombreRazaonSocial { get; set; }
        public string dirFiscal { get; set; }
        public string ciRif { get; set; }
        public string telefono { get; set; }
        public decimal monto { get; set; }
        public Enumerados.enumEstatus estatus { get; set; }
        public Enumerados.enumTipoDocumento tipoDoc { get; set; }
        public string aplica { get; set; }
        public int signo { get; set; }

    }

}