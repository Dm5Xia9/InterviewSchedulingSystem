using InterviewSchedulingSystem.Areas.Admin.ViewModels.OtherTimeViewModels;
using InterviewSchedulingSystem.Services;
using ISSystem.DbContext;
using ISSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewSchedulingSystem.Areas.Admin.Controllers
{
    public class OtherTimeController : AdminController
    {
        RecordingService _recordingService;

        public OtherTimeController(RecordingService recordingService,
            RepositoriesUnitOfWork repositoriesUnitOfWork,
            UserManager<User> manager):
            base(repositoriesUnitOfWork, manager)
        {
            _recordingService = recordingService;
        }

        public ActionResult Index()
        {
            var IsOtherTimeRecordings = _repositoriesUnitOfWork.Recording.GetOtherTimeRecordings();
            IndexViewModel indexViewModel = new IndexViewModel(IsOtherTimeRecordings);

            return View(indexViewModel);
        }

        [HttpPost]
        public ActionResult Accept(int id)
        {
            var userId = _userManager.GetUserId(User);

            var item = _repositoriesUnitOfWork.Recording.GetItemById(id);
            item.UpdatedById = userId;
            item.ChangeIsApproved();

            _repositoriesUnitOfWork.Recording.Update(item);

            var candidate = _repositoriesUnitOfWork.Candidate.GetItemById(item.CandidateId);
            candidate.IsNotified = false;

            _repositoriesUnitOfWork.Candidate.Update(candidate);

            _repositoriesUnitOfWork.SaveChanges();

            return Json(Url.Action(nameof(Index)));
        }

        [HttpPost]
        public ActionResult Locked(int id)
        {
            var userId = _userManager.GetUserId(User);

            var item = _repositoriesUnitOfWork.Recording.GetItemById(id);
            item.UpdatedById = userId;
            item.ChangeIsLocked();

            _repositoriesUnitOfWork.Recording.Update(item);

            _repositoriesUnitOfWork.SaveChanges();

            return Json(Url.Action(nameof(Index)));
        }
    }
}
