using ISSystem.DbContext;
using ISSystem.DbContext.Repositories;
using InterviewSchedulingSystem.Areas.Admin.ViewModels.RecordingsViewModels;
using InterviewSchedulingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ISSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace InterviewSchedulingSystem.Areas.Admin.Controllers
{
    public class RecordingsController : AdminController
    {
        RecordingService _recordingService;
        public RecordingsController(RecordingService recordingService, 
            RepositoriesUnitOfWork repositoriesUnitOfWork,
            UserManager<User> manager) :
            base(repositoriesUnitOfWork, manager)
        {
            _recordingService = recordingService;
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            var IsNormalRecordings = _repositoriesUnitOfWork.Recording.GetNormalRecordings();
            var IsLockedRecordings = _repositoriesUnitOfWork.Recording.GetLockedRecordings();
            var IsApprovedRecordings = _repositoriesUnitOfWork.Recording.GetApprovedRecordings();
            var IsСanceledRecordings = _repositoriesUnitOfWork.Recording.GetСanceledRecordings();

            IndexViewModel indexViewModel = 
                new IndexViewModel(IsNormalRecordings, IsLockedRecordings, 
                IsApprovedRecordings, IsСanceledRecordings);

            return View(indexViewModel);
        }

        [HttpPost]
        public ActionResult Approved(int id, bool isApproved)
        {
            var userId = _userManager.GetUserId(User);

            var recording = _repositoriesUnitOfWork.Recording.GetItemById(id);

            recording.UpdatedById = userId;
            recording.ChangeIsApproved();

            var candidate = _repositoriesUnitOfWork.Candidate.GetItemById(recording.CandidateId);
            candidate.IsNotified = false;

            _repositoriesUnitOfWork.Candidate.Update(candidate);

            _repositoriesUnitOfWork.SaveChanges();
            return Json(Url.Action(nameof(Index)));
        }

        [HttpPost]
        public ActionResult Locked(int id, bool isLocked)
        {
            var userId = _userManager.GetUserId(User);

            var recording = _repositoriesUnitOfWork.Recording.GetItemById(id);

            recording.UpdatedById = userId;
            recording.ChangeIsLocked();

            _repositoriesUnitOfWork.SaveChanges();
            return Json(Url.Action(nameof(Index)));
        }

        public ActionResult Proceed(int id)
        {
            ProceedViewModel proceedViewModel = new();
            proceedViewModel.Fill(_repositoriesUnitOfWork);
            proceedViewModel.RecordingId = id;

            return View(proceedViewModel);
        }

        [HttpPost]
        public ActionResult Proceed(ProceedViewModel proceedViewModel)
        {
            if (!ModelState.IsValid)
            {
                proceedViewModel.Fill(_repositoriesUnitOfWork);
                return View(proceedViewModel);
            }

            var record = _repositoriesUnitOfWork.Recording.GetItemById(proceedViewModel.RecordingId);

            var cand = _repositoriesUnitOfWork.Candidate.GetItemById(record.CandidateId);
            cand.VacancyId = proceedViewModel.VacancyId;

            _repositoriesUnitOfWork.Recording.Delete(record);

            _repositoriesUnitOfWork.Candidate.Update(cand);

            _repositoriesUnitOfWork.SaveChanges();

            return RedirectToAction(nameof(Index));

        }
    }
}
