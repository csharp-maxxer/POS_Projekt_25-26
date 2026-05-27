using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _5_Jahre_Hoelle.classes
{
    public class Enemy
    {
        public List<Bullets> Bullets { get; set; } = new List<Bullets>();
        public double Damage { get; set; }
        public double Speed { get; set; }
        public double Range { get; set; }
        public double Firerate { get; set; }
        public double X_Pos { get; set; }
        public double Y_Pos { get; set; }
        public double Health { get; set; }
        private double shootTimer = 0;

        public Enemy(double damage, double speed, double range, double firerate, double health)
        {
            Damage = damage;
            Speed = speed;
            Range = range;
            Firerate = firerate;
            Health = health;
            Bullets = new List<Bullets>();
        }

        public void Move(Player player, double deltaTime, List<Enemy>AllEnemies)
        {
            double direction_x = player.X_Pos - X_Pos;
            double direction_y = player.Y_Pos - Y_Pos;

            Vector move_direction = new Vector(direction_x, direction_y);

            if (move_direction.Length > 0)
            {
                move_direction.Normalize();
            }

            // claude. kannst du bitte seperation machen?
            foreach (Enemy other in AllEnemies)
            {
                if (other == this) continue;

                double dx = X_Pos - other.X_Pos;
                double dy = Y_Pos - other.Y_Pos;
                double distance = Math.Sqrt(dx * dx + dy * dy);

                if (distance < 60 && distance > 0)
                {
                    move_direction.X += (dx / distance) * 2;
                    move_direction.Y += (dy / distance) * 2;
                }
            }
            // claude ende

            if (move_direction.Length > 0)
            {
                move_direction.Normalize();
            }

            X_Pos += move_direction.X * Speed * deltaTime;
            Y_Pos += move_direction.Y * Speed * deltaTime;
        }

        public void Shoot(Player player, double deltaTime)
        {
            shootTimer -= deltaTime;

            if (shootTimer > 0)
                return;

            shootTimer = Firerate;

            double direction_x = player.X_Pos - X_Pos;
            double direction_y = player.Y_Pos - Y_Pos;

            Vector shoot_direction = new Vector(direction_x, direction_y);

            if (shoot_direction.Length > 0)
            {
                shoot_direction.Normalize();
            }

            Bullets bullet = new Bullets(
                Damage,
                Firerate,
                Range,
                shoot_direction * 6
            );

            bullet.X_pos = X_Pos + 25;
            bullet.Y_pos = Y_Pos + 25;

            Bullets.Add(bullet);
        }
    }
}
