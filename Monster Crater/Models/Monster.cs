using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monster_Crater.Models
{
    public abstract class Monster
    {
        public string Naam { get; set; }
        public  soort Soort { get; set; }
        public int Attack { get; set; }
        public int SpecialAttack { get; set; }
        public int Defence { get; set; }
        public int SpecialDefence { get; set; }
    }
}