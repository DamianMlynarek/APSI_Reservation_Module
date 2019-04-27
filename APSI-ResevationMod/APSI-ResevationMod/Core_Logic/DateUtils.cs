using APSI_ResevationMod.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APSI_ResevationMod.Core_Logic
{
    public static class DateUtils
    {
        public static SortedDictionary<DateTime, int> CalculateProjectsLoadForEmployee(List<PROJECT_EMPLOYEES_RESERVATION> reservations)
        {
            var days = new SortedDictionary<DateTime, int>();

            foreach(var reservation in reservations)
            {
                List<DateTime> dayslist = Enumerable.Range(0, 1 + reservation.EndDate.Subtract(reservation.BeginDate).Days)
               .Select(offset => reservation.BeginDate.AddDays(offset))
               .ToList();
                foreach(var day in dayslist)
                {
                    if(days.ContainsKey(day))
                    {
                        days[day] = days[day] + reservation.Extent;
                    }
                    else
                    {

                    }
                    days.Add(day, reservation.Extent);
                }
            }
            return days;

        }
    }
}