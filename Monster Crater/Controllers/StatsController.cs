using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Monster_Crater.DAL.Repo;
using Monster_Crater.DAL.SQLContext;

namespace Monster_Crater.Controllers
{
    public class StatsController : Controller
    {
        // GET: Stats
        public ActionResult Index(List<string> filterlist )
        {
            MonsterRepo repo = new MonsterRepo(new MonsterSQLContext());

            if (filterlist == null)
            {
                filterlist = new List<string>();
                for (int i = 0; i < 7; i++)
                {
                    filterlist.Add("");
                }
            }
           
            List<string> statList = repo.MonsterFilter(filterlist);
            List<List<string>> statsfilterList = repo.getfilterlist(filterlist);
            statsfilterList[0].Insert(0, "");
            Session["filterresult"] = statList;
            for (int i = 1; i < 6; i++)
            {
                List<int> intlist = statsfilterList[i].Select(int.Parse).ToList();
                intlist.Sort();
                statsfilterList[i] = intlist.ConvertAll<string>(k => k.ToString());
                statsfilterList[i].Insert(0, "");
            }
            Session["newfilters"] = statsfilterList;
            return View();
        }

        public string applyFilter(string filterstring)
        {
            MonsterRepo repo = new MonsterRepo(new MonsterSQLContext());
            List<string> filterlist = filterstring.Split(',').ToList();
            List<string> statList = repo.MonsterFilter(filterlist);
          string obj = System.Web.Helpers.Json.Encode(statList);
            return obj;
        }
        public string getFilter(string filterstring)
        {
            MonsterRepo repo = new MonsterRepo(new MonsterSQLContext());
            List<string> filterlist = filterstring.Split(',').ToList();
            List<List<string>> statsfilterList = repo.getfilterlist(filterlist);
            statsfilterList[0].Insert(0, "");
            for (int i = 1; i < 6; i++)
            {
                List<int> intlist = statsfilterList[i].Select(int.Parse).ToList();
                intlist.Sort();
                statsfilterList[i] = intlist.ConvertAll<string>(k => k.ToString());
                statsfilterList[i].Insert(0, "");
            }
            string obj = System.Web.Helpers.Json.Encode(statsfilterList);
            return obj;
        }
    }
}