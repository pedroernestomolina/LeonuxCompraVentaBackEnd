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
    
    public partial class productos_marca
    {
        public productos_marca()
        {
            this.productos = new HashSet<productos>();
        }
    
        public string auto { get; set; }
        public string nombre { get; set; }
    
        public virtual ICollection<productos> productos { get; set; }
    }
}
