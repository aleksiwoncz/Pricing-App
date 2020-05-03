using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aleksandra_Iwończ_Pricing_App.Models
{
    public class MultiClass
    {
        public IEnumerable<Task> tasks { get; set; }
        public IEnumerable<Technology> technologies { get; set; }
        public IEnumerable<Type> types { get; set; }

        [Required(ErrorMessage = "Task ID cannot be empty")]
        [Display(Name = "Task ID")] // label
        public int taskId { get; set; }

        [Required(ErrorMessage = "Technology ID cannot be empty")]
        [Display(Name = "Technology ID")]
        public int technologyId { get; set; }

        [Required(ErrorMessage = "Type ID cannot be empty")]
        [Display(Name = "Type ID")]
        public int typeId { get; set; }

    }
}
