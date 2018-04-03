using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Monster_Crater.Models
{
    public class World 
    {

        public Map CurrentMap { get; set; }
        
        public Player Player { get; set; }

        public MonsterInstantie WildMonster { get; set; }
        public World()
        {
            
        }

        public World(Map currentMap, Player player)
        {
            Player = player;
            CurrentMap = currentMap;
        }

        public static World DecodeWorld(string jsonstring)
        {
            World world;
            try
            {
                world = JsonConvert.DeserializeObject<World>(jsonstring);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return world;
        }
    }
}