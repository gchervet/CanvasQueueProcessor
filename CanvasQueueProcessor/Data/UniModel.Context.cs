﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CanvasQueueProcessor.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class dev_UniEntities : DbContext
    {
        public dev_UniEntities()
            : base("name=dev_UniEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<uniCanvasNotas> uniCanvasNotas { get; set; }
        public virtual DbSet<uniCanvasColaAuditoria> uniCanvasColaAuditoria { get; set; }
        public virtual DbSet<uniCanvasNotasAuditoria> uniCanvasNotasAuditoria { get; set; }
    
        public virtual int sp_uni_canvas_delete_from_ID(Nullable<int> fromId)
        {
            var fromIdParameter = fromId.HasValue ?
                new ObjectParameter("fromId", fromId) :
                new ObjectParameter("fromId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_uni_canvas_delete_from_ID", fromIdParameter);
        }
    }
}
