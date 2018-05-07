using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasQueueProcessor.Domain.DTO
{
    class EntryDTO
    {
        public Entries entries { get; set; }
    }

    public class Entries
    {
        public List<Entry> entry { get; set; }
    }

    public class Entry
    {
        public int attempt { get; set; }
        public int enrollmentId { get; set; }
        public int? points_possible { get; set; }
        public float grade { get; set; }
        public string activity_name { get; set; }
        public DateTime graded_at { get; set; }
        public DateTime? submitted_at { get; set; }
    }
}