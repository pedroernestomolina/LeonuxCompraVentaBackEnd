//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibEntityVentas
{
    using System;
    using System.Collections.Generic;
    
    public partial class empresa_tasas
    {
        public empresa_tasas()
        {
            this.productos = new HashSet<productos>();
            this.ventas_detalle = new HashSet<ventas_detalle>();
        }
    
        public string auto { get; set; }
        public string nombre { get; set; }
        public decimal tasa { get; set; }
    
        public virtual ICollection<productos> productos { get; set; }
        public virtual ICollection<ventas_detalle> ventas_detalle { get; set; }
    }
}