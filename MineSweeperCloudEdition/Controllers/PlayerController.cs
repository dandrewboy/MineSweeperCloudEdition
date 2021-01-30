using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using MineSweeperCloudEdition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineSweeperCloudEdition.Controllers
{
    public class PlayerController : Controller
    {
        PlayerData playerDAL = new PlayerData();
        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register([Bind] Player objPLayer)
        {
            if(ModelState.IsValid)
            {
                ViewBag.Message = "Registration Successful!";
                playerDAL.AddPlayer(objPLayer);
                return RedirectToAction("Login");
            }
            ViewBag.Message = "An Error occured on Registration.";
            return View(objPLayer);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
