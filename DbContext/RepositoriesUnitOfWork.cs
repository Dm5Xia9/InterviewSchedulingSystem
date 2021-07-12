using ISSystem.DbContext.Repositories;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSystem.DbContext
{
    public class RepositoriesUnitOfWork
    {
        private AppDbContext _appDbContext { get; set; }

        private CalendarRepository calendar;
        private CandidateRepository candidate;
        private RecordingRepository recording;
        private ScheduleRepository schedule;
        private VacancyRepository vacancy;
        private AutoLinkRepository autoLink;
        private TemplateRepository template;

        public CalendarRepository Calendar 
        {
            get
            {
                Singleton<CalendarRepository> sing = new(() => new CalendarRepository(_appDbContext));
                calendar = sing.GetInstance(calendar);
                return calendar;
            }
        }
        public CandidateRepository Candidate
        {
            get
            {
                Singleton<CandidateRepository> sing = new(() => new CandidateRepository(_appDbContext));
                candidate = sing.GetInstance(candidate);
                return candidate;
            }
        }
        public RecordingRepository Recording
        {
            get
            {
                Singleton<RecordingRepository> sing = new(() => new RecordingRepository(_appDbContext));
                recording = sing.GetInstance(recording);
                return recording;
            }
        }
        public ScheduleRepository Schedule
        {
            get
            {
                Singleton<ScheduleRepository> sing = new(() => new ScheduleRepository(_appDbContext));
                schedule = sing.GetInstance(schedule);
                return schedule;
            }
        }
        public VacancyRepository Vacancy
        {
            get
            {
                Singleton<VacancyRepository> sing = new(() => new VacancyRepository(_appDbContext));
                vacancy = sing.GetInstance(vacancy);
                return vacancy;
            }
        }
        public AutoLinkRepository AutoLink
        {
            get
            {
                Singleton<AutoLinkRepository> sing = new(() => new AutoLinkRepository(_appDbContext));
                autoLink = sing.GetInstance(autoLink);
                return autoLink;
            }
        }
        public TemplateRepository Template
        {
            get
            {
                Singleton<TemplateRepository> sing = new(() => new TemplateRepository(_appDbContext));
                template = sing.GetInstance(template);
                return template;
            }
        }


        public RepositoriesUnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void SaveChanges()
        {
            _appDbContext.SaveChanges();
        }
    }
    public class Singleton<T> where T : new()
    {
        public delegate T sct();
        private sct _sct;
        public Singleton(sct sct)
        {
            _sct = sct;
        }
        public T GetInstance(T prObject)
        {
            if (prObject == null)
            {
                prObject = _sct();
            }
            return prObject;
        }
    }
}
