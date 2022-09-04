using static MajorMUD.Interfaces.Common;


namespace MajorMUD.Interfaces
{
    public interface IRoom
    {
        public string Code { get; set; }
        public string Checksum { get; set; }
        public string Name { get; set; }
        public int WCCMap { get; set; }
        public int WCCRoom { get; set; }
        public string Group { get; set; }
        public string Map { get; set; }
        public Flags RoomFlags { get; set; }
        public Exits AvailableExits { get; set; }
        public int MinimumLevel { get; set; }
        public int MaximumLevel { get; set; }
        public int RestrictedToClassId { get; set; }
    }
}
