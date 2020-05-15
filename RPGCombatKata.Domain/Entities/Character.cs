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

        private readonly int _initialHealth = 1000;
        private readonly int _initialLevel = 1;
        private readonly bool _alive = true;


        public Character()
        {
            Health = _initialHealth;
            Alive = _alive;
            Level = _initialLevel;
        }

        //public void DownHealth(int down)
        //{
        //    Health -= down;
        //}

        //public void UpHealth(int up)
        //{
        //    Health -= up;
        //}

        public void DownLevel(int down)
        {
            Level -= down;
        }

        public void UpLevel(int up)
        {
            Level += up;
        }

        public void DownHealth(int down)
        {
            if ((Health - down) > 0)
            {
                Health -= down;
                Level = (Health < 1000)? 1: (int)(Math.Round(Convert.ToDecimal((Health / down))));

            }
            else
            {
                Health = 0;
                Level = 0;
                Alive = false;
            }
        }

        public void UpHealth(int up)
        {
            Health += up;

            if (Health > _initialHealth)
            {
                Level = (int)(Math.Round(Convert.ToDecimal((Health / _initialHealth)))); 
            }
        }

        public void BeCure()
        {

            if (Health > _initialHealth && Level > _initialLevel)
            {
                Level --;
                Health = _initialHealth;
            }
        }

    }
}
