using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Monster_Crater.Models;

namespace Monster_Crater.Controllers
{
    public class MonsterManualController : Controller
    {
        // GET: MonsterManual
        public ActionResult Index()
        {
            World world = (World) Session["World"];
            world.Player.FillMonstermanual();
            Session["World"] = world;
            return View();
        }
    }
}