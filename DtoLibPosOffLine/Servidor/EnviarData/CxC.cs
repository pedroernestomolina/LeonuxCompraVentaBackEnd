using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Servidor.EnviarData
{
    
    public class CxC
    {

        public decimal CCobranza { get; set; }
        public decimal CCobranzap { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoDocumento { get; set; }
        public string Documento { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string Nota { get; set; }
        public decimal Importe { get; set; }
        public decimal Acumulado { get; set; }
        public string AutoCliente { get; set; }
        public string Cliente { get; set; }
        public string CiRif { get; set; }
        public string CodigoCliente { get; set; }
        public string EstatusCancelado { get; set; }
        public decimal Resta { get; set; }
        public string EstatusAnulado { get; set; }
        public string AutoDocumento { get; set; }
        public string Numero { get; set; }
        public string AutoAgencia { get; set; }
        public string Agencia { get; set; }
        public int Signo { get; set; }
        public string AutoVendedor { get; set; }
        public decimal CDepartamento { get; set; }
        public decimal CVentas { get; set; }
        public decimal CVentasp { get; set; }
        public string Serie { get; set; }
        public decimal ImporteNeto { get; set; }
        public int Dias { get; set; }
        public decimal Castigop { get; set; }
        public decimal ImporteDivisa { get; set; }
        public decimal FactorCambio { get; set; }


        public CxC()
        {
            CCobranza = 0.0m;
            CCobranzap = 0.0m;
            Fecha = DateTime.Now;
            TipoDocumento = "";
            Documento = "";
            FechaVencimiento=DateTime.Now;
            Nota = "";
            Importe = 0.0m;
            Acumulado = 0.0m;
            AutoCliente="";
            Cliente = "";
            CiRif = "";
            CodigoCliente = "";
            EstatusCancelado = "";
            Resta = 0.0m;
            EstatusAnulado = "";
            AutoDocumento = "";
            Numero = "";
            AutoAgencia = "";
            Agencia = "";
            Signo = 1;
            AutoVendedor = "";
            CDepartamento = 0.0m;
            CVentas = 0.0m;
            CVentasp = 0.0m;
            Serie = "";
            ImporteNeto = 0.0m;
            Dias = 0;
            Castigop = 0.0m;
            ImporteDivisa = 0.0m;
            FactorCambio = 0.0m;
        }

    }

}