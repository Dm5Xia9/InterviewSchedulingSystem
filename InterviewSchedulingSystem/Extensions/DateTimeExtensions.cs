using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewSchedulingSystem.Extensions
{
    public static class DateTimeExtensions
    {
        public static string GetDayNameOfWeek(this DateTime day) =>
        day.DayOfWeek switch
        {
            DayOfWeek.Monday    => "Понедельник",
            DayOfWeek.Tuesday   => "Вторник",
            DayOfWeek.Wednesday => "Среда",
            DayOfWeek.Thursday  => "Четверг",
            DayOfWeek.Friday    => "Пятница",
            DayOfWeek.Saturday  => "Суббота",
            DayOfWeek.Sunday    => "Воскресенье",
            _                   => "none"
        };

        public static string GetDayNameWhenOfWeek(this DateTime day) =>
        day.DayOfWeek switch
        {
            DayOfWeek.Monday => "Понедельник",
            DayOfWeek.Tuesday => "Вторник",
            DayOfWeek.Wednesday => "Среду",
            DayOfWeek.Thursday => "Четверг",
            DayOfWeek.Friday => "Пятницу",
            DayOfWeek.Saturday => "Субботу",
            DayOfWeek.Sunday => "Воскресенье",
            _ => "none"
        };

        public static string GetShortDayNameOfWeek(this DateTime day) =>
        day.DayOfWeek switch
        {
            DayOfWeek.Monday => "Пн",
            DayOfWeek.Tuesday => "Вт",
            DayOfWeek.Wednesday => "Ср",
            DayOfWeek.Thursday => "Чт",
            DayOfWeek.Friday => "Пт",
            DayOfWeek.Saturday => "Сб",
            DayOfWeek.Sunday => "Вс",
            _ => "none"
        };

        public static string GetMonthName(this DateTime dateTime) =>
        dateTime.Month switch
        {
            1  => "Января",
            2  => "Февраля",
            3  => "Марта",
            4  => "Апреля",
            5  => "Мая",
            6  => "Июня",
            7  => "Июля",
            8  => "Августа",
            9  => "Сентября",
            10 => "Октября",
            11 => "Ноября",
            12 => "Декабря",
            _  => "none"
        };

        public static string GetHourMinute(this DateTime dateTime) => dateTime.ToString("H:mm");
    }
}
