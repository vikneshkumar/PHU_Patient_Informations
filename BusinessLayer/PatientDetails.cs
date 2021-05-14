using System;
using System.Collections.Generic;
using System.Text;

using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using PHU_Patient_Informations.Models;
using PHU_Patient_Informations.DataLayer;

namespace PHU_Patient_Informations.Interface
{
    /// <summary>
    /// Implementation class for IPatientDetails
    /// </summary>
    class PatientDetails : IPatientDetails
    {
        IPatientsDac _patientsDac;

        /// <summary>
        /// Constructor to initialise DataLayer 
        /// </summary>
        public PatientDetails(IPatientsDac patientsDac)
        {
            _patientsDac = patientsDac;
        }

        /// <summary>
        /// Implementaion class for Discharging the patients logic
        /// </summary>
        /// <returns>true or false</returns>
        public void DischargePatient()
        {
            Console.WriteLine("Enter Discharge Details:");
            Console.WriteLine("Patient NHS Number:");
            string nhsNumber = Console.ReadLine();
            var patDetails = _patientsDac.GetPatient(nhsNumber);
            if (patDetails != null && patDetails.Status)
            {
                Console.WriteLine("Discharge Type(Home, Died & Other):");
                patDetails.DischargeStatus = Console.ReadLine();
                patDetails.Status = false;
                patDetails.DischargeDate = DateTime.Now;
                _patientsDac.DischargeDAC(patDetails);
                Console.WriteLine("Patient Record updated succesfully !!!");
            }
            else
            {
                Console.WriteLine("Patient Record Not Found!");
            }
        }

        /// <summary>
        /// Implementaion class for newly adding patient details
        /// </summary>
        /// <returns>true or false</returns>
        public void NewAdmission()
        {
            Console.WriteLine("Add Patient Details:");
            Admission admission = new Admission();
            Console.WriteLine("Patient Name");
            admission.PatientName = Console.ReadLine();
            Console.WriteLine("NHS Number");
            admission.NINumber = Console.ReadLine();
            Console.WriteLine("Enter Ward Type(1.Cardiology 2.Renal 3.A&E 4.Critical Care)");
            admission.WardType = Console.ReadLine();
            Console.WriteLine("Doctor Assigned");
            admission.DoctorId = Console.ReadLine();

            WardDetails wDetails = _patientsDac.GetAvailability(admission.WardType);
            if (wDetails != null && wDetails.AvailableBed != 0)
            {
                admission.Status = true;
                admission.AdmittedDate = DateTime.Now;
                _patientsDac.NewAdmissionDac(admission);
                Console.WriteLine("Patient Details Added Succesfully!!!");
            }
            else
            {
                Console.WriteLine("No Bed available");
            }
        }

        /// <summary>
        /// Implementaion class for patients ward transfer 
        /// </summary>
        /// <returns>true or false</returns>
        public void TransferWard()
        {
            //Console.WriteLine("Transfer Ward");
            TransferDetails transferData = new TransferDetails();
            Console.WriteLine("Patient NHS Number:");
            transferData.NINumber = Console.ReadLine();

            var patDetails = _patientsDac.GetPatient(transferData.NINumber);
            if (patDetails != null && patDetails.Status)
            {
                Console.WriteLine("Transfer Ward Type(1.Cardiology 2.Renal 3.A&E 4.Critical Care):");
                transferData.CurrentWard = Console.ReadLine();
                Console.WriteLine("Doctor Assigned");
                transferData.CurrentDoctor = Console.ReadLine();
                transferData.Date = DateTime.Now;

                if (patDetails.WardType == transferData.CurrentWard)
                {
                    Console.WriteLine("Cannot transfer within same ward");
                }
                else
                {
                    transferData.PreviousDoctor = patDetails.DoctorId;
                    transferData.PreviousWard = patDetails.WardType;
                    WardDetails wDetails = _patientsDac.GetAvailability(transferData.CurrentWard);
                    if (wDetails != null && wDetails.AvailableBed != 0)
                    {
                        patDetails.WardType = transferData.CurrentWard;
                        patDetails.DoctorId = transferData.CurrentDoctor;
                        _patientsDac.TransferWardDac(transferData, patDetails);
                        Console.WriteLine("Patient transferred to new ward!!!");
                    }
                    else
                    {
                        Console.WriteLine("There is no Bed availablility in this ward!!!");
                    }
                }
            }
            else
            {
                Console.WriteLine("Patient Record Not Found!!!");
            }

        }

        public void GetPatientDetails()
        {            
            Console.WriteLine("Patient NHS Number:");
            string niNumber = Console.ReadLine();
            var patDetails = _patientsDac.GetPatient(niNumber);
            if (patDetails != null && patDetails.Status)
            {
                Console.WriteLine("Patient Name: {0}", patDetails.PatientName);
                Console.WriteLine("Ward: {0}", patDetails.WardType);
                Console.WriteLine("Admission Date: {0}", patDetails.AdmittedDate);
                Console.WriteLine("Doctor: {0}", patDetails.DoctorId);                
                if (!patDetails.Status) Console.WriteLine("Current Status: Not Active");
                if (patDetails.DischargeStatus == "Died") Console.WriteLine("Date of Death: {0}", patDetails.DischargeDate);
            }
            else
            {
                Console.WriteLine("Patient Record Not Found!!!");
            }
        }

    }
}
