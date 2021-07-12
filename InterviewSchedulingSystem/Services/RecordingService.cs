using ISSystem.DbContext;
using ISSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISSystem.DbContext.Repositories;
using InterviewSchedulingSystem.ViewModels.ScheduleViewModels;

namespace InterviewSchedulingSystem.Services
{
    public class RecordingService
    {
        private RepositoriesUnitOfWork _repositories;

        public RecordingService(RepositoriesUnitOfWork repositories)
        {
            _repositories = repositories;
        }

        public List<Recording> GetActualRecordingByVacancyId(int id)
        {
            var recs = _repositories.Recording.GetRecordingsByVacancyId(id);
            return recs.Where(p => p.DateTime.CompareTo(DateTime.Now) > 0 && !p.IsDeleted).ToList();
        }

        public DateTime GetDateTimeByCandidateId(int id)
        {
            var rec = _repositories.Recording.GetByCandidateId(id);
            if (rec != null)
            {
                return rec.DateTime;
            }
            return new DateTime(1, 1, 1);
        }

        public void DeleteRecordingByCandidateId(int id)
        {
            var recording = _repositories.Recording.GetByCandidateId(id);
            _repositories.Recording.Delete(recording);
        }

        public List<IsSelectedVacancy> GetIsSelectedVacancyList(int idVacansy)
        {
            List<IsSelectedVacancy> isSelectedVacancies = new List<IsSelectedVacancy>();

            foreach (var vacancy in _repositories.Vacancy.GetActiveVacanciesList())
            {
                if (vacancy.Id == idVacansy)
                    isSelectedVacancies.Add(new IsSelectedVacancy { Vacancy = vacancy, IsSelected = true });
                else
                    isSelectedVacancies.Add(new IsSelectedVacancy { Vacancy = vacancy, IsSelected = false });
            }
            return isSelectedVacancies;

        }

    }
}
