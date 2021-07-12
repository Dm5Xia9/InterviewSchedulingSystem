using ISSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewSchedulingSystem.Areas.Admin.ViewModels.OtherTimeViewModels
{
    public class IndexViewModel
    {
        public IndexViewModel(List<Recording> recordings)
        {
            Recordings = recordings;
        }

        public List<Recording> Recordings { get; set; }
    }
}
