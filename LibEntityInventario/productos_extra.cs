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
    
    public partial class productos_extra
    {
        public string auto_productos { get; set; }
        public byte[] imagen { get; set; }
        public byte[] firma { get; set; }
    
        public virtual productos productos { get; set; }
    }
}
