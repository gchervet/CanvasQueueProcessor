//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class uniCanvasNotas
    {
        public long ID { get; set; }
        public Nullable<int> Attempt { get; set; }
        public Nullable<long> EnrollmentId { get; set; }
        public Nullable<decimal> Points_possible { get; set; }
        public Nullable<decimal> Grade { get; set; }
        public string Activity { get; set; }
        public Nullable<System.DateTime> Graded_at { get; set; }
        public Nullable<System.DateTime> Submitted_at { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string TipoEvaluacion { get; set; }
        public Nullable<decimal> Nota { get; set; }
        public Nullable<bool> Procesado { get; set; }
        public string Mensaje { get; set; }
    }
}
