using InterviewSchedulingSystem.Areas.Admin.ViewModels.TemplateViewModels;
using InterviewSchedulingSystem.Helpers;
using InterviewSchedulingSystem.Services;
using ISSystem.DbContext;
using ISSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewSchedulingSystem.Areas.Admin.Controllers
{
    public class TemplateController : AdminController
    {
        TemplateService _templateService;
        public TemplateController(RepositoriesUnitOfWork repositories, UserManager<User> manager, 
            TemplateService templateService) : 
            base(repositories, manager)
        {
            _templateService = templateService;
        }

        public ActionResult Index()
        {
            IndexViewModel indexViewModel = new IndexViewModel(_repositoriesUnitOfWork.Template.GetActiveItemList());

            return View(indexViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var week = DateTimeHelper.GetWeek();
            CreateViewModel calendarViewModel = new CreateViewModel(week);

            return View(calendarViewModel);
        }

        [HttpPost]
        public ActionResult Create(string jsonG)
        {
            var userId = _userManager.GetUserId(User);

            var jsonObjectPostCreate = JsonCalendarsCreate.DeserializeTempl(jsonG);

            var template = new Template(jsonObjectPostCreate.Name);
            template.CreatedById = userId;
            template.UpdatedById = userId;

            _repositoriesUnitOfWork.Template.Add(template);

            _repositoriesUnitOfWork.SaveChanges();

            var shs = _templateService.GetTemplSchedules(jsonObjectPostCreate.DaysScheduleDict, template.Id, userId);

            _repositoriesUnitOfWork.Schedule.AddRange(shs);


            _repositoriesUnitOfWork.SaveChanges();

            return Json(Url.Action(nameof(Index)));
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var userId = _userManager.GetUserId(User);

            _templateService.DetachSchedulesByTempId(id, userId);
            _repositoriesUnitOfWork.SaveChanges();

            var template = _repositoriesUnitOfWork.Template.GetItemById(id);
            template.UpdatedById = userId;

            _repositoriesUnitOfWork.Template.Delete(template);

            _repositoriesUnitOfWork.SaveChanges();

            return Json(Url.Action(nameof(Index)));
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var temp = _repositoriesUnitOfWork.Template.GetItemById(id);

            var days = DateTimeHelper.GetExtWeek(_repositoriesUnitOfWork.Schedule.GetSchedulesByTempId(temp.Id));

            UpdateViewModel updateViewModel = new UpdateViewModel();
            updateViewModel.Fill(days);

            updateViewModel.Name = temp.Name;
            updateViewModel.Id = temp.Id;

            return View(updateViewModel);
        }

        [HttpPost]
        public ActionResult Update(string jsonG, int id)
        {
            var userId = _userManager.GetUserId(User);

            var jsonObjectPostCreate = JsonCalendarsCreate.DeserializeTempl(jsonG);

            var template = _repositoriesUnitOfWork.Template.GetItemById(id);
            template.Name = jsonObjectPostCreate.Name;
            template.UpdatedById = userId;

            _repositoriesUnitOfWork.Template.Update(template);

            _templateService.DetachSchedulesByTempId(id, userId);
            var shs = _templateService.GetTemplSchedules(jsonObjectPostCreate.DaysScheduleDict, id, userId);

            _repositoriesUnitOfWork.Schedule.AddRange(shs);

            _repositoriesUnitOfWork.SaveChanges();

            return Json(Url.Action(nameof(Index)));
        }

    }
}
