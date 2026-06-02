using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_Jahre_Hoelle.classes
{
    public class Boss
    {
        public List<Bullets> Bullets = new List<Bullets>();
        public int QuestionCount {  get; private set; }
        public double X_pos { get; set; }
        public double Y_pos { get; set; }
        private double Damage {  get; set; }
        private double Speed { get; set; }
        private double Range { get; set; }
        private double Firerate { get; set; }


        public Boss(double damage, double speed, double range, double firerate)
        {
            Damage = damage;
            Speed = speed;
            Range = range;
            Firerate = firerate;
        }

        public void Move()
        {

        }

    }
}
