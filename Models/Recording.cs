using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSystem.Models
{
    public class Recording : BaseModelWithUser
    {
        public Recording()
        { }

        public Recording(int candidateId, DateTime dateTime, int scheduleId, int vacancyId)
        {
            CandidateId = candidateId;
            DateTime = dateTime;
            ScheduleId = scheduleId;
            VacancyId = vacancyId;
        }

        public Recording(int candidateId, DateTime dateTime, int vacancyId, string comment)
        {
            CandidateId = candidateId;
            DateTime = dateTime;
            VacancyId = vacancyId;
            Comment = comment;
            IsOtherTime = true;
        }

        public void ChangeIsApproved()
        {
            IsApproved = !IsApproved;
            if (IsApproved && IsLocked)
                IsLocked = false;
        }

        public void ChangeIsLocked()
        {
            IsLocked = !IsLocked;
            if (IsApproved && IsLocked)
                IsApproved = false;
        }

        public bool IsApproved { get; set; }
        public bool IsLocked { get; set; }
        public bool IsOtherTime { get; set; }

        public string Comment { get; set; }

        public DateTime DateTime { get; set; }

        [ForeignKey("ScheduleId")]
        public Schedule Schedule { get; set; }
        public int? ScheduleId { get; set; }

        [ForeignKey("CandidateId")]
        public Candidate Candidate { get; set; }
        public int CandidateId { get; set; }

        [ForeignKey("VacancyId")]
        public Vacancy Vacancy { get; set; }
        public int VacancyId { get; set; }
    }
}
