using ISSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewSchedulingSystem.Extensions
{
    public static class ScheduleExtensions
    {
        public static string ListToString(this List<Schedule> schedules)
        {
            List<string> sl = new List<string>();
            var sches = schedules.Where(p => p.Date.CompareTo(DateTime.Now) > 0).OrderBy(p => p.Date).Take(5);
            foreach (var schedule in sches)
            {
                var timeList = schedule.TimeSchedule.Times.Select(p => p.Time.GetHourMinute()).Take(5);
                var dayDescr = $"{schedule.Date.GetShortDayNameOfWeek()}: {string.Join(" ", timeList)}";
                sl.Add(dayDescr);
            }

            return string.Join("; ", sl);
        }
        public static string SchedulesError(this List<Schedule> schedules)
        {
            var sches = schedules.Where(p => p.Date.CompareTo(DateTime.Now) > 0);
            if (sches.Count() <= 5)
            {
                return $"Календарь заполнен всего на {sches.Count()} {sches.Count().GetStringByDay()}";
            }
            else
            {
                return "";
            }
        }
        public static string GetStringByDay(this int day) =>
        day switch
        {
            0 => "дней",
            1 => "день",
            2 => "дня",
            3 => "дня",
            4 => "дня",
            5 => "дней",
            _ => "none"
        };
    }
}
