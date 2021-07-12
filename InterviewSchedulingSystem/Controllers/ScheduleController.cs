using ISSystem.DbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using InterviewSchedulingSystem.ViewModels;
using InterviewSchedulingSystem.ViewModels.ScheduleViewModels;
using InterviewSchedulingSystem.Services;
using ISSystem.DbContext.Repositories;
using TelegramBot;

namespace InterviewSchedulingSystem.Controllers
{
    public class ScheduleController : Controller
    {

        private ScheduleService _scheduleService;
        private RecordingService _recordingService;
        private VacancyService _vacancyService;

        private RepositoriesUnitOfWork _repositoriesUnitOfWork;

        private const int _blocksCount = 5;

        private Candidate _currentCandidate
        {
            get
            {
                return _repositoriesUnitOfWork.Candidate.GetCandidateByTelephone(Request.Cookies["telephone"]);
            }
        }

        public ScheduleController(ScheduleService scheduleService, 
            RepositoriesUnitOfWork repositoriesUnitOfWork, RecordingService recordingService, 
            VacancyService vacancyService)
        {
            _scheduleService = scheduleService;
            _recordingService = recordingService;
            _vacancyService = vacancyService;

            _repositoriesUnitOfWork = repositoriesUnitOfWork;
        }
        
        [HttpGet]
        public IActionResult Index(int page = 0)
        {
            if (_currentCandidate == null)
                return RedirectToAction("Index", "Home");

            int idVacancy = _currentCandidate.VacancyId;

            var vac = _repositoriesUnitOfWork.Vacancy.GetVacancyByIdNoA(idVacancy);
            IndexViewModel indexViewModel = new IndexViewModel(_currentCandidate, vac, page);
            indexViewModel.Fill(_repositoriesUnitOfWork, _scheduleService, _blocksCount);

            ViewBag.CurrentDateTime = _recordingService.GetDateTimeByCandidateId(_currentCandidate.Id);
            ViewBag.SelectIdVacancy = idVacancy;
            ViewBag.IdVacancy = indexViewModel.Vacancy?.Id;

            return View(indexViewModel);
        }

        [HttpPost]
        public ActionResult DeleteRecordingTime(int idSchedule, DateTime selectDataTime, int idVacancy)
        {
            if (_currentCandidate == null)
                return Json(Url.Action(nameof(Index)));

            var recording = _repositoriesUnitOfWork.Recording.GetByCandidateId(_currentCandidate.Id);
            if(recording.ScheduleId != null)
            {
                var schadule = _scheduleService.ChangeTime(idSchedule, selectDataTime, isAvailable: true);
                _repositoriesUnitOfWork.Schedule.Update(schadule);
            }

            _recordingService.DeleteRecordingByCandidateId(_currentCandidate.Id);

            _currentCandidate.IsNotified = true;

            _repositoriesUnitOfWork.SaveChanges();
            return Json(Url.Action(nameof(Index)));
        }

        [HttpPost]
        public ActionResult СhangeNotified()
        {
            if (_currentCandidate == null)
                return Json(Url.Action(nameof(Index)));

            _currentCandidate.IsNotified = true;

            _repositoriesUnitOfWork.SaveChanges();
            return Json(Url.Action(nameof(Index)));
        }

        [HttpPost]
        public ActionResult SentNewTime(DateTime newDateTime, string comment)
        {
            if (_currentCandidate == null)
                return Json(Url.Action(nameof(Index)));

            if(_repositoriesUnitOfWork.Recording.IsRecordingByCandidateId(_currentCandidate.Id))
            {
                var currentRecording = _repositoriesUnitOfWork.Recording.GetByCandidateId(_currentCandidate.Id);
                if(currentRecording.ScheduleId != null)
                {
                    var lastSchedule = _scheduleService.ChangeTime((int)currentRecording.ScheduleId,
                       currentRecording.DateTime, isAvailable: true);
                    _repositoriesUnitOfWork.Schedule.Update(lastSchedule);
                }
                _repositoriesUnitOfWork.Recording.Delete(currentRecording);
            }

            var recording =
                new Recording(_currentCandidate.Id,
                newDateTime, _currentCandidate.VacancyId, comment);
            _repositoriesUnitOfWork.Recording.Add(recording);

            _repositoriesUnitOfWork.SaveChanges();

            StartBot.Send($"id<{_repositoriesUnitOfWork.Recording.GetByCandidateId(_currentCandidate.Id).Id}> " +
                $"{_currentCandidate.FullName} только что записался на собеседование по" +
                $" вакансии {_repositoriesUnitOfWork.Vacancy.GetItemById(_currentCandidate.VacancyId).Name}." +
                $" Разберитесь с этим: https://localhost:5001/admin/othertime");

            return Json(Url.Action(nameof(Index)));
        }

        [HttpPost]
        public ActionResult ChangeRecordingTime(int idSchedule, DateTime selectDataTime, int idVacancy)
        {
            if (_currentCandidate == null)
                return Json(Url.Action(nameof(Index)));

            var schadule = _scheduleService.ChangeTime(idSchedule, selectDataTime, isAvailable: false);
            _repositoriesUnitOfWork.Schedule.Update(schadule);


            if (!_repositoriesUnitOfWork.Recording.IsRecordingByCandidateId(_currentCandidate.Id))
            {
                Recording recording = new Recording(_currentCandidate.Id, selectDataTime, idSchedule, idVacancy);
                _repositoriesUnitOfWork.Recording.Add(recording);
            }
            else
            {
                var currentRecording = _repositoriesUnitOfWork.Recording.GetByCandidateId(_currentCandidate.Id);
                if(currentRecording.ScheduleId != null)
                {
                    var lastSchedule = _scheduleService.ChangeTime((int)currentRecording.ScheduleId,
                        currentRecording.DateTime, isAvailable: true);
                    _repositoriesUnitOfWork.Schedule.Update(lastSchedule);
                }

                currentRecording.DateTime = selectDataTime;
                currentRecording.ScheduleId = idSchedule;
                currentRecording.VacancyId = idVacancy;
                currentRecording.IsApproved = false;
                currentRecording.IsLocked = false;
                currentRecording.IsOtherTime = false;
            }
            _currentCandidate.IsNotified = false;
            _currentCandidate.VacancyId = idVacancy;

            _repositoriesUnitOfWork.SaveChanges();

            StartBot.Send($"id<{_repositoriesUnitOfWork.Recording.GetByCandidateId(_currentCandidate.Id).Id}>" +
                $" {_currentCandidate.FullName} только что записался на собеседование по" +
    $" вакансии {_repositoriesUnitOfWork.Vacancy.GetItemById(_currentCandidate.VacancyId).Name}." +
    $" Разберитесь с этим: https://localhost:5001/admin/recordings");
            return Json(Url.Action(nameof(Index)));
        }
    }
}
