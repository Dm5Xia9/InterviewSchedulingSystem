using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewSchedulingSystem.Areas.Admin.ViewModels.CalendarsViewModels
{
    public class EditTempViewModel
    {
        [Display(Name = "Шаблон")]
        public int TemplateId { get; set; }

        public SelectList SelectTemplates { get; set; }

        [Display(Name = "Количество недель для авто-заполнения")]
        public int NumberOfWeeks { get; set; }

        [Display(Name = "Перезаписать календарь")]
        public bool IsOverwrite { get; set; }

        public int Id { get; set; }
    }
}
