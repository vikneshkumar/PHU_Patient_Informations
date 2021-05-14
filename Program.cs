using Microsoft.Extensions.DependencyInjection;
using PHU_Patient_Informations.DataLayer;
using PHU_Patient_Informations.Interface;
using System;

namespace PHU_Patient_Informations
{
    class Program
    {
        static void Main(string[] args)
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()                
                .AddSingleton<IPatientDetails, PatientDetails>() 
                .AddSingleton<IPatientsDac, PatientsDac>()
                .BuildServiceProvider();

            Console.WriteLine("Hello PHU!");
            
            var getDetails = serviceProvider.GetService<IPatientDetails>();            
            
            int output;
            bool valid = false;
            do
            {
                Console.WriteLine("1. New Admission");
                Console.WriteLine("2. Transfer Ward");
                Console.WriteLine("3. Discharge Patients");
                Console.WriteLine("4. Get Patient Record");
                Console.WriteLine("0. Exit");
                Console.WriteLine("Please Enter Options:");

                string input = Console.ReadLine();
                
                if (int.TryParse(input, out output))
                {
                    LogicalController(getDetails, output, ref valid);
                }
                else
                {
                    Console.WriteLine("Invalid Entry. Please enter number 1, 2, 3 or 4");                    
                }
            } while (!valid);           
        }

        /// <summary>
        /// To handle the user logic
        /// </summary>
        /// <param name="getDetails">patient details declaration </param>
        /// <param name="index">user option</param>
        /// <param name="isValid">return option</param>
        private static void LogicalController(IPatientDetails getDetails, int index, ref bool isValid)
        {
            isValid = false;
            switch (index)
            {
                case 1:
                    getDetails.NewAdmission();                    
                    break;
                case 2:
                    getDetails.TransferWard();                    
                    break;
                case 3:
                    getDetails.DischargePatient();                    
                    break;
                case 4:
                    getDetails.GetPatientDetails();
                    break;
                case 0:                    
                    isValid = true;
                    break;
                default:
                    Console.WriteLine("Please Enter valid number");                    
                    break;
            }
        }


    }

    

}
