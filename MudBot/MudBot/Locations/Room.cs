namespace MudBot.Locations
{
    internal class Room
    {
        private int MapID;
        private int RoomID;
        private string Name;
        private int ShopID;
        private int NpcID;
        private int CommandID;
        private string Lair;
        private string Placed;

        private enum Commands
        {
            goPortal,
            goPath
        };
    }
}
