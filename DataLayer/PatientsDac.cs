using FireSharp.Config;
using FireSharp.Interfaces;
using PHU_Patient_Informations.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PHU_Patient_Informations.DataLayer
{
    public class PatientsDac : IPatientsDac
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "oAQB7OoAz9i4VA0SvIF0QyCwdFISE1lVRfg7Kk78",
            BasePath = "https://phu-patients-default-rtdb.europe-west1.firebasedatabase.app/"
        };

        IFirebaseClient firebaseClient;

        /// <summary>
        /// Constructor to initialise Database Connection 
        /// </summary>
        public PatientsDac()
        {            
            firebaseClient = new FireSharp.FirebaseClient(config);
        }

        /// <summary>
        /// Update the patient discharge status
        /// </summary>
        /// <param name="patDetails">discharge patient details</param>
        public void DischargeDAC(Admission patDetails)
        {
            if (firebaseClient != null)
            {                                
                var dischargeEntry = firebaseClient.Update("AdmissionList/" + patDetails.NINumber, patDetails);
                TransferWardAvailability(patDetails.WardType);                             
            }
        }

        /// <summary>
        /// Add newly added patient details
        /// </summary>
        /// <param name="admission">adding patient details</param>
        public void NewAdmissionDac(Admission admission)
        {
            // Insert newly added patient details                       
            if (firebaseClient != null)
            {                                
                var response = firebaseClient.Set("AdmissionList/" + admission.NINumber, admission);
                UpdateWardAvailability(admission.WardType);                             
            }
        }

        /// <summary>
        /// transferring patient from one ward to another
        /// </summary>
        /// <param name="transferData">transferring ward data</param>
        /// <param name="patDetails">previous ward data</param>
        public void TransferWardDac(TransferDetails transferData, Admission patDetails)
        {
            if (firebaseClient != null)
            {               
                var result = firebaseClient.Set("TransferList/" + Guid.NewGuid(), transferData);                            
                var updateAdmission = firebaseClient.Update("AdmissionList/" + transferData.NINumber, patDetails);
                UpdateWardAvailability(transferData.CurrentWard);
                TransferWardAvailability(transferData.PreviousWard);                                                   
            }
        }

        /// <summary>
        /// Method to get the Bed Availability for specific ward type
        /// </summary>
        /// <param name="wardId">Ward Type</param>
        /// <returns>Bed Availability</returns>
        public WardDetails GetAvailability(string wardId)
        {
            WardDetails wDetails = null;
            var result = firebaseClient.Get("WardAvailability/" + wardId);
            if (result.Body != "null")
            {
                wDetails = result.ResultAs<WardDetails>();
            }
            return wDetails;
        }

        /// <summary>
        /// Method to update the bed availability if any patient added
        /// </summary>
        /// <param name="wardId">Ward Type</param>
        public void UpdateWardAvailability(string wardId)
        {
            var currentCount = GetAvailability(wardId);
            currentCount.AvailableBed--;
            currentCount.CurrentlyOccupied++;
            var result = firebaseClient.Update("WardAvailability/" + wardId, currentCount);
        }

        /// <summary>
        /// Method to update the bed availability if any patient transfer or discharge
        /// </summary>
        /// <param name="prewardId">previous ward type</param>
        public void TransferWardAvailability(string prewardId)
        {
            var currentCount = GetAvailability(prewardId);
            currentCount.AvailableBed++;
            currentCount.CurrentlyOccupied--;
            var result = firebaseClient.Update("WardAvailability/" + prewardId, currentCount);
        }

        /// <summary>
        /// Method to get the patient details
        /// </summary>
        /// <param name="nhsNumber">NHS unique number</param>
        /// <returns>Patient details</returns>
        public Admission GetPatient(string nhsNumber)
        {
            Admission patDetails = null;
            var result = firebaseClient.Get("AdmissionList/" + nhsNumber);
            if (result.Body != "null")
            {
                patDetails = result.ResultAs<Admission>();
            }
            return patDetails;
        }
    }
}
