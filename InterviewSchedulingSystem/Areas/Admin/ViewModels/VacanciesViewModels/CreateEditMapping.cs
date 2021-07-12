using AutoMapper;
using ISSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewSchedulingSystem.Areas.Admin.ViewModels.VacanciesViewModels
{
    public class CreateEditMapping : Profile
    {
        public CreateEditMapping()
        {
            CreateMap<Vacancy, EditVacancyViewModel>()
                .ReverseMap();
            //CreateMap<EditVacancyViewModel, Vacancy>();

            CreateMap<CreateVacancyViewModel, Vacancy>();
        }
    }
}
