using ISSystem.DbContext;
using ISSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISSystem.DbContext.Repositories;
using Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InterviewSchedulingSystem.Services
{
    public class CalendarService
    {
        private RepositoriesUnitOfWork _repositories;

        public CalendarService(RepositoriesUnitOfWork repositories)
        {
            _repositories = repositories;
        }

        public SelectList GetSelectListActiveCalendars()
        {
            return new SelectList(_repositories.Calendar.GetActiveItemList(), "Id", "Name");
        }

    }
}
