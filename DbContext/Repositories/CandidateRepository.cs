using ISSystem.DbContext.Interfaces;
using ISSystem.Models;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSystem.DbContext.Repositories
{
    public class CandidateRepository : RepositoryBase<Candidate>
    {
        public CandidateRepository()
        {
        }

        public CandidateRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public List<Candidate> GetAllListInclude()
        {
            return _appDbContext.Candidates.Include(p => p.Vacancy).Include(p => p.AutoLink).OrderBy(p => p.AutoLink).ToList();
        }

        public Candidate GetCandidateByTelephone(string telephone)
        {
            return _appDbContext.Candidates.Include(t => t.Vacancy).FirstOrDefault(p => p.Telephone == telephone && !p.IsDeleted);
        }

        public bool IsUniqueTelephone(string telephone)
        {
            return !_appDbContext.Candidates.Select(p => p.Telephone).Any(p => p == telephone);
        }

    }
}
