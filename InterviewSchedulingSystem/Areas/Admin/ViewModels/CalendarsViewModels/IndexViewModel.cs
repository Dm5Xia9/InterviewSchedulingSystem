using InterviewSchedulingSystem.Services;
using ISSystem.DbContext;
using ISSystem.DbContext.Repositories;
using ISSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewSchedulingSystem.Areas.Admin.ViewModels.CalendarsViewModels
{
    public class IndexViewModel
    {
        public IndexViewModel(List<Calendar> calendars)
        {
            Calendars = calendars;
        }
        public List<Calendar> Calendars { get; set; }
    }
}
