using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCajaBanco.Reporte.Movimiento.VentaPorCliente
{
    
    public class Ficha
    {

        public string entidad { get; set; }
        public string dirFiscal { get; set; }
        public string ciRif { get; set; }
        public string telefono { get; set; }
        public decimal monto { get; set; }
        public decimal montoDivisa { get; set; }
        public int signo { get; set; }
        public string sucCodigo { get; set; }
        public string sucNombre { get; set; }


        public Ficha() 
        {
            entidad = "";
            dirFiscal = "";
            ciRif = "";
            telefono = "";
            monto = 0.0m;
            montoDivisa = 0.0m;
            signo = 1;
            sucCodigo = "";
            sucNombre = "";
        }

    }

}