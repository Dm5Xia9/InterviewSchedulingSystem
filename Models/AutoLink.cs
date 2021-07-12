using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSystem.Models
{
    public class AutoLink : BaseModel
    {

        public string ToPathString()
        {
            return $"https://localhost:5001/invite/{Link}";
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ForeignKey("Candidate")]
        public new int Id { get; set; }

        public string Link { get; set; }

        public bool IsConfirmed { get; set; }

        public Candidate Candidate { get; set; }
    }
}
