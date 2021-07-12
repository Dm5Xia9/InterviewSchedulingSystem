using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSystem.Models
{
    public class Template : BaseModelWithUser
    {
        public Template()
        {
        }

        public Template(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public List<Schedule> Schedules { get; set; } = new List<Schedule>();
        public List<Calendar> Calendars { get; set; } = new List<Calendar>();
    }
}
