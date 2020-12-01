//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibEntitySistema
{
    using System;
    using System.Collections.Generic;
    
    public partial class productos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public productos()
        {
            this.productos_costos = new HashSet<productos_costos>();
            this.productos_precios = new HashSet<productos_precios>();
        }
    
        public string auto { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string nombre_corto { get; set; }
        public string auto_departamento { get; set; }
        public string auto_grupo { get; set; }
        public string auto_subgrupo { get; set; }
        public string auto_tasa { get; set; }
        public string auto_empaque_compra { get; set; }
        public decimal costo_proveedor { get; set; }
        public decimal costo_proveedor_und { get; set; }
        public decimal costo_importacion { get; set; }
        public decimal costo_importacion_und { get; set; }
        public decimal costo_varios { get; set; }
        public decimal costo_varios_und { get; set; }
        public decimal costo { get; set; }
        public decimal costo_und { get; set; }
        public decimal costo_promedio { get; set; }
        public decimal costo_promedio_und { get; set; }
        public decimal utilidad_1 { get; set; }
        public decimal utilidad_2 { get; set; }
        public decimal utilidad_3 { get; set; }
        public decimal utilidad_4 { get; set; }
        public decimal utilidad_pto { get; set; }
        public decimal precio_1 { get; set; }
        public decimal precio_2 { get; set; }
        public decimal precio_3 { get; set; }
        public decimal precio_4 { get; set; }
        public decimal precio_pto { get; set; }
        public string estatus_garantia { get; set; }
        public int dias_garantia { get; set; }
        public string modelo { get; set; }
        public decimal precio_sugerido { get; set; }
        public string comentarios { get; set; }
        public string referencia { get; set; }
        public int contenido_compras { get; set; }
        public string estatus { get; set; }
        public string advertencia { get; set; }
        public System.DateTime fecha_alta { get; set; }
        public System.DateTime fecha_baja { get; set; }
        public string auto_codigo_plan { get; set; }
        public string categoria { get; set; }
        public string origen { get; set; }
        public decimal alto { get; set; }
        public decimal largo { get; set; }
        public decimal ancho { get; set; }
        public decimal peso { get; set; }
        public string codigo_arancel { get; set; }
        public decimal tasa_arancel { get; set; }
        public string auto_marca { get; set; }
        public string estatus_serial { get; set; }
        public string estatus_oferta { get; set; }
        public System.DateTime inicio { get; set; }
        public System.DateTime fin { get; set; }
        public decimal precio_oferta { get; set; }
        public string estatus_web { get; set; }
        public string estatus_corte { get; set; }
        public decimal tasa { get; set; }
        public string auto_precio_1 { get; set; }
        public string auto_precio_2 { get; set; }
        public string auto_precio_3 { get; set; }
        public string auto_precio_4 { get; set; }
        public string auto_precio_pto { get; set; }
        public string memo { get; set; }
        public int contenido_1 { get; set; }
        public int contenido_2 { get; set; }
        public int contenido_3 { get; set; }
        public int contenido_4 { get; set; }
        public int contenido_pto { get; set; }
        public string corte { get; set; }
        public string estatus_pesado { get; set; }
        public string plu { get; set; }
        public string estatus_compuesto { get; set; }
        public string estatus_catalogo { get; set; }
        public string estatus_cambio { get; set; }
        public System.DateTime fecha_movimiento { get; set; }
        public System.DateTime fecha_cambio { get; set; }
        public System.DateTime fecha_ult_venta { get; set; }
        public string presentacion { get; set; }
        public string lugar { get; set; }
        public System.DateTime fecha_ult_costo { get; set; }
        public string estatus_lote { get; set; }
        public System.DateTime fecha_lote { get; set; }
        public string abc { get; set; }
        public decimal divisa { get; set; }
        public string estatus_divisa { get; set; }
        public decimal pdf_1 { get; set; }
        public decimal pdf_2 { get; set; }
        public decimal pdf_3 { get; set; }
        public decimal pdf_4 { get; set; }
        public decimal pdf_pto { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<productos_costos> productos_costos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<productos_precios> productos_precios { get; set; }
    }
}