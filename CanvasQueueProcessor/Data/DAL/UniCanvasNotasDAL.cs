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
        public static int GetMaxId()
        {
            using (var context = new dev_UniEntities())
            {
                int? rtn = null;
                try
                {
                    rtn = (int?)context.uniCanvasNotas.Max(x => x.ID);
                }
                catch (Exception e)
                {
                    rtn = 0;
                }
                if (rtn == null)
                {
                    return 0;
                }
                return (int)rtn.Value;
            }
        }

        public static int GetRowCount()
        {
            using (var context = new dev_UniEntities())
            {
                return context.uniCanvasNotas.Count();
            }
        }

        public static bool Create(Entry entry, string tipoEvaluacion, decimal? nota, string mensaje)
        {
            if (entry != null && tipoEvaluacion != null)
            {
                using (var context = new dev_UniEntities())
                {
                    uniCanvasNotas uniCanvasNotasToAdd = new uniCanvasNotas();

                    uniCanvasNotasToAdd.Activity = entry.activity_name;
                    uniCanvasNotasToAdd.Attempt = entry.attempt;
                    uniCanvasNotasToAdd.EnrollmentId = entry.enrollmentId;
                    uniCanvasNotasToAdd.FechaCreacion = DateTime.Now;
                    uniCanvasNotasToAdd.Grade = entry.grade;
                    uniCanvasNotasToAdd.Graded_at = entry.graded_at;
                    uniCanvasNotasToAdd.Points_possible = entry.points_possible;
                    uniCanvasNotasToAdd.Procesado = false;
                    uniCanvasNotasToAdd.Submitted_at = entry.submitted_at;
                    uniCanvasNotasToAdd.TipoEvaluacion = tipoEvaluacion.Trim();
                    uniCanvasNotasToAdd.Nota = nota;
                    uniCanvasNotasToAdd.Mensaje = mensaje;

                    context.uniCanvasNotas.Add(uniCanvasNotasToAdd);

                    try
                    {
                        context.SaveChanges();
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }

                }
            }
            return false;
        }

        internal static void RollBackFromId(int pkID)
        {
            using (var context = new dev_UniEntities())
            {
                context.sp_uni_canvas_delete_from_ID(pkID);
            }
        }

        public static void CreateAuditoria(int cantNotas)
        {
            using (var context = new dev_UniEntities())
            {
                uniCanvasNotasAuditoria uniCanvasNotasAuditoriaToAdd = new uniCanvasNotasAuditoria();
                uniCanvasNotasAuditoriaToAdd.FechaCreacion = DateTime.Now;
                uniCanvasNotasAuditoriaToAdd.CantNotas = cantNotas;

                context.uniCanvasNotasAuditoria.Add(uniCanvasNotasAuditoriaToAdd);

                try
                {
                    context.SaveChanges();
                }
                catch(Exception e)
                {

                }
            }
        }
    }
}
