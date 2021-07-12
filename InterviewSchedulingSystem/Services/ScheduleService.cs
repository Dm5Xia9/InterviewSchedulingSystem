using ISSystem.DbContext;
using ISSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISSystem.DbContext.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using InterviewSchedulingSystem.ViewModels.ScheduleViewModels;

namespace InterviewSchedulingSystem.Services
{
    public class ScheduleService
    {
        private RepositoriesUnitOfWork _repositories;

        public ScheduleService(RepositoriesUnitOfWork repositories)
        {
            _repositories = repositories;
        }

        public Schedule ChangeTime(int scheduleId, DateTime dateTime, bool isAvailable)
        {
            Schedule schedule = _repositories.Schedule.GetItemById(scheduleId);
            DateTimeSchedule dateTimeSchedule = TimesFirstDateTime(schedule, dateTime);

            int indexDateTimeSchedule = schedule.TimeSchedule.Times.IndexOf(dateTimeSchedule);
            dateTimeSchedule.IsAvailable = isAvailable;
            schedule.TimeSchedule.Times[indexDateTimeSchedule] = dateTimeSchedule;

            return schedule;
        }

        public DateTimeSchedule TimesFirstDateTime(Schedule schedule, DateTime dateTime)
        {
            return schedule.TimeSchedule.Times.FirstOrDefault(p => p.Time == dateTime);
        }

        public IQueryable<Schedule> GetActualSchedules(IQueryable<Schedule> schedules)
        {
            var gg = schedules.ToList();
            var DateStart = DateTime.Now;
            var noactual = schedules.Where(p => p.Date.CompareTo(DateStart) < 0 && !p.IsDeleted);
            var uo = schedules.Skip(noactual.Count()).ToList();
            return schedules.Skip(noactual.Count());
        }

        public List<Schedule> GetCurrentScheduleList(int? calendarId, int blocksCount, int page)
        {
            return GetActualSchedules(_repositories.Schedule.
                GetSchedulesByCalendarId(calendarId)
                .OrderBy(p => p.Date)).Skip(blocksCount * page).Take(blocksCount).ToList();
        }

        public bool IsExistsNextPages(int? calendarId, int blocksCount, int page)
        {
            return _repositories.Schedule.GetSchedulesByCalendarId(calendarId)
                .OrderBy(p => p.Date).Skip(blocksCount * (page + 1)).Any();
        }

        public void AddSchedules(Dictionary<DateTime,
            List<DateTime>> dateTimeListDateTimePairs, int calendarId,
            string userId)
        {
            foreach (var daySchedule in dateTimeListDateTimePairs)
            {
                var timeShedule = daySchedule.Value.Select(t => new DateTimeSchedule { Time = t });

                var sh = new Schedule()
                {
                    CalendarId = calendarId,
                    Date = daySchedule.Key,
                    TimeSchedule = new TimeSchedule
                    {
                        Times = timeShedule.OrderBy(p => p.Time).ToList()
                    }
                };
                sh.CreatedById = userId;
                sh.UpdatedById = userId;
                _repositories.Schedule.Add(sh);
            }
        }

        public void DetachSchedulesByCalendarId(int id, string userId)
        {
            var schedules = _repositories.Schedule.GetSchedulesByCalendarId(id);
            foreach (var sc in schedules)
            {
                sc.CalendarId = null;
                sc.UpdatedById = userId;
                _repositories.Schedule.Update(sc);
            }
        }

        //Fix
        public List<IsSelectedVacancy> GetIsSelectedVacancyList(int idVacansy)
        {
            List<IsSelectedVacancy> isSelectedVacancies = new List<IsSelectedVacancy>();

            foreach (var vacancy in _repositories.Vacancy.GetActiveVacanciesList())
            {
                isSelectedVacancies.Add(new IsSelectedVacancy { Vacancy = vacancy, IsSelected = vacancy.Id == idVacansy });
            }

            return isSelectedVacancies;
        }

        //Fix
        public DateTime GetDateTimeByCandidateId(int id)
        {
            var rec = _repositories.Recording.GetByCandidateId(id);
            if (rec != null)
            {
                return rec.DateTime;
            }
            return new DateTime(1, 1, 1);
        }

        public void UpdateCalendarTemp(DateTime lastDateTime, int numberOfWeek, int calendarId, List<Schedule> tempShedules)
        {
            var dayStartWeek = lastDateTime.AddDays(8 - (int)lastDateTime.DayOfWeek);

            for (int i = 0; i < numberOfWeek; i++)
            {
                var UpTempSchedules = new List<Schedule>();

                for (int j = 0; j < 7; j++)
                {
                    var currSh = tempShedules.FirstOrDefault(p => p.Date.DayOfWeek.ToString() == dayStartWeek.DayOfWeek.ToString());
                    if (currSh == null)
                    {
                        dayStartWeek = dayStartWeek.AddDays(1);
                        continue;
                    }

                    List<DateTimeSchedule> dateTimeSchedules = new List<DateTimeSchedule>();
                    foreach (var time in currSh.TimeSchedule.Times)
                    {
                        TimeSpan timeSpan = new(time.Time.Hour, time.Time.Minute, time.Time.Second);
                        var newDateTime = dayStartWeek.Date + timeSpan;

                        dateTimeSchedules.Add(new DateTimeSchedule
                        {
                            IsAvailable = true,
                            Time = newDateTime
                        });
                    }

                    var UpSh = new Schedule();
                    UpSh.Date = dayStartWeek;
                    UpSh.CalendarId = calendarId;
                    UpSh.TimeSchedule = new TimeSchedule
                    {
                        Times = dateTimeSchedules
                    };
                    UpTempSchedules.Add(UpSh);
                    dayStartWeek = dayStartWeek.AddDays(1);
                }
                _repositories.Schedule.AddRange(UpTempSchedules);
            }
        }

    }
}
