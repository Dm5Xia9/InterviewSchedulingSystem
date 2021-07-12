using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewSchedulingSystem.Areas.Admin.ViewModels.TemplateViewModels
{
    public class UpdateViewModel
    {
        public void Fill(List<Day> days)
        {
            Days = days;
        }

        public List<Day> Days { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class Day
    {
        public DateTime DayT { get; set; }
        public List<Time> Times { get; set; }
    }

    public class Time
    {
        public DateTime TimeD { get; set; }
    }
}
