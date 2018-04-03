using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Monster_Crater.DAL.Repo;
using Monster_Crater.DAL.SQLContext;

namespace Monster_Crater.Models
{
    public class MonsterInstantie : Monster
    {

        public int Id { get; set; }
        public int MonsterId { get; set; }
        public int GrowthStat { get; set; }
        public int Xp { get; set; }
        public int MaxXp { get; set; }
        public int Level { get; set; }
        public int MaxLevel { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public Move[] Moves { get; set; }
        public bool Active { get; set; }
        private readonly MonsterRepo repo = new MonsterRepo(new MonsterSQLContext());

        public MonsterInstantie()
        {
            Moves = new Move[3];
        }
        public MonsterInstantie(int id, int monsterId,string naam, soort soort, int growthStat, int xp, int maxXp, int level, int maxLevel, int attack, int specialAttack, int defence, int specialDefence, int health, int maxhealth)
        {
            Id = id;
            MonsterId = monsterId;
            Naam = naam;
            Soort = soort;
            GrowthStat = growthStat;
            Xp = xp;
            MaxXp = maxXp;
            Level = level;
            MaxLevel = maxLevel;
            Attack = attack;
            SpecialAttack = specialAttack;
            Defence = defence;
            SpecialDefence = specialDefence;
            Health = health;
            MaxHealth = maxhealth;
            Moves = new Move[3];
            Active = false;
        }


        public void usemove(int move, MonsterInstantie enemyInstantie)
        {
            enemyInstantie.Health -= Moves[move].Attack;
        }

        public void UpdateMonster()
        {
            repo.UpdateMonster(this);
        }
        public static MonsterInstantie createMonsterInstantie()
        {
            MonsterSQLContext context = new MonsterSQLContext();
            MonsterRepo repo = new MonsterRepo(context);
            return repo.RandomMonster();
        }

        public void Monsterseen(Player player)
        {
            repo.MonsterSeen(this, player);
        }

    }


}