using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSystem.Models
{
    public abstract class BaseModelWithUser : BaseModel
    {
        [ForeignKey("CreatedById")]
        public User CreatedBy { get; set; }
        public string CreatedById { get; set; }

        [ForeignKey("UpdatedById")]
        public User UpdatedBy { get; set; }
        public string UpdatedById { get; set; }
    }
}
