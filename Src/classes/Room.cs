using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_Jahre_Hoelle.classes
{
    public class Room
    {
        // 14x7 mit je 120px
        private List<List<char>> room_matrix {  get; set; }
        //public List<enemy> { get; private set; }
        public bool IsCleared { get; private set; } 

        Room()
        {
            IsCleared = false;
        }


    }
}
