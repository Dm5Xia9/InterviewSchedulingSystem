using ISSystem.DbContext;
using ISSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewSchedulingSystem.Services
{
    public class TemplateService
    {
        private RepositoriesUnitOfWork _repositories;

        public TemplateService(RepositoriesUnitOfWork repositories)
        {
            _repositories = repositories;
        }

        public List<Schedule> GetTemplSchedules(Dictionary<DateTime,
            List<DateTime>> dateTimeListDateTimePairs, int templId,
            string userId)
        {
            var lists = new List<Schedule>();
            foreach (var daySchedule in dateTimeListDateTimePairs)
            {
                var timeShedule = daySchedule.Value.Select(t => new DateTimeSchedule { Time = t });

                var sh = new Schedule()
                {
                    TemplateId = templId,
                    Date = daySchedule.Key,
                    TimeSchedule = new TimeSchedule
                    {
                        Times = timeShedule.OrderBy(p => p.Time).ToList()
                    }
                };
                sh.CreatedById = userId;
                sh.UpdatedById = userId;
                lists.Add(sh);
            }
            return lists;
        }

        public void DetachSchedulesByTempId(int id, string userId)
        {
            var schedules = _repositories.Schedule.GetSchedulesByTempId(id);
            foreach (var sc in schedules)
            {
                sc.TemplateId = null;
                sc.UpdatedById = userId;
                _repositories.Schedule.Update(sc);
            }
        }
    }
}
