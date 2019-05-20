using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace APSI_ResevationMod.Models
{
    public class EmployeeReservation
    {
        public EMPLOYEES employee { get; set; }
        public List<PROJECT_EMPLOYEES_RESERVATION> reservations { get; set; }
        public PROJECT_EMPLOYEES_RESERVATION reservation { get; set; }
        public IEnumerable<PROJECTS> projects { get; set; }
        //public List<PROJECT> projects { get; set; }
        public SortedDictionary<DateTime, int> precentOfDaysReserved { get; set; }
    }
    //public enum Weeks
    //{
        
    //    Styczeń,
    //    Luty,
    //    Marzec,
    //    Kwiecień,
    //    Maj,
    //    Czerwiec,
    //    Lipiec,
    //    Sierpień,
    //    Wrzesień,
    //    Październik,
    //    Listopad,
    //    Grudzień
    //}
}