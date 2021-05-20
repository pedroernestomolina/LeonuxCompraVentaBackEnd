using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Documento.Lista
{
    
    public class Resumen
    {

        public string auto { get; set; }
        public DateTime fechaEmision { get; set; }
        public string documento { get; set; }
        public string control { get; set; }
        public string tipo { get; set; }
        public string tipoDocNombre { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string codigoSuc { get; set; }
        public string provNombre { get; set; }
        public string provCiRif { get; set; }
        public decimal monto { get; set; }
        public decimal montoDivisa { get; set; }
        public string situacion { get; set; }
        private string estatusAnulado { get; set; }
        public int signo { get; set; }
        public string aplica { get; set; }
        public bool esAnulado
        {
            get
            {
                return estatusAnulado == "1";
            }
        }
        public Enumerados.enumTipoDocumento tipoDoc
        {
            get
            {
                var rt = Enumerados.enumTipoDocumento.SinDefinir;
                switch (tipo.Trim().ToUpper())
                {
                    case "01":
                        rt = DtoLibCompra.Enumerados.enumTipoDocumento.Factura;
                        break;
                    case "02":
                        rt = DtoLibCompra.Enumerados.enumTipoDocumento.NotaDebito;
                        break;
                    case "03":
                        rt = DtoLibCompra.Enumerados.enumTipoDocumento.NotaCredito;
                        break;
                    case "04":
                        rt = DtoLibCompra.Enumerados.enumTipoDocumento.OrdenCompra;
                        break;
                    case "05":
                        rt = DtoLibCompra.Enumerados.enumTipoDocumento.Recepcion;
                        break;
                }
                return rt;
            }
        }

    }

}