using _5_Jahre_Hoelle.subwindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Text;
using System.Threading.Tasks;

namespace _5_Jahre_Hoelle.classes
{
    public class Boss
    {
        public List<Bullets> Bullets = new List<Bullets>();
        public int QuestionCount {  get; private set; }
        public double X_Pos { get; set; }
        public double Y_Pos { get; set; }
        private double Damage {  get; set; }
        private double Speed { get; set; }
        private double Range { get; set; }
        private double Firerate { get; set; }
        private double shootTimer = 0;
        private double attackChoiceTimer = 0;        
        private Random random = new Random();  


        public Boss(double damage, double speed, double range, double firerate)
        {
            Damage = damage;
            Speed = speed;
            Range = range;
            Firerate = firerate;
            QuestionCount = 0;
        }

        public void Move()
        {

        }

        public void Attack(Player player, double deltaTime)
        {
            // 1 in 4 Chance auf Frage
            // Aus den 3/4 Chance eines der untere
            // 1/3 chance auf einen kreis aus 8 Bullets, 2/3 chance auf einen normalen schuss
            attackChoiceTimer -= deltaTime;

            if (attackChoiceTimer > 0)
                return;

            attackChoiceTimer = Firerate * 3;

            int decide_attack_question = random.Next(4);

            if (decide_attack_question == 0)
            {
                QuestionAttack(player);
            }
            else
            {
                int chose_attack = random.Next(3);

                if (chose_attack == 0)
                {
                    OctaAttack(deltaTime);
                }
                else
                {
                    NormalAttack(player, deltaTime);
                }
            }

        }

        private void QuestionAttack(Player player)
        {
            question_ask question_Ask = new question_ask(player, 1);
            question_Ask.ShowDialog();
            if (question_Ask.AwnserCorrect)
            {
                QuestionCount++;
            }
        }

        private void NormalAttack(Player player, double deltaTime)
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
                shoot_direction * 6,
                0,
                0
            );

            bullet.X_pos = X_Pos + 25;
            bullet.Y_pos = Y_Pos + 25;

            Bullets.Add(bullet);
        }

        private void OctaAttack(double deltaTime)
        {
            // KI: Chatgpt
            // Prompt: kannst du mir bitte eine c# liste mit vektoren machen wo in 8 richtungen zeigt wie im kompass
            // KI Anfang:
            List<Vector> directions = new List<Vector>
            {
                new Vector(0, -1),      // oben
                new Vector(0, 1),       // unten
                new Vector(-1, 0),      // links
                new Vector(1, 0),       // rechts
                new Vector(1, -1),      // oben rechts
                new Vector(-1, -1),     // oben links
                new Vector(1, 1),       // unten rechts
                new Vector(-1, 1)       // unten links
            };
            // KI Ende

            foreach (Vector dir in directions)
            {
                Vector normalized_vector = dir;
                normalized_vector.Normalize();

                Bullets bullet = new Bullets(
                    Damage,
                    Firerate,
                    Range,
                    normalized_vector * 6,
                    X_Pos + 25,
                    Y_Pos + 25
                );

                bullet.X_pos = X_Pos + 25;
                bullet.Y_pos = Y_Pos + 25;

                Bullets.Add(bullet);
            }
        }
    }
}
