using System.Collections.Generic;
using static MajorMUD.Interfaces.Common;


namespace MajorMUD.Interfaces
{
    public class IMonster
    {
        public int MonsterId { get; set; }
        public string MonsterName { get; set; }
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
        public int ItemDrop1_ItemID { get; set; }
        public int ItemDrop2_ItemID { get; set; }
        public int ItemDrop3_ItemID { get; set; }
        public int ItemDrop4_ItemID { get; set; }
        public int ItemDrop5_ItemID { get; set; }
        public int ItemDrop1_DropRate { get; set; }
        public int ItemDrop2_DropRate { get; set; }
        public int ItemDrop3_DropRate { get; set; }
        public int ItemDrop4_DropRate { get; set; }
        public int ItemDrop5_DropRate { get; set; }
        public int CashDrop_Runic { get; set; }
        public int CashDrop_Platinum { get; set; }
        public int CashDrop_Gold { get; set; }
        public int CashDrop_Silver { get; set; }
        public int CashDrop_Copper { get; set; }
        public int Experience { get; set; }

        // TODO:  Fill in the rest of Monsters info here.  
        public int BSDefense;                   // BS defense
        public int CharmResist;                 // Charm resist
        public int FollowChance;                // Monster follow% (agility?)
        public int sLevelUnknown;               // HP regen? Monster level?
        public int Undead;                      // Undead?
        public IReadOnlyDictionary<Abilities, short> Abilities = new Dictionary<Abilities, short>();      // max 10   // Monster abilities
        public int AbilityValues;               // max 10   // Monster abil values
        public IItem Weapon;                    // Weapon used
        public AttackType Attack;               // max 5    // Attack type (0=None,1=Normal,2=Spell,3=Rob3?)
        public int AttackChance;                // max 5    // Attack chance     (only 5 used now)
        public int AttackAccuracy;              // max 5    // Attack accuracy/spell#
        public int AttackMinimumDamage;         // max 5    // Attack min damage/cast%
        public int AttackMaxDamage;             // max 5    // Attack max damage/cast level
        public int AttackEnergyCost;            // max 5    // Attack energy used
        public int AlternateAttackHitSpells;    // max 5    // Alternate attack hit spells
        public ISpell OnDeathSpell;             // Death spell
        public ISpell OnRegenSpell;             // Regen spell
        public ISpell InBetweenRoundSpell;      // max 5    // In-between round spells
        public int InBetweenRoundSpellChange;   // max 5    // In-between round chance %
        public int InBetweenRoundSpellLevel;    // max 5    // In-between rount cast level
    }
}
