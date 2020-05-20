//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibEntitySqLitePosOffLine
{
    using System;
    using System.Collections.Generic;
    
    public partial class Venta
    {
        public long id { get; set; }
        public string documento { get; set; }
        public string fecha { get; set; }
        public string nombreRazonSocial { get; set; }
        public string dirFiscal { get; set; }
        public string ciRif { get; set; }
        public decimal montoExento { get; set; }
        public decimal montoBase { get; set; }
        public decimal montoImpuesto { get; set; }
        public decimal base1 { get; set; }
        public decimal base2 { get; set; }
        public decimal base3 { get; set; }
        public decimal impuesto1 { get; set; }
        public decimal impuesto2 { get; set; }
        public decimal impuesto3 { get; set; }
        public decimal montoTotal { get; set; }
        public decimal tasaIva1 { get; set; }
        public decimal tasaIva2 { get; set; }
        public decimal tasaIva3 { get; set; }
        public string mesRelacion { get; set; }
        public string control { get; set; }
        public decimal descuentoMonto1 { get; set; }
        public decimal descuentoMonto2 { get; set; }
        public decimal cargoMonto1 { get; set; }
        public decimal descuentoPorc1 { get; set; }
        public decimal descuentoPorc2 { get; set; }
        public decimal cargoPorc_1 { get; set; }
        public long estatusActivo { get; set; }
        public long tipoDocumento { get; set; }
        public string aplica { get; set; }
        public decimal montoSubTotalNeto { get; set; }
        public string telefono { get; set; }
        public decimal factorCambio { get; set; }
        public string usuario { get; set; }
        public string usuarioCodigo { get; set; }
        public string hora { get; set; }
        public decimal montoDivisa { get; set; }
        public string estacion { get; set; }
        public long renglones { get; set; }
        public string anoRelacion { get; set; }
        public string autoUsuario { get; set; }
        public long signo { get; set; }
        public string serie { get; set; }
        public decimal montoSubTotalImpuesto { get; set; }
        public decimal montoSubTotal { get; set; }
        public decimal montoVentaNeta { get; set; }
        public decimal montoCostoVenta { get; set; }
        public decimal montoUtilidad { get; set; }
        public decimal montoUtilidadPorc { get; set; }
        public string codigoSucursal { get; set; }
        public string autoDeposito { get; set; }
        public string codigoDeposito { get; set; }
        public string deposito { get; set; }
        public string autoVendedor { get; set; }
        public string codigoVendedor { get; set; }
        public string vendedor { get; set; }
        public string autoTransporte { get; set; }
        public string codigoTransporte { get; set; }
        public string transporte { get; set; }
        public string autoCobrador { get; set; }
        public string codigoCobrador { get; set; }
        public string cobrador { get; set; }
        public decimal montoRecibido { get; set; }
        public decimal cambioDar { get; set; }
        public string esCredito { get; set; }
    }
}