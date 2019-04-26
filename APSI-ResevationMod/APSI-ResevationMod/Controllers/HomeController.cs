using APSI_ResevationMod.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APSI_ResevationMod.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult UserDetails()
        {
            DetailsUser model = new DetailsUser();

            model.User = new User
            {
                Name = "Damian",
                Surname = "Kappa",
                DateOfbirth = "01.01.1994",
                EmployeeID = "123",
                ContactPhone = "666999333"

            };
            model.UserReservation = new List<UserReservation>();
            model.UserReservation.Add(new UserReservation
            {
                EmployeeID = "123",
                Project = "APSI nr 4",
                TimePercentReserved=12,

                
            });

            ViewBag.Message = "User data";

            return View(model);

        }

        public ActionResult Hardware()
        {
            DetailsHardware model = new DetailsHardware();

            model.Hardware = new Hardware
            {
                Name = "Printer"
            };

            model.HardwareReservations = new List<HardwareReservation>();
            model.HardwareReservations.Add(new HardwareReservation
            {
                WhatIsReserved = "Printer",
                WhoReserves = "Damian"
            });

            ViewBag.Message = "Hardware data";

            return View();

        }

        public ActionResult Room()
        {
            DetailsRoom model = new DetailsRoom();

            model.Room = new Room
            {
                RoomNumber = "1234"
            };

            model.RoomReservations = new List<RoomReservation>();
            model.RoomReservations.Add(new RoomReservation
            {
                RoomNumberReserved = "1234",
                WhoReserves = "Damian"
            });
            
            ViewBag.Message = "Room data";

            return View();

        }
    }
}