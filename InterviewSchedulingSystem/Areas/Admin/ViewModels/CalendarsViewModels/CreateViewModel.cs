using InterviewSchedulingSystem.Helpers;
using InterviewSchedulingSystem.Services;
using ISSystem.DbContext.Repositories;
using ISSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewSchedulingSystem.Areas.Admin.ViewModels.CalendarsViewModels
{
    public class CreateViewModel
    {
        public CreateViewModel(List<List<DateTime>> weeks, 
            List<Vacancy> vacancies, SelectList vacanciesSelectList)
        {
            Weeks = weeks;
            Vacancies = vacancies;
            VacanciesSelectList = vacanciesSelectList;
        }

        [Required]
        public string Name { get; set; }
        public List<List<DateTime>> Weeks { get; set; }

        public List<Vacancy> Vacancies { get; set; }
        public SelectList VacanciesSelectList { get; set; }
    }
}
