using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace SC_601_Project.Models
{
    public class ScheduleViewModel
    {
        [DisplayName("ID")]
        [Required(ErrorMessage = "This field is required.")]
        public int Id { get; set; }
        [DisplayName("Hora")]
        [Required(ErrorMessage = "This field is required.")]
        public string time { get; set; }
        [DisplayName("Duración")]
        [Required(ErrorMessage = "This field is required.")]
        public Nullable<int> duration { get; set; }
        [DisplayName("Capacidad")]
        [Required(ErrorMessage = "This field is required.")]
        public Nullable<int> capacity { get; set; }

    }
}