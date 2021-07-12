using ISSystem.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace InterviewSchedulingSystem.Hangfire
{
    public static class AutoLink
    {
        public static string connectString { get; set; }
        public static void Send(int AutoLinkId)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            AppDbContext appDbContext = new AppDbContext(optionsBuilder
                    .UseNpgsql(connectString)
                    .Options);

            var link = appDbContext.AutoLinks.FirstOrDefault(p => p.Id == AutoLinkId);

            appDbContext.AutoLinks.Remove(link);
            appDbContext.SaveChanges();
        }
    }
}
