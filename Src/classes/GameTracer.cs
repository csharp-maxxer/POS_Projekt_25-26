using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _5_Jahre_Hoelle.classes
{
    public static class GameTracer
    {
        public static int LevelNumber { get; set; }

        public static int RoomsNumber
        {
            get
            {
                if (LevelNumber == 1)
                    return 4;
                if (LevelNumber == 2)
                    return 7;
                if (LevelNumber == 3)
                    return 9;
                if (LevelNumber == 4)
                    return 10;
                if (LevelNumber == 5)
                    return 11;
                if (LevelNumber == -10)
                    return 15;
                MessageBox.Show("error, keine stage existiert");
                return -1;
            }
            private set
            {
                
            }
        }
    }
}
