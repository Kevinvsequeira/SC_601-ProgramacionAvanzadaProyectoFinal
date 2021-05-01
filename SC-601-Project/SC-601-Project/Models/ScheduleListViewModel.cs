using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SC_601_Project.Models
{
    public class ScheduleListViewModel
    {
        [DisplayName("ID")]
        [Required(ErrorMessage = "This field is required.")]
        public int id { get; set; }
        [DisplayName("Hora")]
        [Required(ErrorMessage = "This field is required.")]
        public string time { get; set; }
        [DisplayName("Usuario")]
        [Required(ErrorMessage = "This field is required.")]
        public string username { get; set; }

        public virtual Schedule Schedule { get; set; }

    }
}