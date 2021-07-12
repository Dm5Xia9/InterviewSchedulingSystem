using ISSystem.DbContext.Interfaces;
using ISSystem.Models;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSystem.DbContext.Repositories
{
    public class CalendarRepository : RepositoryBase<Calendar>
    {
        private AppDbContext _appDbContext;
        public CalendarRepository()
        {

        }
        public CalendarRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public List<Calendar> GetIncludeSchedulesList()
        {
            return _appDbContext.Calendars.Where(t => !t.IsDeleted).Include(p => p.Schedules).ToList();
        }
    }
}
