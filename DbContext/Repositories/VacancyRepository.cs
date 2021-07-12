using ISSystem.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSystem.DbContext.Repositories
{
    public class VacancyRepository : RepositoryBase<Vacancy>
    {
        public VacancyRepository()
        {
        }
        public VacancyRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public IQueryable<Vacancy> GetVacanciesByCalendarId(int id)
        {
           return  _appDbContext.Vacancies.Where(p => p.CalendarId == id && !p.IsDeleted);
        }

        public IQueryable<Vacancy> GetActiveVacancies()
        {
            return _appDbContext.Vacancies.Where(p => !p.IsDeleted);
        }

        public Vacancy GetVacancyByIdNoA(int id)
        {
            return _appDbContext.Vacancies.FirstOrDefault(p => p.Id == id);
        }

        public List<Vacancy> GetActiveVacanciesList()
        {
            return _appDbContext.Vacancies.Where(p => p.IsActive && !p.IsHidden && !p.IsDeleted).ToList();
        }
        
        public List<Vacancy> GetHiddenVacancies()
        {
            return _appDbContext.Vacancies.Where(p => p.IsHidden && !p.IsDeleted).ToList();
        }

    }
}
