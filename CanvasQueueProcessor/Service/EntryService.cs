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
            foreach (Entry entry in entryList.entry)
            {

                string[] separators = { "[", "]" };
                string value = entry.activity_name;
                string[] words = value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {

                }
            }
        }
    }
}
