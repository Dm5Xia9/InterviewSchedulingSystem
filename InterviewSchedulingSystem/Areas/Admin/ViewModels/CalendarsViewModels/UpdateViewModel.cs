using InterviewSchedulingSystem.Helpers;
using InterviewSchedulingSystem.Services;
using ISSystem.DbContext.Repositories;
using ISSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewSchedulingSystem.Areas.Admin.ViewModels.CalendarsViewModels
{
    public class UpdateViewModel
    {
        public void Fill(List<List<Day>> days, 
            SelectList vacanciesSelectList, List<int> vacanciesId)
        {
            Days = days;
            VacanciesSelectList = vacanciesSelectList;
            VacanciesId = vacanciesId;
        }

        public List<List<Day>> Days { get; set; }
        public SelectList VacanciesSelectList { get; set; }
        public List<int> VacanciesId { get; set; }
        public string Name { get; set; }
        public int Id { get; set; } 
    }

    public class Day
    {
        public DateTime DayT { get; set; }
        public List<Time> Times { get; set; }
    }

    public class Time
    {
        public DateTime TimeD { get; set; }
    }

}
