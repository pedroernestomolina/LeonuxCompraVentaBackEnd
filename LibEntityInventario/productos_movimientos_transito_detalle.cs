//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibEntityInventario
{
    using System;
    using System.Collections.Generic;
    
    public partial class productos_movimientos_transito_detalle
    {
        public int id { get; set; }
        public int idTransito { get; set; }
        public string autoDepart { get; set; }
        public string autoGrupo { get; set; }
        public string autoTasa { get; set; }
        public string autoProd { get; set; }
        public string codigoProd { get; set; }
        public string nombreProd { get; set; }
        public string categoriaProd { get; set; }
        public decimal exFisica { get; set; }
        public int contEmpaque { get; set; }
        public string descEmpaque { get; set; }
        public string decimales { get; set; }
        public decimal costo { get; set; }
        public decimal costoUnd { get; set; }
        public decimal costoDivisa { get; set; }
        public string esAdmDivisa { get; set; }
        public string descTasa { get; set; }
        public decimal valorTasa { get; set; }
        public string fechaUltActCosto { get; set; }
        public decimal cantSolicitada { get; set; }
        public string empaqueIdSolicitado { get; set; }
        public string ajusteIdSolicitado { get; set; }
        public decimal costoSolicitado { get; set; }
        public decimal nivelMinimo { get; set; }
        public decimal nivelOptimo { get; set; }
        public decimal costoDivisaUnd { get; set; }
        public decimal exFisicaDestino { get; set; }
    
        public virtual productos_movimientos_transito productos_movimientos_transito { get; set; }
    }
}