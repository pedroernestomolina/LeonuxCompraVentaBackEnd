using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCajaBanco.Reporte.Movimiento.ResumenVenta
{
    
    public class Ficha
    {

        private string auto { get; set; }
        public string usuarioNombre { get; set; }
        public string usuarioCodigo { get; set; }
        public DateTime fecha { get; set; }
        public string hora { get; set; }
        public string documento { get; set; }
        public string clienteNombre { get; set; }
        public string clienteRif { get; set; }
        public decimal total { get; set; }
        public int signo { get; set; }
        public string tipo { get; set; }
        public string serie { get; set; }
        public int renglones { get; set; }
        public string documentoNombre { get; set; }
        public decimal descuento { get; set; }
        public string condicionPago { get; set; }
        public string estacion 
        {
            get 
            {
                return auto.Substring(2, 2);
            }
        }

    }

}