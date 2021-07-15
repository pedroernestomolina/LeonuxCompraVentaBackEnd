using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCajaBanco.Reporte.Analisis.VentaPorCierre
{
    
    public class Ficha
    {

        public string autoCierre { get; set; }
        public DateTime fecha { get; set; }
        public string hora { get; set; }
        public string codSucursal { get; set; }
        public string nomSucursal { get; set; }
        public int cntDoc { get; set; }
        public int cntDocFac { get; set; }
        public int cntDocNcr { get; set; }
        public decimal montoEfectivo { get; set; }
        public decimal montoDivisa { get; set; }
        public decimal montoDebito { get; set; }
        public decimal montoOtros { get; set; }
        public int cntDivisa { get; set; }
        public decimal total { get; set; }


        public Ficha()
        {
            autoCierre = "";
            fecha = DateTime.Now.Date;
            hora = "";
            codSucursal = "";
            nomSucursal = "";
            cntDoc = 0;
            cntDocFac = 0;
            cntDocNcr = 0;
            montoEfectivo = 0.0m;
            montoDivisa = 0.0m;
            montoDebito = 0.0m;
            montoOtros = 0.0m;
            cntDivisa = 0;
            total = 0.0m;
        }

    }

}