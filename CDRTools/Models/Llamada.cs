using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CDRTools.Models
{
    public class Llamada
    {
        public int globalCallID_callManagerId { get; set; }
        public int globalCallID_callId { get; set; }

        [Display(Name = "Fecha")]
        public DateTime dateTimeOrigination { get; set; }

        [Display(Name = "Número origen")]
        public string callingPartyNumber { get; set; }

        [Display(Name = "Número destino")]
        public string originalCalledPartyNumber { get; set; }

        [Display(Name = "Duración")]
        public int duration { get; set; }
    }
}