using ISSystem.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSystem.DbContext.Repositories
{
    public class AutoLinkRepository : RepositoryBase<AutoLink>
    {

        AppDbContext _appDbContext;
        public AutoLinkRepository()
        {
        }

        public AutoLinkRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public AutoLink GetAutoLinkByLink(string link)
        {
            return _appDbContext.AutoLinks.FirstOrDefault(p => p.Link == link);
        }
    }
}
