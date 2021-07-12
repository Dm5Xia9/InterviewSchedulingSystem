using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSystem.Models
{
    public class Vacancy: BaseModelWithUser
    {
        public Vacancy()
        {
            IsActive = true;
        }

        public string Name { get; set; }
      
        public bool IsActive { get; set; }

        public bool IsHidden { get; set; }

        [ForeignKey("CalendarId")]
        public Calendar Calendar { get; set; }
        public int? CalendarId { get; set; }

        public List<Candidate> Candidates { get; set; }
        public List<Recording> Recordings { get; set; }
    }
}
