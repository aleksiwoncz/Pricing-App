using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Aleksandra_Iwończ_Pricing_App.Models
{
    public class Project
    {
        [Key]
        public int projectId { get; set; }

        [Display(Name = "Date")]
        public DateTime date { get; set; }

        [Display(Name = "Costs")]
        public int costs { get; set; }


        [Required(ErrorMessage = "Hours cannot be empty")]
        [Display(Name = "Hours")]
        [Range(0, 1000000)]
        public int hours { get; set; }

        [Required(ErrorMessage = "Project Manager cannot be empty")]
        [Display(Name = "Project Manager")]
        public string projectManager { get; set; }


        [Required(ErrorMessage = "Type cannot be empty")]
        [Display(Name = "Type")]
        public string typeName { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> types { get; set; }

        [Required(ErrorMessage = "Task cannot be empty")]
        [Display(Name = "Task")]
        public string taskName { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> tasks { get; set; }

        [Required(ErrorMessage = "technologyName cannot be empty")]
        [Display(Name = "Technology")]
        public string technologyName { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> technologies { get; set; }


    }
}
