using System.Collections.Generic;
using static MajorMUD.Interfaces.Common;


namespace MajorMUD.Interfaces
{
    public interface IMonster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Priority AttackPriority { get; set; }
        public MegamudFlags Flags { get; set; }
        public MonsterAlignment Alignment { get; set; }
        public Relationship MonsterRelationship { get; set; }
        public string PreAttackSpell { get; set; }
        public string AttackSpell { get; set; }
        public int PreAttackSpellMaxCast { get; set; }
        public int AttackSpellMaxCast { get; set; }
        public Gender Sex { get; set; }
        public int Level { get; set; }
        public int Hitpoints { get; set; }
        public int Energy { get; set; }
        public int MagicResistance { get; set; }
        public int Accuracy { get; set; }
        public int ArmorClass { get; set; }
        public int DamageReduction { get; set; }
        public int EnslaveLevel { get; set; }
        public LocationGroup Group { get; set; }
        public MonsterType Type { get; set; }
        public int GameMax { get; set; }
        public int RegenInHours { get; set; }
        public int LocationMap { get; set; }
        public int LocationRoom { get; set; }
        public HashSet<(int /* ItemId */, int /* DropRate */)> ItemDrops { get; set; }
        public long CashDrop { get; set; }
        public int Experience { get; set; }

        // TODO:  Fill in the rest of Monsters info here.  
        public int BSDefense { get; set; }                   // BS defense
        public int CharmResist { get; set; }                 // Charm resist
        public int FollowChance { get; set; }                // Monster follow% (agility?)
        public int sLevelUnknown { get; set; }               // HP regen? Monster level?
        public int Undead { get; set; }                      // Undead?
        public IReadOnlyDictionary<Abilities, short> Abilities { get; set; }  // max 10   // Monster abilities
        public int AbilityValues { get; set; }               // max 10   // Monster ability values
        public IItem Weapon { get; set; }                    // Weapon used
        public AttackType Attack { get; set; }               // max 5    // Attack type (0=None,1=Normal,2=Spell,3=Rob3?)
        public int AttackChance { get; set; }                // max 5    // Attack chance     (only 5 used now)
        public int AttackAccuracy { get; set; }              // max 5    // Attack accuracy/spell#
        public int AttackMinimumDamage { get; set; }         // max 5    // Attack min damage/cast%
        public int AttackMaxDamage { get; set; }             // max 5    // Attack max damage/cast level
        public int AttackEnergyCost { get; set; }            // max 5    // Attack energy used
        public int AlternateAttackHitSpells { get; set; }    // max 5    // Alternate attack hit spells
        public ISpell OnDeathSpell { get; set; }             // Death spell
        public ISpell OnRegenSpell { get; set; }             // Regen spell
        public ISpell InBetweenRoundSpell { get; set; }      // max 5    // In-between round spells
        public int InBetweenRoundSpellChange { get; set; }   // max 5    // In-between round chance %
        public int InBetweenRoundSpellLevel { get; set; }    // max 5    // In-between round cast level
    }
}
