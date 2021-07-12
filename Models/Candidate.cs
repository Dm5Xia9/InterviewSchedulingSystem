using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSystem.Models
{
    public class Candidate : BaseModel
    {

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Telephone { get; set; }

        public bool IsNotified { get; set; }

        [ForeignKey("VacancyId")]
        public Vacancy Vacancy { get; set; }
        public int VacancyId { get; set; }

        public AutoLink AutoLink { get; set; }
        public int AutoLinkId { get; set; }

        public List<Recording> Recording { get; set; }
    }

}
