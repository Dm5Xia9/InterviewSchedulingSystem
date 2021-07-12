using ISSystem.DbContext;
using InterviewSchedulingSystem.ViewModels.CandidateViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using ISSystem.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using InterviewSchedulingSystem.Services;
using ISSystem.DbContext.Repositories;

namespace InterviewSchedulingSystem.Controllers
{
    public class HomeController : Controller
    {
        RepositoriesUnitOfWork _repositoriesUnitOfWork;
        public HomeController(RepositoriesUnitOfWork repositoriesUnitOfWork)
        {
            _repositoriesUnitOfWork = repositoriesUnitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var candidate = _repositoriesUnitOfWork.Candidate.GetCandidateByTelephone(Request.Cookies["telephone"]);
            if (candidate != null)
                return RedirectToAction("Index", "Schedule");

            var candidateRegisterViewLogin = new AuthenticateViewModel();
            candidateRegisterViewLogin.Fill(_repositoriesUnitOfWork);

            return View(candidateRegisterViewLogin);
        }
    }
}
