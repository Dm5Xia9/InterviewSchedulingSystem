using ISSystem.DbContext;
using Microsoft.AspNetCore.Mvc.Rendering;
using ISSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using InterviewSchedulingSystem.Services;

namespace InterviewSchedulingSystem.ViewModels.CandidateViewModels
{
    public class AuthenticateViewModel
    {
        public virtual void Fill(RepositoriesUnitOfWork repositoriesUnitOfWork)
        {
            Vacancies = new SelectList(repositoriesUnitOfWork.Vacancy.GetActiveVacanciesList(), "Id", "Name");
        }

        [Required]
        [Display(Name = "ФИО")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Телефон")]
        public string Telephone { get; set; }

        [Required]
        [Display(Name = "Вакансия")]
        public int VacancyId { get; set; }
        public SelectList Vacancies { get; set; }
    }
}
