using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SC_601_Project.Models
{
    public class PersonViewModel
    {
        [DisplayName("ID")]
        [Required(ErrorMessage = "This field is required.")]
        public int idNum { get; set; }
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "This field is required.")]
        public string first_name { get; set; }
        [DisplayName("Apellidos")]
        [Required(ErrorMessage = "This field is required.")]
        public string last_name { get; set; }
        [DisplayName("Fecha Registro")]
        [Required(ErrorMessage = "This field is required.")]
        public Nullable<System.DateTime> registerDate { get; set; }
        [DisplayName("Fecha Nacimiento")]
        [Required(ErrorMessage = "This field is required.")]
        public Nullable<System.DateTime> birthDate { get; set; }
        [DisplayName("Teléfono")]
        [Required(ErrorMessage = "This field is required.")]
        public string phone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserCred> UserCreds { get; set; }
        public string LoginErrorMessage { get; set; }

    }
}