using ISSystem.DbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewSchedulingSystem.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {

        [HttpGet]
        public ActionResult Index()
        {
            return Redirect("/admin/calendars");
        }
    }
}
