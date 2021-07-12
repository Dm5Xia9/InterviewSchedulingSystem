using AutoMapper;
using ISSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewSchedulingSystem.Areas.Admin.ViewModels.CalendarsViewModels
{
    public class UpdateMapping : Profile
    {
        public UpdateMapping()
        {
            CreateMap<Calendar, UpdateViewModel>();
        }
    }
}
