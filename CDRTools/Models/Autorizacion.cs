using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDRTools.Models
{
    public class Autorizacion
    {
        [Required]
        [Display(Name = "Clave de referencia")]
        public string Id_Autorizacion { get; set; }

        [Required]
        [Display(Name = "C.A. para restrictor")]
        public int Codigo { get; set; }

        [ForeignKey("Id_Extension")]
        public int Id_Extension { get; set; }
    }
}