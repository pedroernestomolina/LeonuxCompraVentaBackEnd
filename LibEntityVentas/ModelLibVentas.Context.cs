﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class libVentasEntities : DbContext
    {
        public libVentasEntities()
            : base("name=libVentasEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ventas> ventas { get; set; }
        public DbSet<productos> productos { get; set; }
        public DbSet<productos_deposito> productos_deposito { get; set; }
        public DbSet<empresa_depositos> empresa_depositos { get; set; }
        public DbSet<productos_medida> productos_medida { get; set; }
        public DbSet<empresa_departamentos> empresa_departamentos { get; set; }
        public DbSet<productos_grupo> productos_grupo { get; set; }
        public DbSet<productos_marca> productos_marca { get; set; }
        public DbSet<empresa_cobradores> empresa_cobradores { get; set; }
        public DbSet<empresa_transporte> empresa_transporte { get; set; }
        public DbSet<vendedores> vendedores { get; set; }
        public DbSet<clientes> clientes { get; set; }
        public DbSet<empresa_medios> empresa_medios { get; set; }
        public DbSet<empresa_agencias> empresa_agencias { get; set; }
        public DbSet<cxc> cxc { get; set; }
        public DbSet<clientes_grupo> clientes_grupo { get; set; }
        public DbSet<clientes_zonas> clientes_zonas { get; set; }
        public DbSet<sistema_estados> sistema_estados { get; set; }
        public DbSet<sistema_contadores> sistema_contadores { get; set; }
        public DbSet<cxc_recibos> cxc_recibos { get; set; }
        public DbSet<cxc_documentos> cxc_documentos { get; set; }
        public DbSet<cxc_medio_pago> cxc_medio_pago { get; set; }
        public DbSet<productos_kardex> productos_kardex { get; set; }
        public DbSet<sistema_configuracion> sistema_configuracion { get; set; }
        public DbSet<usuarios> usuarios { get; set; }
        public DbSet<usuarios_grupo> usuarios_grupo { get; set; }
        public DbSet<productos_conceptos> productos_conceptos { get; set; }
        public DbSet<ventas_detalle> ventas_detalle { get; set; }
        public DbSet<empresa_series_fiscales> empresa_series_fiscales { get; set; }
        public DbSet<empresa_tasas> empresa_tasas { get; set; }
    }
}
