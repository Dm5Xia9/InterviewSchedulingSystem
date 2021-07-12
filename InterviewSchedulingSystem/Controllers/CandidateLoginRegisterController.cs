using AutoMapper;
using ISSystem.DbContext;
using ISSystem.DbContext.Repositories;
using InterviewSchedulingSystem.Services;
using InterviewSchedulingSystem.ViewModels.CandidateViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ISSystem.Models;
using System;
using System.Threading.Tasks;
using InterviewSchedulingSystem.Helpers;

namespace InterviewSchedulingSystem.Controllers
{
    public class CandidateLoginRegisterController : Controller
    {

        IMapper _mapper;

        RepositoriesUnitOfWork _repositoriesUnitOfWork;

        public CandidateLoginRegisterController( IMapper mapper, RepositoriesUnitOfWork repositoriesUnitOfWork)
        {
            _mapper = mapper;
            _repositoriesUnitOfWork = repositoriesUnitOfWork;
        }

        [HttpPost]
        public IActionResult CandatateAuthenticate(AuthenticateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            
            var сandidate = _repositoriesUnitOfWork.Candidate.GetCandidateByTelephone(model.Telephone);

            if (сandidate == null)
            {

                сandidate = _mapper.Map<AuthenticateViewModel, Candidate>(model);

                _repositoriesUnitOfWork.Candidate.Add(сandidate);

                сandidate.IsNotified = true;

                Response.Cookies.Append("telephone", model.Telephone, 
                    new CookieOptions { Expires = DateTime.Now.AddMonths(1) });
            }
            else
            {
                сandidate.IsNotified = false;

                Response.Cookies.Append("telephone", model.Telephone, 
                    new CookieOptions { Expires = DateTime.Now.AddMonths(1) });
            }

            _repositoriesUnitOfWork.SaveChanges();
            return Redirect(Url.Action("Index", "Schedule"));
        }
    }
}
