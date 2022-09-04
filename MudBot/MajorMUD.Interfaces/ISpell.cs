using static MajorMUD.Interfaces.Common;


namespace MajorMUD.Interfaces
{
    public interface ISpell
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Command { get; set; }
        public int Mana { get; set; }
        public int Level { get; set; }
        public SpellType Type { get; set; }
        public SpellFlag Flag { get; set; }
        public int LevelMultiplier { get; set; }
        public int EnergyUsed { get; set; }
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }
        public int Duration { get; set; }
        public int Chance { get; set; }
        public bool AreaOfEffect { get; set; }
        public int MaximumLevel { get; set; }                    // Max. level
        public int LevelDivider { get; set; }                    // Level divider?
        public int UseLevel { get; set; }                        // Use level range?
        public int IncEvery { get; set; }                        // ?
        public CastType CastingType { get; set; }                // Cast type
        public IItem LearnedFromItem { get; set; }
    }
}
