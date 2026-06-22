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
        public Vector Direction { get; set; }
        public double Damage { get; private set; }

        public double Speed { get; private set; }
        public double Range { get; private set; }
        public double X_pos { get; set; }

        public double Y_pos { get; set; }

        public double X_pos_created { get; set; }

        public double Y_pos_created { get; set; }

        public int random { get; set; }
        public double rotation { get; set; }

        public Bullets(double damage, double speed, double range, Vector direction, double x_create, double y_created)
        {
            Logger.logger.Debug("bullet gets created");

            Random random_ = new Random();

            random = random_.Next(1, 3);
            rotation = 0;
            Damage = damage;
            Speed = speed;
            Range = range;
            Range = range;
            Direction = direction;
            X_pos_created = x_create;
            Y_pos_created = y_created;

            Logger.logger.Debug("bullet stats got set");
        }

        public Bullets()
        {
            
        }


    }
}