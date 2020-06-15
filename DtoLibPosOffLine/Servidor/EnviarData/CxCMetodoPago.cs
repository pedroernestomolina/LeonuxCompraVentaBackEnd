using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Servidor.EnviarData
{

    public class CxCMetodoPago
    {

        public string AutoMedioPago { get; set; }
        public string AutoAgencia { get; set; }
        public string Medio { get; set; }
        public string Codigo { get; set; }
        public decimal MontoRecibido { get; set; }
        public DateTime Fecha { get; set; }
        public string EstatusAnulado { get; set; }
        public string Numero { get; set; }
        public string Agencia { get; set; }
        public string AutoUsuario { get; set; }
        public string Lote { get; set; }
        public string Referencia { get; set; }
        public string AutoCobrador { get; set; }
        public string Cierre { get; set; }
        public DateTime FechaAgencia { get; set; }

    }

}