using System;
using System.Collections.Generic;
using System.Text;

namespace PHU_Patient_Informations.Models
{
    public class Admission
    {
        public string PatientName { get; set; }

        public string NINumber { get; set; }

        public string WardType { get; set; }

        public string DoctorId { get; set; }

        public bool  Status { get; set; }

        public DateTime AdmittedDate { get; set; }

        public DateTime DischargeDate { get; set; }

        public string DischargeStatus { get; set; }
    }
}
