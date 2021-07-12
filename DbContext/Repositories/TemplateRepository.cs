using ISSystem.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSystem.DbContext.Repositories
{
    public class TemplateRepository : RepositoryBase<Template>
    {
        public TemplateRepository()
        {
        }

        public TemplateRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }



    }
}
