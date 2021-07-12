using ISSystem.DbContext;
using Microsoft.EntityFrameworkCore;
using ISSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISSystem.DbContext.Repositories;
using InterviewSchedulingSystem.Services;

namespace InterviewSchedulingSystem.Areas.Admin.ViewModels.VacanciesViewModels
{
    public class IndexViewModel
    {
        public IndexViewModel(List<Vacancy> normalVacancies, List<Vacancy> hiddenVacancies)
        {
            NormalVacancies = normalVacancies;
            HiddenVacancies = hiddenVacancies;
        }

        public List<Vacancy> NormalVacancies { get; set; }
        public List<Vacancy> HiddenVacancies { get; set; }
    }
}
