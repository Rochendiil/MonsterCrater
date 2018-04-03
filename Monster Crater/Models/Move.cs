using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monster_Crater.Models
{
    public class Move
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public soort Soort { get; set; }
        public int Attack { get; set; }
        public int CurrentPP { get; set; }
        public int MaxPP { get; set; }

        public Move(int id, string name, soort soort, int attack)
        {
            Id = id;
            Name = name;
            Soort = soort;
            Attack = attack;
        }
    }
}