using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Servidor.EnviarData
{
    
    public class CxCRecibo
    {

        public DateTime Fecha { get; set; }
        public string AutoUsuario { get; set; }
        public decimal Importe { get; set; }
        public string Usuario { get; set; }
        public decimal MontoRecibido { get; set; }
        public string Cobrador { get; set; }
        public string AutoCliente { get; set; }
        public string Cliente { get; set; }
        public string CiRif { get; set; }
        public string Codigo { get; set; }
        public string EstatusAnulado { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string AutoCobrador { get; set; }
        public decimal Anticipos { get; set; }
        public decimal Cambio { get; set; }
        public string Nota { get; set; }
        public string CodigoCobrador { get; set; }
        public string AutoCxC { get; set; }
        public decimal Retenciones { get; set; }
        public decimal Descuentos { get; set; }
        public string Hora { get; set; }
        public string Cierre { get; set; }
        public decimal ImporteDivisa { get; set; }
        public decimal FactorCambio { get; set; }

    }

}