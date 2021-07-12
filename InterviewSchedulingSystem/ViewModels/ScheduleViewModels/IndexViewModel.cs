using ISSystem.DbContext.Repositories;
using InterviewSchedulingSystem.Services;
using ISSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ISSystem.DbContext;

namespace InterviewSchedulingSystem.ViewModels.ScheduleViewModels
{
    public class IndexViewModel
    {
        
        public Candidate Candidate { get; set; }

        public Vacancy Vacancy { get; set; }
        
        public Recording Recording { get; set; }

        public DateTime CurrentDateTime { get; set; }

        public bool IsNewCandidate { get; set; }

        public List<Schedule> Schedules { get; set; }

        public Vacancy SelectedVacancy { get; set; }


        public bool IsExistsNextPages { get; set; }
        public bool IsExistsPrevPages { get; set; }
        public int CurrentPage { get; set; }

        public IndexViewModel(Candidate candidate, Vacancy vac, int page)
        {
            Candidate = candidate;
            SelectedVacancy = vac;
            CurrentPage = page;
            IsExistsPrevPages = CurrentPage != 0;
        }

        public void Fill(RepositoriesUnitOfWork repositoriesUnitOfWork, ScheduleService scheduleService, int blocksCount)
        {
            IsNewCandidate = !repositoriesUnitOfWork.Recording.IsRecordingByCandidateId(Candidate.Id); ;

            var rec = repositoriesUnitOfWork.Recording.GetByCandidateId(Candidate.Id);
            if (rec != null)
            {
                Recording = rec;
                Vacancy = repositoriesUnitOfWork.Vacancy.GetVacancyByIdNoA(Recording.VacancyId);
            }

            IsExistsNextPages = scheduleService.IsExistsNextPages(SelectedVacancy.CalendarId, blocksCount, CurrentPage);
            Schedules = scheduleService.GetCurrentScheduleList(SelectedVacancy.CalendarId, blocksCount, CurrentPage);
            CurrentDateTime = scheduleService.GetDateTimeByCandidateId(Candidate.Id);
        }
    }
    public class IsSelectedVacancy
    {
        public Vacancy Vacancy { get; set; }
        public bool IsSelected { get; set; }
    }
}
