using InterviewSchedulingSystem.Helpers;
using InterviewSchedulingSystem.ViewModels.CandidateViewModels;
using ISSystem.DbContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewSchedulingSystem.Areas.Admin.ViewModels.CandidatesViewModels
{
    public class CreateViewModel : AuthenticateViewModel
    {
        public override void Fill(RepositoriesUnitOfWork repositoriesUnitOfWork)
        {
            base.Fill(repositoriesUnitOfWork);

            var link = AutoLinkHelper.GenLink();

            FullLink = $"https://localhost:5001/invite/{link}";

            Link = link;
        }

        public string FullLink { get; set; }
        public string Link { get; set; }
    }
}
