using System.Collections.Generic;
using static MajorMUD.Interfaces.Common;


namespace MajorMUD.Interfaces
{
    public class IClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ExperiencePercentage { get; set; }
        public int Combat { get; set; }
        public int HitpointsPerLevelMinimum { get; set; }
        public int HitpointsPerLevelMaximum { get; set; }
        public WeaponClasses WeaponType { get; set; }
        public ArmorClasses ArmorType { get; set; }
        public int MagicLevel { get; set; }
        public MagicTypes MagicType { get; set; }
        public Dictionary<Abilities, short> AbilitiesAndMods { get; set; } = new Dictionary<Abilities, short>();
    }
}
