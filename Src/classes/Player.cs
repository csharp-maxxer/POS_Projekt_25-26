using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _5_Jahre_Hoelle.classes
{
    public class Player
    {
        public List<Bullets> Bullets;
        public double Damage {  get; set; }
        public double Speed { get; set; }
        public double Range { get; set; }
        public double Grade { get; set; }
        public double Energy { get; set; }
        public double Firerate { get; set; }
        public double X_Pos { get; set; } 
        public double Y_Pos { get; set; }

        public Player(double damage, double speed, double ramge, double firerate,int grade,double energy)
        {
            Damage = damage;
            Speed = speed;  
            Range = ramge;
            Firerate = firerate;
            Grade = grade;
            Energy = energy;
            X_Pos = 803;
            Y_Pos = 370;
            Bullets = new List<Bullets>();
        }

        
        public void Attack(Bullets bullet)
        {
            Bullets.Add(bullet);
        }
    }
}
