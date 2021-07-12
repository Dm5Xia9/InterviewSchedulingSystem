using ISSystem.DbContext;
using ISSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewSchedulingSystem.Areas.Admin.ViewModels.CandidatesViewModels
{
    public class IndexViewModel
    { 
        public List<CandidateExt> Candidates { get; set; }

        public void Fill(RepositoriesUnitOfWork repositoriesUnitOfWork)
        {
            Candidates = new List<CandidateExt>();
            foreach(var item in repositoriesUnitOfWork.Candidate.GetAllListInclude())
            {
                Candidates.Add(new CandidateExt 
                { 
                    Candidate = item, 
                    Recording = repositoriesUnitOfWork.Recording.GetByCandidateId(item.Id) 
                });
            }
           
        }
    }

    public class CandidateExt
    {
        public Candidate Candidate { get; set; }
        public Recording Recording { get; set; }
    }
}
