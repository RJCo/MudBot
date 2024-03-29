﻿using System.Collections.Generic;
using static MajorMUD.Interfaces.Common;


namespace MajorMUD.Interfaces
{
    public interface IPath
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Filename { get; set; }
        public IItem ItemNeeded { get; set; }
        public IPath OnFailDo { get; set; }
        public IPath OnFinishDo { get; set; }
        public IRoom StartingRoom { get; set; }
        public IRoom EndingRoom { get; set; }
        public int RequiredGold { get; set; }
        public int LastExpRate { get; set; }
        public int StepCount { get; set; }
        public List<IRoom> Rooms { get; set; }
        public List<PathFlags> PathFlags { get; set; }
    }
}
