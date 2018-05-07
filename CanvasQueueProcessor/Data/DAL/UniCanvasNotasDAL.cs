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
        public static void Create(Entry entry)
        {
            if (entry != null)
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
                    uniCanvasNotasToAdd.Procesado = true;
                    uniCanvasNotasToAdd.Submitted_at = entry.submitted_at;

                    context.uniCanvasNotas.Add(uniCanvasNotasToAdd);

                    context.SaveChanges();
                }
            }
        }
    }
}
