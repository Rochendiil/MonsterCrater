using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Monster_Crater.Models;
using Newtonsoft.Json.Linq;

namespace Monster_Crater.Controllers
{
    public class HomeController : Controller
    {
        private World world;
        // GET: Home
        public ActionResult Index()
        {
            Session["World"] = null;
            return View();
        }
   
        public ActionResult Login(string username, string password)
        {
            Player player = Player.LoginPlayer(username, password);
            if (player != null)
            {

                player.PositionY = 5;
                player.PositionX = 5;
                Session["Player"] = player;
                return RedirectToAction("Map");
            }
            else
            {
                Session["message"] = "Username or Password is incorrect";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Map()
        {
            World testworld = (World) Session["World"];
            if (testworld == null)
            {
                Player player = (Player) Session["Player"];
                Map map = new Map(new Size(20, 10), new Size(40, 40), new Size(20, 20));
                World model = new World(map, player);
                Session["World"] = model;
                this.world = model;
                return View();
            }
            else
            {
                return View();
            }

        }

    }
}