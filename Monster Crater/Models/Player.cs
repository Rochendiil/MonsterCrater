using System;
using System.Drawing;
using Monster_Crater.DAL.Interface;
using Monster_Crater.DAL.Repo;
using Monster_Crater.DAL.SQLContext;
using Newtonsoft.Json;

namespace Monster_Crater.Models
{
    public class Player : Character
    {
        public string Gender { get; set; }
        public int MonsterBalls { get; set;}
        public MonsterManual MonsterManual { get; set; }
        public Player()
        {
            
        }

        public Player(int id, string name, string gender, int monsterBalls)
        {
            this.Id = id;
            Name = name;
            Gender = gender;
            MonsterBalls = monsterBalls;
            Party = new MonsterInstantie[5];
        }
        
    
        public static Player LoginPlayer(string username, string password)
        {
            PlayerRepo repo = new PlayerRepo(new PlayerSQLContext());
            Player player = new Player();
            player = repo.Login(username, password);
            if (player != null)
            {
                player.MonsterManual = repo.FillMonstermanual(player.Id);
            }
            return player;
        }

        public void FillMonstermanual()
        {
            PlayerRepo repo = new PlayerRepo(new PlayerSQLContext());
            this.MonsterManual = repo.FillMonstermanual(this.Id);
        }
        public static void RegisterPlayer(string username, string password, string name, string gender)
        {
            PlayerRepo repo = new PlayerRepo(new PlayerSQLContext());
            repo.Register(username, password, name, gender);
        }

        public static bool Inuse(string username)
        {
            PlayerRepo repo = new PlayerRepo(new PlayerSQLContext());
            return repo.InUse(username);
        }
    }
}