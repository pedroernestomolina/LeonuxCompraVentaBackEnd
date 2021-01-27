using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Reportes.CompraDocumento
{
    
    public class Ficha
    {

        private string estatusAnulado { get; set; }
        public DateTime fecha { get; set; }
        public string documento { get; set; }
        public string control { get; set; }
        public string serieDoc { get; set; }
        public string provNombre { get; set; }
        public string provCiRif { get; set; }
        public decimal total { get; set; }
        public string tipoDoc { get; set; }
        public decimal totalDivisa { get; set; }
        public int renglones { get; set; }
        public decimal factorDoc { get; set; }
        public int signoDoc { get; set; }
        public string nombreDoc { get; set; }
        public decimal montoDscto { get; set; }
        public decimal montoCargo { get; set; }
        public bool EsAnulado { get { return estatusAnulado == "1"; } }
        
    }

}