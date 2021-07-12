using ISSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public static class CommandHelper
    {
        public static string CreateStringByListRecording(List<Recording> recordings)
        {
            string retstr = "";

            if (recordings.Count() == 0)
                return "Записей не обнаружено";

            foreach (var item in recordings)
            {
                retstr += "-----------------";
                retstr += $"\n| id{item.Id}  \n| {item.Candidate.FullName}  \n| {item.Candidate.Telephone}  \n| {item.Vacancy.Name}  \n| {item.DateTime} \n";
            }
            retstr += "--------------------";
            return retstr;
        }

        public static string CreateStringByRecording(Recording recording)
        {
            string retstr = "";
            retstr += "-----------------";
            retstr += $"\n| id{recording.Id}  \n| {recording.Candidate.FullName}  \n| {recording.Candidate.Telephone}  \n| {recording.Vacancy.Name}  \n| {recording.DateTime} \n";
            retstr += "-----------------";
            return retstr;
        }

        public static string CreateStringByListOtherRecording(List<Recording> recordings)
        {
            string retstr = "";

            if (recordings.Count() == 0)
                return "Записей не обнаружено";

            foreach (var item in recordings)
            {
                retstr += "-----------------";
                retstr += $"\n| id{item.Id}  \n| {item.Candidate.FullName}  \n| {item.Candidate.Telephone}  \n| {item.Vacancy.Name}  \n| {item.DateTime}  \n| {item.Comment} \n";
            }
            retstr += "--------------------";
            return retstr;
        }

        public static string CreateStringByListVacancy(List<Vacancy> vacancy)
        {
            string retstr = "";

            if (vacancy.Count() == 0)
                return "Записей не обнаружено";

            foreach (var item in vacancy)
            {
                retstr += "-----------------";
                retstr += $"\n| id{item.Id}  \n| {item.Name}  \n| id календаря: {item.CalendarId}  \n| {item.Calendar.Name}  \n| активна: {item.IsActive} \n";
            }
            retstr += "--------------------";
            return retstr;
        }

        public static string CreateStringByListCalendar(List<Calendar> calendar)
        {
            string retstr = "";

            if (calendar.Count() == 0)
                return "Записей не обнаружено";

            foreach (var item in calendar)
            {
                retstr += "-----------------";
                retstr += $"\n| id{item.Id}  \n| {item.Name} \n";
            }
            retstr += "--------------------";
            return retstr;
        }

        public static (string, string) SplitString(string str)
        {
            var split = str.Split(',').Select(p => p.Trim()).ToArray();
            return (split[0], split[1]);
        }
    }
}
