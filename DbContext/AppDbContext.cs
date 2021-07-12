using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Repositories;
using ISSystem.DbContext.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ISSystem.DbContext
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<Recording> Recording { get; set; }
        public DbSet<TelegramChat> TelegramChats { get; set; }
        public DbSet<AutoLink> AutoLinks { get; set; }
        public DbSet<Template> Templates { get; set; }

        //public RepositoriesObject Repositories { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Repositories = new RepositoriesObject(this);
        }
        public void OnModelCreated(ModelBuilder builder)
        {

            builder.Entity<Candidate>()
                     .HasIndex(candidate => candidate.Id)
                     .IsUnique();
        }
    }
   
}
