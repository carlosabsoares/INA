using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace RPGCombatKata.Domain.Entities
{
    public class Character
    {
        public string Name { get; set; }

        public int Health { get; private set; }
        public int Level { get; private set; }
        public bool Alive { get; private set; }

        public Character()
        {
            Health = 1000;
            Alive = true;
            Level = 1;
        }

        public void DownHealth(int down)
        {

            if ((Health-=down) > 0)
            {
                Health -= down;
            }
            else
            {
                Health = 0;
                Alive = false;
            }
        }

        public void UpHealth(int down)
        {
            if ((Health += down) > 1000)
            {
                Health += down;
            }
            else
            {
                Health = 1000;
            }
        }
    }
}
