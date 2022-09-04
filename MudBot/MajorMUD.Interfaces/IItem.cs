using System.Collections.Generic;
using static MajorMUD.Interfaces.Common;


namespace MajorMUD.Interfaces
{
    public class IItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ItemType Type { get; set; }
        public ItemEquippableSlot Slot { get; set; }
        public ItemEquippedOn CurrentEquippedOn { get; set; }
        public int MinimumToKeep { get; set; }
        public int MaximumToGet { get; set; }
        public long Price { get; set; }
        public int MaximumInGame { get; set; }
        public string Shop { get; set; }
        public string Path { get; set; }
        public int Weight { get; set; }
        public ItemFlags Flags { get; set; }
        public int MaximumUses { get; set; }
        public int MinimumStrengthToUse { get; set; }
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }
        public int Accuracy { get; set; }
        public int Speed { get; set; }
        public int ArmorClass { get; set; }
        public int DamageReduction { get; set; }
        public IReadOnlyDictionary<Abilities, short> Abilities = new Dictionary<Abilities, short>(10); // Max 10
        public int WeaponNumberOfHandsNeeded { get; set; }
        public int Material { get; set; }                   // TODO:  What is this?  WeaponType and ArmorType?
        public int Body { get; set; }
        public HashSet<int> NegatesSpells = new HashSet<int>();   // Max 5 spell IDs
        public HashSet<int> Classes { get; set; } = new HashSet<int>();   // Allowed classes, max 10
        public HashSet<int> Races { get; set; } = new HashSet<int>();     // Allowed races, max 10
        public HashSet<int> Casts { get; set; } = new HashSet<int>();
        public HashSet<int> LinkToSpell { get; set; } = new HashSet<int>();  // `black tome` teaches two spells, so we create a collection

        public int DroppedBy { get; set; }
        public int FromItem { get; set; }
        public int BasePrice { get; set; }
    }
}
