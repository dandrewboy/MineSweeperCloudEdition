﻿using DataAccessLayer;
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
        static Player playerOBJ;

        public IActionResult Index()
        {
            return RedirectToAction("Login", "Player");
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
                HttpContext.Session.SetInt32(SessionKeyId, objPlayer.PlayerID); //set the session var
                playerOBJ = objPlayer;
                return View("Dashboard", objPlayer);
            }
            return View();
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            //checks if user session is set or not, and if not, we determine its an unauthorized user.
            var User = HttpContext.Session.GetInt32("_Id");
            if (User.HasValue)
            {
                return View(playerOBJ);
            }
            else
            return RedirectToAction("Index", "Player");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            //clears session so player can set new session.
            HttpContext.Session.Clear();
            playerOBJ = null;
            return RedirectToAction("Index", "Player");
        }


        [HttpGet]
        public IActionResult Edit()
        {
            return View(playerOBJ);
        }

        //will implement user account update next milestone
        [HttpPost]
        public IActionResult Edit([Bind] Player objPlayer)
        {
            /*playerDAL.ValidatePlayer(objPlayer);
            if (objPlayer.PlayerID == -1)
            {
                ViewBag.Message = "Login Failed!"; //Does show up :)
            }
            else
            {
                ViewBag.Message = "Login Successful!"; //Will not show in time because of redirect
                HttpContext.Session.SetInt32(SessionKeyId, objPlayer.PlayerID);
                playerOBJ = objPlayer;
                return View("Dashboard", objPlayer);
            }*/
            return View();
        }
    }
}
