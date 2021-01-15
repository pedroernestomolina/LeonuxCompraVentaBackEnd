using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Documento.Agregar.Factura
{
    
    public class FichaDocumento
    {

        public string documentoNro { get; set; }
        public DateTime fechaDocumento { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public string nombreRazonSocialProveedor { get; set; }
        public string direccionFiscalProveedor { get; set; }
        public string ciRifProveedor { get; set; }
        public string tipoDocumento { get; set; }
        public decimal montoExento { get; set; }
        public decimal montoBase1 { get; set; }
        public decimal montoBase2 { get; set; }
        public decimal montoBase3 { get; set; }
        public decimal montoImpuesto1 { get; set; }
        public decimal montoImpuesto2 { get; set; }
        public decimal montoImpuesto3 { get; set; }
        public decimal montoBase { get; set; }
        public decimal montoImpuesto { get; set; }
        public decimal montoTotal { get; set; }
        public decimal valorTasaIva1 { get; set; }
        public decimal valorTasaIva2 { get; set; }
        public decimal valorTasaIva3 { get; set; }
        public string notaDocumento { get; set; }
        public decimal valorTasaRetencionIva { get; set; }
        public decimal valorTasaRetencionISLR { get; set; }
        public decimal montoRetencionIva { get; set; }
        public decimal montoRetencionISLR { get; set; }
        public string autoProveedor { get; set; }
        public string codigoProveedor { get; set; }
        public string mesRelacion { get; set; }
        public string controlNro { get; set; }
        public string ordenCompraNro { get; set; }
        public int diasCredito { get; set; }
        public decimal valorPorcDescuento1 { get; set; }
        public decimal valorPorcDescuento2 { get; set; }
        public decimal valorPorccargo { get; set; }
        public decimal montoDescuento1 { get; set; }
        public decimal montoDescuento2 { get; set; }
        public decimal montoCargo { get; set; }
        public string columna { get; set; }
        public string esAnulado { get; set; }
        public string aplicaDocumentoNro { get; set; }
        public string comprobanteRetencionNro { get; set; }
        public decimal subTotalNeto { get; set; }
        public string telefonoPropveedor { get; set; }
        public decimal factorCambio { get; set; }
        public string codicionPago { get; set; }
        public string usuarioNombre { get; set; }
        public string usuarioCodigo { get; set; }
        public string sucursalCodigo { get; set; }
        public decimal montoDivisa { get; set; }
        public string estacionEquipo { get; set; }
        public int cntRenglones { get; set; }
        public decimal montoSaldoPendeiente { get; set; }
        public string anoRelacion { get; set; }
        public string comprobanteRetencionISLR { get; set; }
        public int diasValidez { get; set; }
        public string usuarioAuto { get; set; }
        public string situacionDocumento { get; set; }
        public int signoDocumento { get; set; }
        public string serieDocumento { get; set; }
        public string tarifa { get; set; }
        public string tipoRemision { get; set; }
        public string documentoRemision { get; set; }
        public string autoRemision { get; set; }
        public string documentoNombre { get; set; }
        public decimal subTotalImpuesto { get; set; }
        public decimal subTotal { get; set; }
        public string tipoProveedor { get; set; }
        public string planilla { get; set; }
        public string expediente { get; set; }
        public decimal anticipoIva { get; set; }
        public decimal tercerosIva { get; set; }
        public decimal montoNeto { get; set; }
        public decimal montoCosto { get; set; }
        public decimal montoUtilidad { get; set; }
        public decimal valorPorctUtilidad { get; set; }
        public string documentoTipo { get; set; }
        public string denominacionFiscal { get; set; }
        public string autoConcepto { get; set; }
        public DateTime fechaRetencion { get; set; }
        public string estatusCierreContable { get; set; }
        public string cierreFtp { get; set; }

    }

}
