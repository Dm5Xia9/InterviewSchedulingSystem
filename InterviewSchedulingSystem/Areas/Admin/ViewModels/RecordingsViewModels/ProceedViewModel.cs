using ISSystem.DbContext;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewSchedulingSystem.Areas.Admin.ViewModels.RecordingsViewModels
{
    public class ProceedViewModel
    {
        public virtual void Fill(RepositoriesUnitOfWork repositoriesUnitOfWork)
        {
            Vacancies = new SelectList(repositoriesUnitOfWork.Vacancy.GetHiddenVacancies(), "Id", "Name");
        }

        public int RecordingId { get; set; }

        [Required]
        [Display(Name = "Выберите вакансию")]
        public int VacancyId { get; set; }

        public SelectList Vacancies { get; set; }
    }
}
