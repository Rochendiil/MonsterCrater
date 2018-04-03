using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Monster_Crater.DAL.Interface;
using Monster_Crater.Models;

namespace Monster_Crater.DAL.Repo
{
    public class PlayerRepo
    {
        private IPlayer context;

        public PlayerRepo(IPlayer context)
        {
            this.context = context;
        }

        public Player Login(string username, string password)
        {
            return context.Login(username, password);
        }

        public void Register(string username, string password, string name, string gender)
        {
            context.Register(username, password, name, gender);
        }

        public MonsterInstantie RandomMonster()
        {
            return context.RandomMonster();
        }

        public MonsterManual FillMonstermanual(int playerid)
        {
            return context.FillMonstermanual(playerid);
        }

        public bool InUse(string username)
        {
            return context.InUse(username);
        }
    }
}