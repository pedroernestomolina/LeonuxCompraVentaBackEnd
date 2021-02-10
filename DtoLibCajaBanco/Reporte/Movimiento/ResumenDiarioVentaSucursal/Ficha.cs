using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCajaBanco.Reporte.Movimiento.ResumenDiarioVentaSucursal
{

    public class Ficha
    {

        public int cntMov { get; set; }
        public decimal montoTotal { get; set; }
        public decimal montoDivisa { get; set; }
        public string nombreSuc { get; set; }
        public string codigoSuc { get; set; }
        public DateTime fecha { get; set; }
        public string cierre { get; set; }
        public string caja { get; set; }
        public string horaI { get; set; }
        public string horaF { get; set; }
        public string docI { get; set; }
        public string docF { get; set; }
        public int signo { get; set; }
        public string tipoDoc { get; set; }

    }

}