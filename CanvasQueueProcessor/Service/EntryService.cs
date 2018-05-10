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
        public static void CreateNotaEntry(Entries entryList)
        {
            if(entryList.entry != null)
            foreach (Entry entry in entryList.entry)
            {
                // Conseguimos el tipo de evaluación
                string[] separators = { "[", "]" };
                string value = entry.activity_name;
                string[] words = value.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                decimal? nota = null;
                string mensaje = null;

                // Calculamos la nota en escala 1-10
                if(entry.points_possible.HasValue)
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


                // Primero se genera el registro en la tabla Staging uniCanvasNotas
                if (UniCanvasNotasDAL.Create(entry, words.LastOrDefault(), nota, mensaje))
                {
                    
                    foreach (var word in words)
                    {

                    }

                }
            }
        }
    }
}
