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
    public class RecordingRepository : RepositoryBase<Recording>
    {
        public RecordingRepository()
        {
        }

        public RecordingRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public bool IsRecordingByCandidateId(int candidateId)
        {
            return _appDbContext.Recording.Any(p => p.CandidateId == candidateId && !p.IsDeleted);
        }

        public Recording GetByCandidateId(int candidateId)
        {
            return _appDbContext.Recording.FirstOrDefault(p => p.CandidateId == candidateId && !p.IsDeleted);
        }

        public List<Recording> GetNormalRecordings()
        {
            return _appDbContext.Recording.Where(p => !p.IsApproved && !p.IsLocked && !p.IsDeleted && !p.IsOtherTime)
                .Include(t => t.Candidate)
                .Include(t => t.Vacancy)
                .OrderBy(p => p.Id).ToList();
        }

        public List<Recording> GetLockedRecordings()
        {
            return _appDbContext.Recording.Where(p => p.IsLocked && !p.IsDeleted)
                .Include(t => t.Candidate)
                .Include(t => t.Vacancy)
                .OrderBy(p => p.Id).ToList();
        }

        public List<Recording> GetApprovedRecordings()
        {
            return _appDbContext.Recording.Where(p => p.IsApproved && !p.IsDeleted)
                .Include(t => t.Candidate)
                .Include(t => t.Vacancy)
                .OrderBy(p => p.Id).ToList();
        }

        public List<Recording> GetСanceledRecordings()
        {
            return _appDbContext.Recording.Where(p => p.IsDeleted)
                .Include(t => t.Candidate)
                .Include(t => t.Vacancy)
                .OrderBy(p => p.Id).ToList();
        }

        public List<Recording> GetOtherTimeRecordings()
        {
            return _appDbContext.Recording.Where(p => p.IsOtherTime && !p.IsDeleted && !p.IsApproved && !p.IsLocked)
                .Include(t => t.Candidate)
                .Include(t => t.Vacancy)
                .OrderBy(p => p.Id).ToList();
        }

        public List<Recording> GetRecordingsByVacancyId(int id)
        {
            var cands = _appDbContext.Candidates.Where(p => p.VacancyId == id && !p.IsDeleted).Include(p => p.Recording).SelectMany(p => p.Recording);

            return cands.ToList();
        }
    }
}
