using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_Jahre_Hoelle.classes
{
    public class SaveGame
    {
        public double PlayerDamage { get; set; }
        public double PlayerSpeed { get; set; }
        public double PlayerRange { get; set; }
        public double PlayerFirerate { get; set; }
        public double PlayerGrade { get; set; }
        public double PlayerEnergy { get; set; }
        public double PlayerX { get; set; }
        public double PlayerY { get; set; }

        public int CurrentRoomX { get; set; }
        public int CurrentRoomY { get; set; }

        public List<RoomSaveData> Rooms { get; set; }

        public bool HasBoss { get; set; }
        public double BossX { get; set; }
        public double BossY { get; set; }
        public int QuestionCount { get; set; }
    }
}
