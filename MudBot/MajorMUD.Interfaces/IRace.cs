using System.Collections.Generic;
using static MajorMUD.Interfaces.Common;


namespace MajorMUD.Interfaces
{
    public class IRace
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public short MinimumStrength { get; set; }
        public int MaximumStrength { get; set; }
        public int MinimumIntellect { get; set; }
        public int MaximumIntellect { get; set; }
        public int MinimumWillpower { get; set; }
        public int MaximumWillpower { get; set; }
        public int MinimumAgility { get; set; }
        public int MaximumAgility { get; set; }
        public int MinimumHealth { get; set; }
        public int MaximumHealth { get; set; }
        public int MinimumCharm { get; set; }
        public int MaximumCharm { get; set; }
        public int HitpointModifierPerLevel { get; set; }
        public int ExperiencePercentage { get; set; }
        public IReadOnlyDictionary<Abilities, short> AbilitiesAndMods { get; set; }

    }
}
