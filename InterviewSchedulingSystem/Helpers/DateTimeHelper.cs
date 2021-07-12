using InterviewSchedulingSystem.Areas.Admin.ViewModels.CalendarsViewModels;
using ISSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewSchedulingSystem.Helpers
{
    public static class DateTimeHelper
    {
        public static bool NowOutput()
        {
            var d = DateTime.Now.DayOfWeek;
            if (d == DayOfWeek.Saturday || d == DayOfWeek.Sunday || d == DayOfWeek.Friday)
                return true;
            else
                return false;
        }

        public static string CalendarClassNowOutput(int index)
        {
            if (index != 1)
                return "";

            var d = DateTime.Now.DayOfWeek;
            if (d == DayOfWeek.Saturday || d == DayOfWeek.Sunday || d == DayOfWeek.Friday)
                return "active";
            else
                return "";
        }

        public static List<DateTime> GetListWholeDateTime()
        {
            var listString = new List<DateTime>();
            DateTime dateTime = new DateTime(2000, 1, 1, 10, 0, 0);
            while(dateTime.Hour <= 21)
            {
                listString.Add(dateTime);
                dateTime = dateTime.AddHours(1);
            }
            return listString;
        }
        public static List<DateTime> GetListHalfDateTime()
        {
            var listString = new List<DateTime>();
            DateTime dateTime = new DateTime(2000, 1, 1, 10, 30, 0);
            while (dateTime.Hour < 21)
            {
                listString.Add(dateTime);
                dateTime = dateTime.AddHours(1);
            }
            return listString;
        }
        public static List<DateTime> GetListFourthDateTime()
        {
            var listString = new List<DateTime>();
            DateTime dateTime = new DateTime(2000, 1, 1, 10, 15, 0);
            while (dateTime.Hour <= 21)
            {
                listString.Add(dateTime);
                dateTime = dateTime.AddHours(1);
            }
            return listString;
        }
        public static List<DateTime> GetWeek()
        {
            var week = new List<DateTime>();
            var dayStartWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek + 1);
            for(int i = 0; i < 5; i++)
            {
                week.Add(dayStartWeek.AddDays(i));
            }
            return week;
        }

        public static List<Areas.Admin.ViewModels.TemplateViewModels.Day> GetExtWeek(IQueryable<Schedule> schedules)
        {
            var week = new List<Areas.Admin.ViewModels.TemplateViewModels.Day>();
            var dayStartWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek + 1);
            for (int i = 0; i < 5; i++)
            {
                var cDay = new Areas.Admin.ViewModels.TemplateViewModels.Day();
                cDay.DayT = dayStartWeek.AddDays(i);
                if (schedules.Any(p => p.Date.Date == dayStartWeek.AddDays(i).Date))
                {
                    cDay.Times = new List<Areas.Admin.ViewModels.TemplateViewModels.Time>();
                    foreach (var item in schedules.FirstOrDefault(p => p.Date.Date ==
                        dayStartWeek.AddDays(i).Date && !p.IsDeleted).TimeSchedule.Times)
                    {
                        cDay.Times.Add(new Areas.Admin.ViewModels.TemplateViewModels.Time { TimeD = item.Time });
                    }
                }
                week.Add(cDay);
            }
            return week;
        }

        public static List<List<DateTime>> GetWeeks()
        {
            var Weeks = new List<List<DateTime>>();
            var DaysCurrent = new List<DateTime>();
            var dayStartWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek + 1);
            for (int i = 0; i < 84; i++)
            {
                if (dayStartWeek.AddDays(i).DayOfWeek != DayOfWeek.Saturday &&
                    dayStartWeek.AddDays(i).DayOfWeek != DayOfWeek.Sunday)
                    DaysCurrent.Add(dayStartWeek.AddDays(i));
                if (DaysCurrent.Count != 0 && DaysCurrent.Last().DayOfWeek == DayOfWeek.Friday)
                {
                    Weeks.Add(DaysCurrent);
                    DaysCurrent = new List<DateTime>();
                }
            }
            return Weeks;
        }

        public static List<List<Day>> GetExtendedWeek(IQueryable<Schedule> schedules)
        {
            var Days = new List<List<Day>>();
            var DaysCurrent = new List<Day>();
            var dayStartWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek + 1);
            for (int i = 0; i < 84; i++)
            {
                if (dayStartWeek.AddDays(i).DayOfWeek != DayOfWeek.Saturday &&
                    dayStartWeek.AddDays(i).DayOfWeek != DayOfWeek.Sunday)
                {
                    Day cDay = new Day();
                    cDay.DayT = dayStartWeek.AddDays(i);
                    var shf = schedules.ToList();
                    var s = schedules.FirstOrDefault(p => p.Date.Date == dayStartWeek.AddDays(i).Date && !p.IsDeleted);
                    if (schedules.Any(p => p.Date.Date == dayStartWeek.AddDays(i).Date))
                    {
                        cDay.Times = new List<Time>();
                        foreach (var item in schedules.FirstOrDefault(p => p.Date.Date == 
                        dayStartWeek.AddDays(i).Date && !p.IsDeleted).TimeSchedule.Times)
                        {
                            cDay.Times.Add(new Time { TimeD = item.Time });
                        }
                    }

                    DaysCurrent.Add(cDay);
                }
                if (DaysCurrent.Count != 0 && DaysCurrent.Last().DayT.DayOfWeek == DayOfWeek.Friday)
                {
                    Days.Add(DaysCurrent);
                    DaysCurrent = new List<Day>();
                }
            }
            return Days;
        }

        
    }
}
