using System;
using System.Collections.Generic;
using System.Text;

namespace PHU_Patient_Informations.Models
{
     public class WardDetails
    {
        public int WardID { get; set; }

        public string WardName { get; set; }
        public int TotalBed { get; set; }
        public int AvailableBed { get; set; }
        public int CurrentlyOccupied { get; set; }
       
    }
}
