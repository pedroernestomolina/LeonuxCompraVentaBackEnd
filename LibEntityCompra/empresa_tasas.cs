//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibEntityCompra
{
    using System;
    using System.Collections.Generic;
    
    public partial class empresa_tasas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public empresa_tasas()
        {
            this.productos = new HashSet<productos>();
            this.compras_detalle = new HashSet<compras_detalle>();
        }
    
        public string auto { get; set; }
        public string nombre { get; set; }
        public decimal tasa { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<productos> productos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<compras_detalle> compras_detalle { get; set; }
    }
}
