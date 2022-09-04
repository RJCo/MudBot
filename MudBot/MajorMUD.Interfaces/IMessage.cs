using System.Collections.Generic;
using static MajorMUD.Interfaces.Common;


namespace MajorMUD.Interfaces
{
    public class IMessage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MessageText { get; set; }
        public string EndsWith { get; set; }
        public string Reply { get; set; }
        public List<MessageFlag> MessageFlags { get; set; }  // TODO:  Why is this a list of flags?
        public List<Effect> Effects { get; set; }
        public ActionToTake Action { get; set; }
    }
}
