using ISSystem.DbContext;
using ISSystem.DbContext.Repositories;
using ISSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewSchedulingSystem.Services
{
    public class VacancyService
    {
        private RepositoriesUnitOfWork _repositories;

        public VacancyService(RepositoriesUnitOfWork repositories)
        {
            _repositories = repositories;
        }

        public List<Vacancy> GetNormalVacancyListInCalendar()
        {
            return _repositories.Vacancy.GetActiveVacancies().Where(p => !p.IsHidden)
                .OrderBy(t => t.Name).Include(p => p.Calendar).ToList();
        }

        public List<Vacancy> GetHiddenVacancyListInCalendar()
        {
            return _repositories.Vacancy.GetActiveVacancies().Where(p => p.IsHidden)
                .OrderBy(t => t.Name).Include(p => p.Calendar).ToList();
        }

        public void EditVacancies(List<string> ids, int idCalendar, string userId)
        {
            foreach (var item in ids)
            {
                var vac = _repositories.Vacancy.GetItemById(Convert.ToInt32(item));
                vac.CalendarId = idCalendar;
                vac.UpdatedById = userId;
                _repositories.Vacancy.Update(vac);
            }
        }
        public void DetachCalendarById(int id, string userId)
        {
            foreach (var vacancy in _repositories.Vacancy.GetVacanciesByCalendarId(id))
            {
                vacancy.CalendarId = null;
                vacancy.UpdatedById = userId;
                _repositories.Vacancy.Update(vacancy);
            }
        }
    }
}
