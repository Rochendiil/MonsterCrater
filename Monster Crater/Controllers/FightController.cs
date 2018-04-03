using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Monster_Crater.Models;

namespace Monster_Crater.Controllers
{
    public class FightController : Controller
    {
        
        public ActionResult StartFight(string worldstring)
        {

            World world = World.DecodeWorld(worldstring);
            world.WildMonster = MonsterInstantie.createMonsterInstantie();
            world.WildMonster.Monsterseen(world.Player);
            Session["World"] = world;

            return Redirect("Fight");
        }
        
        public ActionResult EndFight(string worldstring)
        {

            World world = World.DecodeWorld(worldstring);
            Session["Player"] = world.Player;
            foreach (MonsterInstantie monster in world.Player.Party)
            {
                monster?.UpdateMonster();
            }
           
            Session["World"] = world;

            return Redirect("/Home/Map");
        }

        public ActionResult StartSwitch(string worldstring)
        {
            World world = World.DecodeWorld(worldstring);
            Session["World"] = world;
            return Redirect("Switch");
        }
        public ActionResult EndSwitch(string worldstring)
        {

            World world = World.DecodeWorld(worldstring);
            Session["World"] = world;
            return Redirect("Fight");
        }

        public ActionResult Switch()
        {
            return View();
        }
        public ActionResult Fight()
        {
            return View();
        }
    }
}