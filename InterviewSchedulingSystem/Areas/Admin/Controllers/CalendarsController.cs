using ISSystem.DbContext;
using InterviewSchedulingSystem.Areas.Admin.ViewModels;
using InterviewSchedulingSystem.Areas.Admin.ViewModels.CalendarsViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ISSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Diagnostics;
using InterviewSchedulingSystem.Services;
using InterviewSchedulingSystem.Helpers;
using ISSystem.DbContext.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace InterviewSchedulingSystem.Areas.Admin.Controllers
{
    public class CalendarsController : AdminController
    {
        CalendarService _calendarService;
        VacancyService _vacancyService;
        ScheduleService _scheduleService;

        public CalendarsController(CalendarService calendarService, 
            RepositoriesUnitOfWork repositoriesUnitOfWork,
            IMapper mapper, VacancyService vacancyService, 
            ScheduleService scheduleService,
            UserManager<User> manager):
            base(mapper, repositoriesUnitOfWork, manager)
        {
            _calendarService = calendarService;
            _vacancyService = vacancyService;
            _scheduleService = scheduleService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var calendars = _repositoriesUnitOfWork.Calendar.GetIncludeSchedulesList();
            IndexViewModel indexViewModel = new IndexViewModel(calendars);

            return View(indexViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var weeks = DateTimeHelper.GetWeeks();
            var vacancies = _repositoriesUnitOfWork.Vacancy.GetActiveItemList();
            var vacanciesSelectList = new SelectList(vacancies, "Id", "Name");
            CreateViewModel calendarViewModel = 
                new CreateViewModel(weeks, vacancies, vacanciesSelectList);

            return View(calendarViewModel);
        }

        [HttpPost]
        public ActionResult Create(string jsonG)
        {
            var userId = _userManager.GetUserId(User);

            JsonObjectPostCreate jsonObjectPostCreate = JsonCalendarsCreate.Deserialize(jsonG);

            var calendar = new Calendar(jsonObjectPostCreate.Name);
            calendar.CreatedById = userId;
            calendar.UpdatedById = userId;

            _repositoriesUnitOfWork.Calendar.Add(calendar);

            _repositoriesUnitOfWork.SaveChanges();

            _vacancyService.EditVacancies(jsonObjectPostCreate.Vacancies, calendar.Id, userId);
            _scheduleService.AddSchedules(jsonObjectPostCreate.DaysScheduleDict, calendar.Id, userId);


            _repositoriesUnitOfWork.SaveChanges();

            
            return Json(Url.Action(nameof(Index)));
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var userId = _userManager.GetUserId(User);

            var vacs = _repositoriesUnitOfWork.Vacancy.GetVacanciesByCalendarId(id);
            if (vacs.Count() !=  0)
                return Json(Url.Action(nameof(Index)));


            _scheduleService.DetachSchedulesByCalendarId(id, userId);
            _repositoriesUnitOfWork.SaveChanges();

            var calendar = _repositoriesUnitOfWork.Calendar.GetItemById(id);
            calendar.UpdatedById = userId;

            _repositoriesUnitOfWork.Calendar.Delete(calendar);

            _repositoriesUnitOfWork.SaveChanges();

            return Json(Url.Action(nameof(Index)));
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            Calendar calendar = _repositoriesUnitOfWork.Calendar.GetItemById(id);

            var days = DateTimeHelper.GetExtendedWeek(_repositoriesUnitOfWork.Schedule.GetSchedulesByCalendarId(calendar.Id));
            var vacanciesSelectList = new SelectList(_repositoriesUnitOfWork.Vacancy.GetActiveItemList(), "Id", "Name");
            var vacanciesId = _repositoriesUnitOfWork.Vacancy.GetVacanciesByCalendarId(calendar.Id).Select(p => p.Id).ToList();

            UpdateViewModel calendarVM = _mapper.Map<Calendar, UpdateViewModel>(calendar);
            calendarVM.Fill(days, vacanciesSelectList, vacanciesId);

            return View(calendarVM);
        }

        [HttpPost]
        public ActionResult Update(string jsonG, int id)
        {
            var userId = _userManager.GetUserId(User);

            JsonObjectPostCreate jsonObjectPostCreate = JsonCalendarsCreate.Deserialize(jsonG);

            var calendar = _repositoriesUnitOfWork.Calendar.GetItemById(id);
            calendar.Name = jsonObjectPostCreate.Name;
            calendar.UpdatedById = userId;

            _repositoriesUnitOfWork.Calendar.Update(calendar);

            _vacancyService.DetachCalendarById(calendar.Id, userId);
            _vacancyService.EditVacancies(jsonObjectPostCreate.Vacancies, calendar.Id, userId);

            _scheduleService.DetachSchedulesByCalendarId(id, userId);
            _scheduleService.AddSchedules(jsonObjectPostCreate.DaysScheduleDict, id, userId);

            _repositoriesUnitOfWork.SaveChanges();

            return Json(Url.Action(nameof(Index)));
        }

        [HttpGet]
        public ActionResult EditTemp(int id)
        {
            EditTempViewModel viewModel = new EditTempViewModel();
            viewModel.SelectTemplates = new SelectList(_repositoriesUnitOfWork.Template.GetActiveItemList(), "Id", "Name");
            viewModel.Id = id;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditTemp(EditTempViewModel model)
        {
            if(model.NumberOfWeeks < 1)
                return View(model);

            var userId = _userManager.GetUserId(User);

            var tempShedules = _repositoriesUnitOfWork.Schedule.GetSchedulesByTempId(model.TemplateId).ToList();

            if (model.IsOverwrite)
            {
                _scheduleService.DetachSchedulesByCalendarId(model.Id, userId);
                _repositoriesUnitOfWork.SaveChanges();
            }

            var shs = _scheduleService.GetActualSchedules(_repositoriesUnitOfWork.Schedule.GetSchedulesByCalendarId(model.Id));

            DateTime lastDateTime;
            if (shs.Count() == 0)
                lastDateTime = DateTime.Now;
            else
                lastDateTime = shs.Max(p => p.Date.Date);

            _scheduleService.UpdateCalendarTemp(lastDateTime, model.NumberOfWeeks, model.Id, tempShedules);

            _repositoriesUnitOfWork.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
    
}
