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
    
    public partial class VentaDetalle
    {
        public long id { get; set; }
        public long idVenta { get; set; }
        public string autoProducto { get; set; }
        public string codigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public string autoDepartamento { get; set; }
        public string autoGrupo { get; set; }
        public string autoSubGrupo { get; set; }
        public string autoTasa { get; set; }
        public decimal cantidad { get; set; }
        public string empaqueDescripcion { get; set; }
        public decimal precioNeto { get; set; }
        public decimal porctDesc1 { get; set; }
        public decimal porctDesc2 { get; set; }
        public decimal porctDesc3 { get; set; }
        public decimal montoDesc1 { get; set; }
        public decimal montoDesc2 { get; set; }
        public decimal montoDesc3 { get; set; }
        public decimal costoVenta { get; set; }
        public decimal totalNeto { get; set; }
        public decimal tasaIva { get; set; }
        public decimal montoIva { get; set; }
        public decimal total { get; set; }
        public decimal precioFinal { get; set; }
        public string decimales { get; set; }
        public string tarifa { get; set; }
        public string categoria { get; set; }
        public decimal costoCompraUnd { get; set; }
        public decimal costoPromedioUnd { get; set; }
        public string notas { get; set; }
        public decimal precioSugerido { get; set; }
        public long diaEmpaqueGarantia { get; set; }
        public long empaqueContenido { get; set; }
        public decimal cantidadUnd { get; set; }
        public decimal precioUnd { get; set; }
        public decimal utilidadMonto { get; set; }
        public decimal utilidadPorct { get; set; }
        public decimal precioItem { get; set; }
        public decimal totalDescuento { get; set; }
        public string tipoIva { get; set; }
        public long esPesado { get; set; }
        public string empaqueCodigo { get; set; }
    }
}
