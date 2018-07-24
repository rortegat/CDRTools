using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CDRTools.Models
{
    public class Extension
    {
        [Required]
        [Display(Name ="Extension")]
        public int Id_Extension { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(100)]
        public string Extension_Descripcion { get; set; }
    }
}