using CanvasQueueProcessor.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasQueueProcessor.Data.DAL
{
    public class UniCanvasNotasDAL
    {
        public static bool Create(Entry entry, string tipoEvaluacion, decimal? nota)
        {
            // 
            if (entry != null && tipoEvaluacion != null)
            {
                using (var context = new dev_UniEntities())
                {
                    uniCanvasNotas uniCanvasNotasToAdd = new uniCanvasNotas();

                    uniCanvasNotasToAdd.Activity = entry.activity_name;
                    uniCanvasNotasToAdd.Attempt = entry.attempt;
                    uniCanvasNotasToAdd.EnrollmentId = entry.enrollmentId;
                    uniCanvasNotasToAdd.FechaCreacion = DateTime.Now;
                    uniCanvasNotasToAdd.Grade = (decimal)entry.grade;
                    uniCanvasNotasToAdd.Graded_at = entry.graded_at;
                    uniCanvasNotasToAdd.Points_possible = entry.points_possible;
                    uniCanvasNotasToAdd.Procesado = false;
                    uniCanvasNotasToAdd.Submitted_at = entry.submitted_at;
                    uniCanvasNotasToAdd.TipoEvaluacion = tipoEvaluacion;
                    uniCanvasNotasToAdd.Nota = nota;

                    context.uniCanvasNotas.Add(uniCanvasNotasToAdd);

                    try
                    {
                        context.SaveChanges();
                        return true;
                    }
                    catch(Exception e)
                    {
                        return false;
                    }

                }
            }
            return false;
        }
    }
}
