using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Monster_Crater.DAL.Interface;
using Monster_Crater.Models;

namespace Monster_Crater.DAL.Repo
{
    public class MonsterRepo
    {
        private IMonster context;

        public MonsterRepo(IMonster context)
        {
            this.context = context;
        }

        public void UpdateMonster(MonsterInstantie monsterInstantie)
        {
            context.UpdateMonster(monsterInstantie);
        }

        public MonsterInstantie RandomMonster()
        {
            return context.RandomMonster();
        }

        public void MonsterSeen(MonsterInstantie monsterInstantie, Player player)
        {
            context.MonsterSeen(monsterInstantie, player);
        }

        public List<string> MonsterFilter(List<string> filterList)
        {
            return context.MonsterFilter(filterList);
        }

        public List<List<string>> getfilterlist(List<string> filterList)
        {
            return context.getfilterlist(filterList);
        }
    }
}