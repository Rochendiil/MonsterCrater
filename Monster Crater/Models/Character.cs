using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.WebPages.Instrumentation;

namespace Monster_Crater.Models
{
    abstract public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public MonsterInstantie[] Party { get; set; }

        public Character()
        {
            
        }

    }
}