using AutoMapper;
using Hangfire;
using InterviewSchedulingSystem.Areas.Admin.ViewModels.CandidatesViewModels;
using InterviewSchedulingSystem.Helpers;
using ISSystem.DbContext;
using ISSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewSchedulingSystem.Areas.Admin.Controllers
{
    public class CandidatesController : AdminController
    {
        public CandidatesController(RepositoriesUnitOfWork repositoriesUnitOfWork, 
            UserManager<User> manager, IMapper mapper) : 
            base(mapper, repositoriesUnitOfWork, manager)
        {

        }


        public ActionResult Index()
        {
            IndexViewModel indexViewModel = new IndexViewModel();
            indexViewModel.Fill(_repositoriesUnitOfWork);

            return View(indexViewModel);
        }

        public ActionResult Create()
        {
            CreateViewModel createViewModel = new CreateViewModel();
            createViewModel.Fill(_repositoriesUnitOfWork);

            return View(createViewModel);
        }

        [HttpPost]
        public ActionResult Create(CreateViewModel createViewModel)
        {
            if (!ModelState.IsValid)
            {
                createViewModel.Fill(_repositoriesUnitOfWork);
                return View(createViewModel);
            }
                

            var сandidate = _mapper.Map<CreateViewModel, Candidate>(createViewModel);

            if(!_repositoriesUnitOfWork.Candidate.IsUniqueTelephone(сandidate.Telephone))
            {
                createViewModel.Fill(_repositoriesUnitOfWork);
                return View(createViewModel);
            }

            сandidate.IsNotified = false;

            _repositoriesUnitOfWork.Candidate.Add(сandidate);

            _repositoriesUnitOfWork.SaveChanges();

            var link = new ISSystem.Models.AutoLink
            {
                Id = сandidate.Id,
                Link = createViewModel.Link,
            };

            _repositoriesUnitOfWork.AutoLink.Add(link);

            _repositoriesUnitOfWork.SaveChanges();

            BackgroundJob.Schedule(() =>
                    InterviewSchedulingSystem.Hangfire.AutoLink.Send(link.Id), 
                    DateTimeOffset.UtcNow.AddMonths(1));


            return RedirectToAction(nameof(Index));
        }

    }
}
