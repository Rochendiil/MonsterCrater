using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Monster_Crater.Models;

namespace Monster_Crater.DAL.Interface
{
    public interface IMonster
    {
        void UpdateMonster(MonsterInstantie monsterInstantie);
        MonsterInstantie RandomMonster();
        void MonsterSeen(MonsterInstantie monsterInstantie, Player player);
        List<string> MonsterFilter(List<string> filterList);
        List<List<string>> getfilterlist(List<string>filterList);
    }
}