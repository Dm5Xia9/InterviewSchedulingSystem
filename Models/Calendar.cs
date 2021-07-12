using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSystem.Models
{
    public class Calendar : BaseModelWithUser
    {
        public Calendar()
        {
        }

        public Calendar(string name)
        {
            Name = name;
        }

        public string Name { get; set; }


        [ForeignKey("TemplateId")]
        public Template Template { get; set; }
        public int? TemplateId { get; set; }

        public List<Schedule> Schedules { get; set; } = new List<Schedule>();
        public List<Vacancy> Vacancies { get; set; } = new List<Vacancy>();

    }
}
