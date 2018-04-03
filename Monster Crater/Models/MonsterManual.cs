using System.Collections.Generic;
using Monster_Crater.DAL.Repo;
using Monster_Crater.DAL.SQLContext;

namespace Monster_Crater.Models
{
    public class MonsterManual
    {
        public List<MonsterEntry> Monsterlist;

        public MonsterManual(List<MonsterEntry> monsterlist)
        {
            Monsterlist = monsterlist;
        }
    }
}