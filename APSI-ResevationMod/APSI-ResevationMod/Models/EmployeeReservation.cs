using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace APSI_ResevationMod.Models
{
    public class EmployeeReservation
    {
        public EMPLOYEE employee;
        public List<PROJECT_EMPLOYEES_RESERVATION> reservations;
        public SortedDictionary<DateTime, int> precentOfDaysReserved;
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