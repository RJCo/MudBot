
namespace MajorMUD.Interfaces
{
    public class Common
    {
        #region Path related enums
        public enum PathFlags
        {
            None = 0x00000000,
            PathIsLoop = 0x00000001,
            IgnoreItemIfPicklocks = 0x00000002,
            Activated = 0x01000000,             // Available to player
            OutOfDate = 0x02000000,             // Path need updating?
            Verified = 0x04000000,              // Path has been verified against database
        }

        public enum ActionToTake
        {
            Ignore = 0,
            CheckRoom = 1,
            WaitForItToEnd = 2,
            RestForHPs = 3,
            RestForMana = 4,
            RunAway = 5,
            Hangup = 6,
        }
        #endregion

        #region Message related enums
        public enum Effect
        {
            None = 0x00000000,
            Blinded = 0x00000001,
            Confused = 0x00000002,
            Poisoned = 0x00000004,
            LosingHPs = 0x00000008,
            CannotMove = 0x00000010,
            CannotAttack = 0x00000020,
            Diseased = 0x00000040,
            RegeningHP = 0x00000080,
            FindAnywhereInText = 0x00000100,
            RegeningMana = 0x00000200,
            FindInConversation = 0x00000400,
            EndsCombat = 0x00001000,
            LastActionFailed = 0x00002000,
            UseWhenChasing = 0x00004000,
            Disabled = 0x00008000,
        }

        // Not sure here
        public enum MessageFlag
        {
            None = 0x00000000,
            Active = 0x00000001,
            Wait = 0x00000002,
            Parse1 = 0x00000004,
            Parse2 = 0x00000008,
        }
        #endregion

        #region Monster related enums
        public enum MonsterType
        {
            Solo = 0x00,
            Leader = 0x01,
            Follower = 0x02,
            Stationary = 0x03,
        }

        public enum MonsterAlignment
        {
            Good = 0x00,
            Evil = 0x01,
            ChoaticEvil = 0x02,
            Neutral = 0x03,
            LawfulGood = 0x04,
            NeutralEvil = 0x05,
            LawfulEvil = 0x06,
            Unknown = 0x07,
        }

        public enum Priority
        {
            Normal = 0x00,
            FindFirst = 0x08,
            Last = 0x10,
            Low = 0x20,
            High = 0x40,
            First = 0x80,
        }

        public enum MegamudFlags
        {
            NotHostile = 0x02,
            DontBackstab = 0x01,
            CheckIfAlive = 0x04,
        }

        public enum Relationship
        {
            Unknown = 0x00,
            Friend = 0x02,
            Avoid = 0x03,
            Enemy = 0x04,
            Flee = 0x05,
            Hangup = 0x06,
        }

        public enum Gender
        {
            It = 0x00,
            Male = 0x01,
            Female = 0x02,
            Unknown = 0x03
        }
        #endregion

        #region Room related enums
        public enum LocationGroup
        {
            OrcMansion = 0x26,
            SpecialCreateAtUser = 0x25,
            NewhavenGraveyardAndCrypt = 0x24,
            ProphecyOfPlagueMod9 = 0x20,
            SavageLandsMod7 = 0x1f,
            SmugglersMod5 = 0x1e,
            ObsidianPassageways = 0x1d,
            CrystalTunnels = 0x1c,
            CrackedPlainsAndLavaFields = 0x1b,
            FungusForestAndBlackFort = 0x1a,
            HazySwamp = 0x19,
            DwarfMinesMod4 = 0x18,
            BlackWastelands = 0x17,
            BlackCavesMod3 = 0x16,
            AncientRuinsMod2 = 0x15,
            DesertMod6 = 0x14,
            Rhudaur = 0x13,
            NomadsAndMonastery = 0x12,
            FrozenCaverns = 0x11,
            RockyTrailNomads = 0x10,
            Monastery = 0x0f,
            CryptQuestMonsters = 0x0e,
            HornedDemon = 0x0d,
            CryptQuestArea = 0x0c,
            Labyrinth = 0x0b,
            GoblinCaves = 0x0a,
            DragonsTeethHills = 0x09,
            BlackHouseAndBelow = 0x08,
            GraveyardAndCrypt = 0x07,
            SlumsAndSewers = 0x06,
            GuardsTemplars = 0x05,
            UniquesShopkeepers = 0x02,
            Arena = 0x00,
        }

        public enum AttackType
        {
            None = 0,
            Normal = 1,
            Spell = 2,
            Rob3 = 3,
        }

        public enum Flags
        {
            NoFlags = 0x0000,
            Shop = 0x0002,
            Bank = 0x0004,
            Trainer = 0x0008,
            StopBeforeEntering = 0x0010,
            AvoidThisRoomIfPossible = 0x0020,
            HideInGoToList = 0x4040,
            SpecialRoom = 0x8000
        }

        public enum PathRoomFlags
        {
            None = 0x0000,
            DarkRoom = 0x0001,
            RestUpHere = 0x0002,
            DoNotRestHere = 0x0004,
            PitchBlack = 0x0008,
            StashPoint = 0x0010,
            DoNotAttackHere = 0x0040,
            RelearnRoom = 0x0080,
            SuspectUNKNOWN = 0x0100,
            DisarmTraps = 0x0200,
            CanPickLock = 0x0400,
            CycledUNKNOWN = 0x8000,
        }

        public enum Exits
        {
            North = 0x00001,
            DoorNorth = 0x00002,
            South = 0x00004,
            DoorSouth = 0x00008,
            East = 0x00010,
            DoorEast = 0x00020,
            West = 0x00040,
            DoorWest = 0x00080,
            Northeast = 0x00100,
            DoorNortheast = 0x00200,
            Northwest = 0x00400,
            DoorNorthwest = 0x00800,
            Southeast = 0x01000,
            DoorSoutheast = 0x02000,
            Southwest = 0x04000,
            DoorSouthwest = 0x08000,
            Up = 0x10000,
            DoorUp = 0x20000,
            Down = 0x40000,
            DoorDown = 0x80000,
        }
        #endregion

        #region Class related enums
        public enum WeaponClasses
        {
            OneHandedBlunt = 0x00,
            TwoHandedBlunt = 0x01,
            OneHandedSharp = 0x02,
            TwoHandedSharp = 0x03,
            OneHanded = 0x04,
            TwoHanded = 0x05,
            AnySharp = 0x06,
            BluntOnly = 0x07,
            AllWeapons = 0x08,
            Staff = 0x09,
        }

        public enum ArmorClasses
        {
            Platemail = 0x09,
            Scalemail = 0x08,
            Chainmail = 0x07,
            Leather = 0x06,
            Ninja = 0x02,
            Silk = 0x01,
            Unknown = 0x00,
        }
        #endregion

        #region Spell related enums
        public enum MagicTypes
        {
            None = 0x00,
            Mage = 0x01,
            Priest = 0x02,
            Druid = 0x03,
            Bard = 0x04,
            Kai = 0x05,
        }

        public enum SpellType
        {
            ANY = 0,
            PRIEST1 = 1,
            PRIEST2 = 2,
            PRIEST3 = 3,
            MAGE1 = 4,
            MAGE2 = 5,
            MAGE3 = 6,
            DRUID1 = 7,
            DRUID2 = 8,
            DRUID3 = 9,
            BARDIC = 10,
            MYSTIC = 11,
        }

        public enum SpellFlag
        {
            NONE = 0x0000,
            UNSURE = 0x0001,
            TIMED = 0x0002,
            EVIL = 0x0004,
            INSTANT = 0x0008,
            SELF = 0x0010,
            PLAYER = 0x0020,
            MONSTER = 0x0040,
            AREA = 0x0080,
            LEARNT = 0x1000,
        }

        // TODO:  Are there more CastTypes?
        public enum CastType
        {
            None = 0,
        }
        #endregion

        #region Item related enums
        public enum ItemType
        {
            Clothing = 0,
            Weapon = 1,
            Projectile = 2,
            Furniture = 3,
            Food = 4,
            Drink = 5,
            Light = 6,
            Key = 7,
            Container = 8,
            Scroll = 9,
            Special = 10,
        }

        public enum ItemEquippableSlot
        {
            Unknown = 0,
            None = 1,
            Weapon = 2,
            Offhand = 3,
            Head = 4,
            Hands = 5,
            Finger = 6,
            Feet = 7,
            Arms = 8,
            Back = 9,
            Neck = 10,
            Legs = 11,
            Waist = 12,
            Torso = 13,
            Wrist = 14,
            Ears = 15,
        }

        public enum ItemEquippedOn
        {
            None = 0,
            Unknown = 1,
            Head = 2,
            Hands = 3,
            Finger = 4,
            Feet = 5,
            Arms = 6,
            Back = 7,
            Neck = 8,
            Legs = 9,
            Waist = 10,
            Torso = 11,
            Offhand = 12,
            Finger2 = 13,
            Wrist = 14,
            Ears = 15,
            Worn = 16,
            Wrist2 = 17,
            Face = 18,
            Eyes = 19,
        }

        public enum ItemWeaponType
        {
            OneHandedBlunt = 0,
            TwoHandedBlunt = 1,
            OneHandedSharp = 2,
            TwoHandedSharp = 3,
        }

        public enum ItemArmorType
        {
            None = 0,
            Natural = 1,
            Robes = 2,
            Padded = 3,
            SoftLeather = 4,
            StuddedLeather = 5,
            RigidLeather = 6,
            Chainmail = 7,
            Scalemail = 8,
            Platemail = 9,
        }

        public enum ItemFlags
        {
            None = 0x00000000,
            Unknown = 0x00000001,
            AutoGet = 0x00000002,
            AutoBuy = 0x00000004,
            AutoSell = 0x00000008,
            AutoEquip = 0x00000010,
            AutoFind = 0x00000020,
            CantBeTaken = 0x00000040,
            CanUseToBackstab = 0x00000200,
            MinToKeep = 0x00000400,
            AutoDrop = 0x00000800,
            AutoOpen = 0x00001000,
            IsAWeapon = 0x00010000,
            IsLoyal = 0x00020000,
            MustGet = 0x00100000,
            TriedGet = 0x00200000,
            NoStock = 0x00400000,
            Cash = 0x00800000,
        }
        #endregion

        public enum Abilities
        {
            NoAbility = 0x0000,
            AbsoluteDamage = 0x0001,
            Defence = 0x0002,
            ResistCold = 0x0003,
            AlterAttackDamage = 0x0004,
            ResistFire = 0x0005,
            EnslaveCreature = 0x0006,
            ResistDamage = 0x0007,
            StealHPs = 0x0008,
            ShadowStealth = 0x0009,
            ProtectiveShield = 0x000A,
            Speed = 0x000B,
            SummonCreature = 0x000C,
            NightVision = 0x000D,
            RoomLight = 0x000E,
            Hunger = 0x000F,
            Thirst = 0x0010,
            InflictDamage = 0x0011,
            Heal = 0x0012,
            Poison = 0x0013,
            CurePoison = 0x0014,
            PoisonImmunity = 0x0015,
            AlterAttackValue = 0x0016,
            KillDead = 0x0017,
            DefenceAgainstEvil = 0x0018,
            DefenceAgainstGood = 0x0019,
            DetectMagic = 0x001A,
            Stealth = 0x001B,
            Magical = 0x001C,
            Punch = 0x001D,
            Kick = 0x001E,
            Bash = 0x001F,
            Smash = 0x0020,
            Killblow = 0x0021,
            Dodge = 0x0022,
            Jumpkick = 0x0023,
            ResistMagic = 0x0024,
            Picklocks = 0x0025,
            Tracking = 0x0026,
            Thievery = 0x0027,
            FindTraps = 0x0028,
            DisarmTraps = 0x0029,
            LinkToSpell = 0x002A,
            Casts = 0x002B,
            AlterIntellect = 0x002C,
            AlterWisdom = 0x002D,
            AlterStrength = 0x002E,
            AlterHealth = 0x002F,
            AlterAgility = 0x0030,
            AlterCharm = 0x0031,
            MageBaneQuest = 0x0032,
            AntiMagic = 0x0033,
            EvilInCombat = 0x0034,
            BlindingLight = 0x0035,
            AlterGeneralLight = 0x0036,
            GeneralLightDuration = 0x0037,
            RechargeItem = 0x0038,
            SeeHidden = 0x0039,
            CriticalHitChance = 0x003A,
            ClassItemInclusion = 0x003B,
            Flee = 0x003C,
            AffectExit = 0x003D,
            AlterEvilChance = 0x003E,
            AlterExperience = 0x003F,
            AddCP = 0x0040,
            ResistStone = 0x0041,
            ResistLightning = 0x0042,
            Quickness = 0x0043,
            Slowness = 0x0044,
            Mana = 0x0045,
            SpellCasting = 0x0046,
            Confusion = 0x0047,
            DamagingShield = 0x0048,
            DispelMagic = 0x0049,
            HoldPerson = 0x004A,
            Paralyze = 0x004B,
            Mute = 0x004C,
            Perception = 0x004D,
            Animal = 0x004E,
            Magebind = 0x004F,
            AffectsAnimal = 0x0050,
            Freedom = 0x0051,
            Cursed = 0x0052,
            MajorCurse = 0x0053,
            RemoveCurse = 0x0054,
            Shatter = 0x0055,
            Quality = 0x0056,
            EnergyUsage = 0x0057,
            AlterHP = 0x0058,
            AlterPunchAverage = 0x0059,
            AlterKickAverage = 0x005A,
            AlterJumpkickAverage = 0x005B,
            AlterPunchDamage = 0x005C,
            AlterKickDamage = 0x005D,
            AlterJumpkickDamage = 0x005E,
            Slay = 0x005F,
            Encumberance = 0x0060,
            GoodAligned = 0x0061,
            EvilAligned = 0x0062,
            AlterDRbyPercent = 0x0063,
            LoyalItem = 0x0064,
            ConfuseMessage = 0x0065,
            RaceStealth = 0x0066,
            ClassStealth = 0x0067,
            DefenceModifier = 0x0068,
            Alter2ndAttack = 0x0069,
            Alter3rdAttack = 0x006A,
            BlindUser = 0x006B,
            AffectsLiving = 0x006C,
            Nonliving = 0x006D,
            NotGoodAligned = 0x006E,
            NotEvilAligned = 0x006F,
            NeutralAligned = 0x0070,
            NotNeutralAligned = 0x0071,
            AutoUseAbilities = 0x0072,
            DescriptionMessage = 0x0073,
            AlterBSAttack = 0x0074,
            AlterBSMinimum = 0x0075,
            AlterBSMaximum = 0x0076,
            DeleteAtCleanup = 0x0077,
            StartingMessage = 0x0078,
            CleanupRecharge = 0x0079,
            RemoveSpell = 0x007A,
            AlterHealingRate = 0x007B,
            NegateAbility = 0x007C,
            IceSorceressThroneQuest = 0x007D,
            GoodQuest = 0x007E,
            NeutralQuest = 0x007F,
            EvilQuest = 0x0080,
            HighDruidGemQuest = 0x0081,
            ChampionOfBloodAltarQuest = 0x0082,
            AdultSheDragonRubyQuest = 0x0083,
            MOD3_WereratContraptionQuest = 0x0084,
            MOD5_LowLevelQuest = 0x0085,
            MOD6_DesertQuest = 0x0086,
            MinimumLevel = 0x0087,
            MaximumLevel = 0x0088,
            ShockshieldMessage = 0x0089,
            VisiblePlacedItem = 0x008A,
            SpellImmunity = 0x008B,
            TeleportRoom = 0x008C,
            TeleportMap = 0x008D,
            HitMagical = 0x008E,
            ClearItem = 0x008F,
            NonMagicalSpell = 0x0090,
            ManaRegeneration = 0x0091,
            GuardedBy = 0x0092,
            ResistWater = 0x0093,
            TriggerTextblock = 0x0094,
            DeleteFromInventoryAtCleanup = 0x0095,
            HealManaOrKai = 0x0096,
            CastOnEnding = 0x0097,
            Rune = 0x0098,
            TerminateSpell = 0x0099,
            RemainVisibleAtCleanup = 0x009A,
            DeathTextblock = 0x009B,
            QuestItem = 0x009C,
            ScatterItems = 0x009D,
            RequiredToHit = 0x009E,
            KaiBind = 0x009F,
            GiveSpellTemporarily = 0x00A0,
            OpenDoor = 0x00A1,
            Lore = 0x00A2,
            SpellComponent = 0x00A3,
            CastOnEndingChance = 0x00A4,
            AlterSpellDamage = 0x00A5,
            AlterSpellDuration = 0x00A6,
            UnequipItem = 0x00A7,
            EquipItem = 0x00A8,
            CannotWearLocation = 0x00A9,
            PutToSleep = 0x00AA,
            Invisibility = 0x00AB,
            SeeInvisible = 0x00AC,
            Scry = 0x00AD,
            StealMana = 0x00AE,
            StealHPToMana = 0x00AF,
            StealManaToHP = 0x00B0,
            SpellColours = 0x00B1,
            Shadowform = 0x00B2,
            FindTrapsValue = 0x00B3,
            PicklocksValue = 0x00B4,
            GangHouseDeed = 0x00B5,
            GangHouseTax = 0x00B6,
            GangHouseItem = 0x00B7,
            GangShopController = 0x00B8,
            DoNotAttackIfItem = 0x00B9,
            PerfectStealth = 0x00BA,
            Meditate = 0x00BB,

            ClassStealthGypsy = 0x0167,
            ClassStealthMissionary = 0x0267,
            ClassStealthBard = 0x0467,
            ClassStealthMystic = 0x0567,

            BashMagely = 0x011F,
            BashPriestly = 0x021F,
            BashDruidly = 0x031F,
        }
    }
}
