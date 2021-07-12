using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSystem.Models
{
    public class Schedule : BaseModelWithUser
    {
        public DateTime Date { get; set; }

        [Required]
        [Column(TypeName = "jsonb")]
        public TimeSchedule TimeSchedule { get; set; }

        [ForeignKey("CalendarId")]
        public Calendar Calendar { get; set; }
        public int? CalendarId { get; set; }

        [ForeignKey("TemplateId")]
        public Template Template { get; set; }
        public int? TemplateId { get; set; }

        public List<Recording> Recordings { get; set; }
    }

    public class TimeSchedule
    {
        public List<DateTimeSchedule> Times { get; set; }
    }

    public class DateTimeSchedule
    {
        public DateTime Time { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}
