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
    
    public partial class VentaPago
    {
        public long id { get; set; }
        public long idVenta { get; set; }
        public string autoMedioCobro { get; set; }
        public string codioMedioCobro { get; set; }
        public string descripMedioCobro { get; set; }
        public decimal importe { get; set; }
        public decimal montoRecibido { get; set; }
        public decimal tasa { get; set; }
        public string lote { get; set; }
        public string referencia { get; set; }
        public long tipoMedioCobro { get; set; }
    }
}
