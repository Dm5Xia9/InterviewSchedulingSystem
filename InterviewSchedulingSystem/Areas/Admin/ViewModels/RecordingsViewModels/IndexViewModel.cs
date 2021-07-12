using ISSystem.DbContext;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ISSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterviewSchedulingSystem.Services;

namespace InterviewSchedulingSystem.Areas.Admin.ViewModels.RecordingsViewModels
{
    public class IndexViewModel
    {
        public IndexViewModel(
            List<Recording> isNormalRecordings, 
            List<Recording> isLockedRecordings, 
            List<Recording> isApprovedRecordings, 
            List<Recording> isСanceledRecordings)
        {
            IsNormalRecordings = isNormalRecordings;
            IsLockedRecordings = isLockedRecordings;
            IsApprovedRecordings = isApprovedRecordings;
            IsСanceledRecordings = isСanceledRecordings;
        }

        public List<Recording> IsNormalRecordings { get; set; }
        public List<Recording> IsLockedRecordings { get; set; }
        public List<Recording> IsApprovedRecordings { get; set; }
        public List<Recording> IsСanceledRecordings { get; set; }
    }
}
