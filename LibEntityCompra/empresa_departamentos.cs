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
    
    public partial class empresa_departamentos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public empresa_departamentos()
        {
            this.productos = new HashSet<productos>();
        }
    
        public string auto { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public decimal comision_g { get; set; }
        public decimal comision_1 { get; set; }
        public decimal comision_2 { get; set; }
        public decimal comision_3 { get; set; }
        public decimal comision_4 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<productos> productos { get; set; }
    }
}
