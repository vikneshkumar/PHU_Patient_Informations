using System;
using System.Collections.Generic;
using System.Text;

namespace PHU_Patient_Informations.Models
{
    public class TransferDetails
    {
        public string NINumber { get; set; }

        public string PreviousWard { get; set; }

        public string PreviousDoctor { get; set; }

        public string CurrentWard { get; set; }

        public string CurrentDoctor { get; set; }

        public DateTime Date { get; set; }
    }
}
