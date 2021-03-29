using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using MineSweeperCloudEdition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MineSweeperCloudEdition.Controllers
{
    public class PlayerController : Controller
    {
        PlayerData playerDAL = new PlayerData();
        public const string SessionKeyId = "_Id";
        public string SessionInfo_Id { get; private set; }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register([Bind] Player objPLayer)
        {
            string success = "Registration Successful!";
            string fail = "An Error occured on Registration.";
            if (ModelState.IsValid)
            {
                playerDAL.AddPlayer(objPLayer);
                ViewBag.Message = success;
                return RedirectToAction("Login");
            }
            ViewBag.Message = fail;
            return View(objPLayer);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([Bind] Player objPlayer)
        {
            playerDAL.ValidatePlayer(objPlayer);
            if (objPlayer.PlayerID == -1)
            {
                ViewBag.Message = "Login Failed!"; //Does show up :)
            }
            else
            {
                ViewBag.Message = "Login Successful!"; //Will not show in time because of redirect
                HttpContext.Session.SetInt32(SessionKeyId, objPlayer.PlayerID);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
