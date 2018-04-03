namespace Monster_Crater.Models
{
    public class MonsterEntry : Monster
    {
        public bool Seen { get; set; }
        public bool Catched { get; set; }
        public int MaxXp { get; set; }
        public int EvolutieLevel { get; set; }
        public MonsterEntry(string naam, bool seen, bool catched, int maxXp, int evolutieLevel, soort soort, int attack, int specialAttack, int defence, int specialDefence)
        {
            Naam = naam;
            Seen = seen;
            Catched = catched;
            MaxXp = maxXp;
            EvolutieLevel = evolutieLevel;
            Soort = soort;
            Attack = attack;
            SpecialAttack = specialAttack;
            Defence = defence;
            SpecialDefence = specialDefence;
        }
    }
}