using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace _5_Jahre_Hoelle.classes
{
    public class Bullets
    {
        public Vector Direction {  get; private set; }
        public double Damage { get; private set; }

        public double Speed {  get; private set; }
        public double Range { get; private set; }
        public double X_pos { get; private set; }

        public double Y_pos { get; private set; } 

        public Bullets(double damage, double speed, double range, Vector direction)
        {
            Speed = speed;
            Range = range;
            Range = range;
            Direction = direction;
        }


    } 
}
