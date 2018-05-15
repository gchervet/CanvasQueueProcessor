using CanvasQueueProcessor.Data.DAL;
using CanvasQueueProcessor.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasQueueProcessor.Service
{
    public class EntryService
    {
        public static bool CreateNotaEntry(Entries entryList)
        {
            if (entryList.entry != null)
            {
                int entryList_Count = entryList.entry.Count();
                int uniCanvasNotas_Count = UniCanvasNotasDAL.GetRowCount();

                int uniCanvasNotas_MaxID = UniCanvasNotasDAL.GetMaxId();

                foreach (Entry entry in entryList.entry)
                {
                    // Conseguimos el tipo de evaluación
                    string[] separators = { "[", "]" };
                    string value = entry.activity_name;
                    string[] words = value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    string tipoActividadString = string.Empty;

                    if(words.Length > 1)
                    {
                        tipoActividadString = words.LastOrDefault();
                    }
                    decimal? nota = null;
                    string mensaje = null;

                    // Calculamos la nota en escala 1-10
                    if (entry.points_possible.HasValue)
                    {
                        try
                        {
                            decimal notaAverage = (decimal)((entry.grade * 100) / entry.points_possible.Value);
                            nota = notaAverage / 10;
                        }
                        catch (Exception e)
                        {
                            nota = null;
                            mensaje = e.Message;
                        }

                    }
                    UniCanvasNotasDAL.Create(entry, tipoActividadString, nota, mensaje);
                }

                int uniCanvasNotas_UpdatedCount = UniCanvasNotasDAL.GetRowCount();

                if ((uniCanvasNotas_UpdatedCount - entryList_Count) == uniCanvasNotas_Count)
                {
                    // El número de columnas agregadas a la BDD se corresponde con el número que llegó en la lista de Canvas
                    return true;
                }
                else
                {
                    // El número de columnas agregadas a la BDD no se corresponde con el número que llegó en la lista de Canvas, así que se hace rollback
                    RollBackFromId(uniCanvasNotas_MaxID);
                    return false;
                }
            }
            // Nos llegó una lista vacía
            return true;
        }

        public static void RollBackFromId(int pkID)
        {
            UniCanvasNotasDAL.RollBackFromId(pkID);
        }
    }
}
