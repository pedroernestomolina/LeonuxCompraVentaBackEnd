//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibEntityPos
{
    using System;
    using System.Collections.Generic;
    
    public partial class p_operador
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public p_operador()
        {
            this.p_pendiente = new HashSet<p_pendiente>();
            this.p_venta = new HashSet<p_venta>();
            this.p_resumen = new HashSet<p_resumen>();
        }
    
        public int id { get; set; }
        public string auto_usuario { get; set; }
        public string id_equipo { get; set; }
        public System.DateTime fecha_apertura { get; set; }
        public string hora_apertura { get; set; }
        public string estatus { get; set; }
        public Nullable<System.DateTime> fecha_cierre { get; set; }
        public string hora_cierre { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<p_pendiente> p_pendiente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<p_venta> p_venta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<p_resumen> p_resumen { get; set; }
    }
}
