using InterviewSchedulingSystem.Services;
using ISSystem.DbContext;
using ISSystem.DbContext.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewSchedulingSystem.Areas.Admin.ViewModels.VacanciesViewModels
{
    public abstract class VacancyBaseViewModel
    {
        [Display(Name = "Название")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Календарь")]
        [Required]
        public int CalendarId { get; set; }
        public SelectList CalendarsSelectList { get; set; }

        public void Fill(SelectList calendarsSelectList)
        {
            CalendarsSelectList = calendarsSelectList;
        }

    }
    public class EditVacancyViewModel: VacancyBaseViewModel
    {
        public int Id { get; set; }
    }
    public class CreateVacancyViewModel : VacancyBaseViewModel
    {

    }
}
