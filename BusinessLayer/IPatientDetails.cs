using System;
using System.Collections.Generic;
using System.Text;

namespace PHU_Patient_Informations.Interface
{
    interface IPatientDetails
    {
        void NewAdmission();

        void TransferWard();

        void DischargePatient();

        void GetPatientDetails();
    }
}
