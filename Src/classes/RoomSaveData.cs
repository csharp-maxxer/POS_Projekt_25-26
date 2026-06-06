using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_Jahre_Hoelle.classes
{
    public class RoomSaveData
    {
        public int Xpos { get; set; }
        public int Ypos { get; set; }
        public char Type { get; set; }
        public bool IsCleared { get; set; }
        public bool DoorTop { get; set; }
        public bool DoorRight { get; set; }
        public bool DoorBottom { get; set; }
        public bool DoorLeft { get; set; }
        public bool ExamRoom { get; set; }
        public bool ExamTriggered { get; set; }
        public List<EnemySaveData> Enemies { get; set; } = new List<EnemySaveData>();
        public List<List<char>> room_matrix { get; set; }
    }
}
