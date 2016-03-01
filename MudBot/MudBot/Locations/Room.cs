using System;
using System.Collections.Generic;
using System.Text;

namespace MudBot.Locations {
    class Room {
        int MapID;
        int RoomID;
        string Name;
        int ShopID;
        int NpcID;
        int CommandID;
        string Lair;
        string Placed;

        private enum Commands {
            goPortal,
            goPath
        };
    }
}
