using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SC_601_Project.Models
{
    public class UserCredViewModel
    {

        [DisplayName("ID")]
        [Required(ErrorMessage = "This field is required.")]
        public int id { get; set; }
        //public Nullable<int> idNum { get; set; }
        [DisplayName("ID")]
        [Required(ErrorMessage = "This field is required.")]
        public int idNum { get; set; }
        [DisplayName("Usuario")]
        [Required(ErrorMessage = "This field is required.")]
        public string username { get; set; }
        [DisplayName("Contraseña")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This field is required.")]
        public string password { get; set; }
        [DisplayName("Estado")]
        [Required(ErrorMessage = "This field is required.")]
        public string status { get; set; }
        [DisplayName("Rol")]
        [Required(ErrorMessage = "This field is required.")]
        public string role { get; set; }

        public virtual Person Person { get; set; }
        public string LoginErrorMessage { get; set; }

    }
}