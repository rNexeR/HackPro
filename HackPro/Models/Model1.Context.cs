﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HackPro.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class hackprodb_1Entities : DbContext
    {
        public hackprodb_1Entities()
            : base("name=hackprodb_1Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<tbl_calificacion> tbl_calificacion { get; set; }
        public DbSet<tbl_cat_evento> tbl_cat_evento { get; set; }
        public DbSet<tbl_comentario_evento> tbl_comentario_evento { get; set; }
        public DbSet<tbl_comentario_proyecto> tbl_comentario_proyecto { get; set; }
        public DbSet<tbl_equipo> tbl_equipo { get; set; }
        public DbSet<tbl_equipo_evento> tbl_equipo_evento { get; set; }
        public DbSet<tbl_equipo_usuario> tbl_equipo_usuario { get; set; }
        public DbSet<tbl_evento> tbl_evento { get; set; }
        public DbSet<tbl_jurado> tbl_jurado { get; set; }
        public DbSet<tbl_patrocinio> tbl_patrocinio { get; set; }
        public DbSet<tbl_patrocinio_evento> tbl_patrocinio_evento { get; set; }
        public DbSet<tbl_proyecto> tbl_proyecto { get; set; }
        public DbSet<tbl_tipo_aporte> tbl_tipo_aporte { get; set; }
        public DbSet<tbl_usuario> tbl_usuario { get; set; }
    }
}
