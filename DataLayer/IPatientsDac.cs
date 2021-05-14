using PHU_Patient_Informations.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PHU_Patient_Informations.DataLayer
{
    interface IPatientsDac
    {
        void DischargeDAC(Admission patDetails);

        void NewAdmissionDac(Admission admission);

        void TransferWardDac(TransferDetails transferData, Admission patDetails);

        WardDetails GetAvailability(string wardId);

        void UpdateWardAvailability(string wardId);

        void TransferWardAvailability(string prewardId);

        Admission GetPatient(string nhsNumber);

    }
}
