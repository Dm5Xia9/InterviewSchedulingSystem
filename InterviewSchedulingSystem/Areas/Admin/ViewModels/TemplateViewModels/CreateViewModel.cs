using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewSchedulingSystem.Areas.Admin.ViewModels.TemplateViewModels
{
    public class CreateViewModel
    {
        public CreateViewModel(List<DateTime> week)
        {
            Week = week;
        }

        [Required]
        public string Name { get; set; }
        public List<DateTime> Week { get; set; }
    }
}
