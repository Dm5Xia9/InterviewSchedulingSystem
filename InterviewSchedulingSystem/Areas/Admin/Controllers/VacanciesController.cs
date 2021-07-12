using AutoMapper;
using InterviewSchedulingSystem.Areas.Admin.ViewModels.VacanciesViewModels;
using InterviewSchedulingSystem.Services;
using ISSystem.DbContext;
using ISSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InterviewSchedulingSystem.Areas.Admin.Controllers
{
    public class VacanciesController : AdminController
    {
        VacancyService _vacancyService;
        CalendarService _calendarService;
        RecordingService _recordingService;

        public VacanciesController(VacancyService vacancyService, 
            IMapper mapper, RepositoriesUnitOfWork repositoriesUnitOfWork,
            CalendarService calendarService, RecordingService recordingService,
            UserManager<User> manager) : 
            base(mapper, repositoriesUnitOfWork, manager)
        {
            _vacancyService = vacancyService;
            _calendarService = calendarService;
            _recordingService = recordingService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var normalVacancies = _vacancyService.GetNormalVacancyListInCalendar();
            var hiddenVacancies = _vacancyService.GetHiddenVacancyListInCalendar();
            IndexViewModel indexViewModel = new IndexViewModel(normalVacancies, hiddenVacancies);

            return View(indexViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var calendarsSelectList = _calendarService.GetSelectListActiveCalendars();
            CreateVacancyViewModel vacancyViewModel = new CreateVacancyViewModel();
            vacancyViewModel.Fill(calendarsSelectList);

            return View(vacancyViewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateVacancyViewModel vacancyViewModel)
        {
            if (!ModelState.IsValid)
            {

                var calendarsSelectList = _calendarService.GetSelectListActiveCalendars();
                vacancyViewModel.Fill(calendarsSelectList);

                return View(vacancyViewModel);
            }

            var userId = _userManager.GetUserId(User);

            var vac = _mapper.Map<CreateVacancyViewModel, Vacancy>(vacancyViewModel);
            vac.CreatedById = userId;
            vac.UpdatedById = userId;

            _repositoriesUnitOfWork.Vacancy.Add(vac);

            _repositoriesUnitOfWork.SaveChanges();
            return RedirectToAction("Index", "Vacancies");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var Vacancy = _repositoriesUnitOfWork.Vacancy.GetItemById(id);

            if (Vacancy == null)
                return NotFound();

            var vm = _mapper.Map<Vacancy, EditVacancyViewModel>(Vacancy);
            var calendarsSelectList = _calendarService.GetSelectListActiveCalendars();
            vm.Fill(calendarsSelectList);

            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(EditVacancyViewModel editVacancyViewModel)
        {
            if (!ModelState.IsValid)
            {
                var calendarsSelectList = _calendarService.GetSelectListActiveCalendars();
                editVacancyViewModel.Fill(calendarsSelectList);
                return View(editVacancyViewModel);
            }

            var userId = _userManager.GetUserId(User);

            var model = _mapper.Map<EditVacancyViewModel, Vacancy>(editVacancyViewModel);
            model.UpdatedById = userId;
            _repositoriesUnitOfWork.Vacancy.Update(model);

            _repositoriesUnitOfWork.SaveChanges();
            return RedirectToAction("Index", "Vacancies");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var userId = _userManager.GetUserId(User);

            var actualRecording = _recordingService.GetActualRecordingByVacancyId(id);
            if (actualRecording.Count != 0)
            {
                return Json("err");
            }
            var vac = _repositoriesUnitOfWork.Vacancy.GetItemById(id);
            vac.UpdatedById = userId;
            _repositoriesUnitOfWork.Vacancy.Delete(vac);

            _repositoriesUnitOfWork.SaveChanges();
            return Json(Url.Action(nameof(Index)));
        }

        [HttpPost]
        public ActionResult Active(int id)
        {
            var userId = _userManager.GetUserId(User);

            var vacancy = _repositoriesUnitOfWork.Vacancy.GetItemById(id);
            vacancy.IsActive = !vacancy.IsActive;
            vacancy.UpdatedById = userId;
            _repositoriesUnitOfWork.Vacancy.Update(vacancy);

            _repositoriesUnitOfWork.SaveChanges();
            return Json(Url.Action(nameof(Index)));
        }

        [HttpPost]
        public ActionResult Hidden(int id)
        {
            var userId = _userManager.GetUserId(User);

            var vacancy = _repositoriesUnitOfWork.Vacancy.GetItemById(id);
            vacancy.IsHidden = !vacancy.IsHidden;
            vacancy.UpdatedById = userId;
            _repositoriesUnitOfWork.Vacancy.Update(vacancy);

            _repositoriesUnitOfWork.SaveChanges();
            return Json(Url.Action(nameof(Index)));
        }
    }
}
