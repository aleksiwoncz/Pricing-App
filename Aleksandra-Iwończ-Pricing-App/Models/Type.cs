using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aleksandra_Iwończ_Pricing_App.Models
{
    public class Type
    {
        [Key]
        public int typeId { get; set; }
        public string name { get; set; }
        public int pricePerHour { get; set; }
    }
}
