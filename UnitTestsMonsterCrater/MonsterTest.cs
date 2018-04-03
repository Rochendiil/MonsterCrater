using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monster_Crater.DAL.Repo;
using Monster_Crater.DAL.SQLContext;
using Monster_Crater.Models;

namespace UnitTestsMonsterCrater
{
    [TestClass]
    public class MonsterTest
    {
        [TestMethod]
        public void CreateRandomMonster()
        {
            MonsterInstantie monster = MonsterInstantie.createMonsterInstantie();
            Assert.IsNotNull(monster);
        }

        [TestMethod]
        public void UseMove()
        {
            MonsterInstantie monster = new MonsterInstantie();
            MonsterInstantie enemymonster = new MonsterInstantie();
            enemymonster.Health = 100;
            Move move = new Move(1,"name", soort.Paper, 20);
            monster.Moves[0] = move;
            monster.usemove(0,enemymonster);
            Assert.AreEqual(enemymonster.Health, 80);


        }
        [TestMethod]
        public void UpdateMonster()
        {
            MonsterInstantie monster = MonsterInstantie.createMonsterInstantie();
            Assert.IsNotNull(monster);
            monster.Naam = "test";
            monster.Xp = 1;
            monster.GrowthStat = 2;
            monster.Level = 3;
            monster.Health = 4;
            monster.UpdateMonster();
            Assert.AreEqual(monster.Naam, "test");
            Assert.AreEqual(monster.Xp, 1);
            Assert.AreEqual(monster.GrowthStat, 2);
            Assert.AreEqual(monster.Level, 3);
            Assert.AreEqual(monster.Health, 4);
        }
    }
}
