using ISSystem.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSystem.DbContext.Repositories
{
    public class ScheduleRepository : RepositoryBase<Schedule>
    {
        public ScheduleRepository()
        {
        }
        public ScheduleRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        public IQueryable<Schedule> GetSchedulesByCalendarId(int? calendarId)
        {
            return _appDbContext.Schedule.Where(p => p.CalendarId == calendarId && !p.IsDeleted);
        }

        public IQueryable<Schedule> GetSchedulesByTempId(int? tempId)
        {
            return _appDbContext.Schedule.Where(p => p.TemplateId == tempId && !p.IsDeleted);
        }
    }
}
