﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LeonuxPosOffLineEntities : DbContext
    {
        public LeonuxPosOffLineEntities()
            : base("name=LeonuxPosOffLineEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ProductoBarra> ProductoBarra { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<UsuarioGrupo> UsuarioGrupo { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<Fiscal> Fiscal { get; set; }
        public virtual DbSet<VentaDetalle> VentaDetalle { get; set; }
        public virtual DbSet<Deposito> Deposito { get; set; }
        public virtual DbSet<Vendedor> Vendedor { get; set; }
        public virtual DbSet<Pendiente> Pendiente { get; set; }
        public virtual DbSet<MedioCobro> MedioCobro { get; set; }
        public virtual DbSet<Cobrador> Cobrador { get; set; }
        public virtual DbSet<Transporte> Transporte { get; set; }
        public virtual DbSet<VentaPago> VentaPago { get; set; }
        public virtual DbSet<Permiso> Permiso { get; set; }
        public virtual DbSet<Sistema> Sistema { get; set; }
        public virtual DbSet<Serie> Serie { get; set; }
        public virtual DbSet<Operador> Operador { get; set; }
        public virtual DbSet<Jornada> Jornada { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<MovConceptoInv> MovConceptoInv { get; set; }
        public virtual DbSet<Empresa> Empresa { get; set; }
        public virtual DbSet<Venta> Venta { get; set; }
        public virtual DbSet<OperadorCierre> OperadorCierre { get; set; }
    }
}
