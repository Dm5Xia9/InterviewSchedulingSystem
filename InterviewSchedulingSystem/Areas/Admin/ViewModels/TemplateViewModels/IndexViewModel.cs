using ISSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewSchedulingSystem.Areas.Admin.ViewModels.TemplateViewModels
{
    public class IndexViewModel
    {
        public IndexViewModel(List<Template> templates)
        {
            Templates = templates;
        }

        public List<Template> Templates { get; set; }
    }
}
