using AutoMapper;
using ISSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewSchedulingSystem.ViewModels.CandidateViewModels
{
    public class AuthenticateMapping : Profile
    {
        public AuthenticateMapping()
        {
            CreateMap<AuthenticateViewModel, Candidate>();
        }
    }
}
