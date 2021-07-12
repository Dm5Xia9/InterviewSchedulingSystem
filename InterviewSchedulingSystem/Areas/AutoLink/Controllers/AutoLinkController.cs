using InterviewSchedulingSystem.Areas.AutoLink.ViewModels;
using ISSystem.DbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewSchedulingSystem.Areas.AutoLink.Controllers
{
    [Area("AutoLink")]
    public class AutoLinkController : Controller
    {
        readonly RepositoriesUnitOfWork _repositories;
        public AutoLinkController(RepositoriesUnitOfWork repositories)
        {
            _repositories = repositories;
        }

        [Route("invite/{id}")]
        public ActionResult Invite(string id = "")
        {
            if (id == null || id == "")
                return Redirect("/");

            var autoLink = _repositories.AutoLink.GetAutoLinkByLink(id);

            if(autoLink == null)
                return Redirect("/");

            if (autoLink.IsConfirmed)
            {
                var cand = _repositories.Candidate.GetItemById(autoLink.Id);
                Response.Cookies.Append("telephone", cand.Telephone,
                    new CookieOptions { Expires = DateTime.Now.AddMonths(1) });
                return Redirect("/");
            }

            InviteViewModel inviteViewModel = new()
            {
                link = id,
            };

            return View(inviteViewModel);
        }

        [HttpPost]
        [Route("invite/{id}")]
        public ActionResult Invite(InviteViewModel inviteViewModel)
        {
            var autoLink = _repositories.AutoLink.GetAutoLinkByLink(inviteViewModel.link);
            if(autoLink == null)
                return View(inviteViewModel);

            var cand = _repositories.Candidate.GetItemById(autoLink.Id);
            if(cand.Telephone != inviteViewModel.telephone)
                return View(inviteViewModel);

            cand.IsNotified = true;

            _repositories.Candidate.Update(cand);


            autoLink.IsConfirmed = true;
            _repositories.AutoLink.Update(autoLink);

            _repositories.SaveChanges();

            Response.Cookies.Append("telephone", cand.Telephone,
                    new CookieOptions { Expires = DateTime.Now.AddMonths(1) });
            return Redirect("/");
        }

    }//https://localhost:5001/invite/ace0a14c69cb4e57 (295) 629-4756
}
