using System;
using System.Collections.Generic;
using System.Text;


namespace RPGCombatKata.Domain.Entities
{
    public class Objects : IHealth
    {

        public int Health { get; private set; }

        public bool Destroyed { get; private set; }
        private readonly bool _destroyed = false;


        public Objects(int _health)
        {
            Destroyed = _destroyed;
            Health = _health;
        }


        public void DownHealth(int down)
        {
            if ((Health - down) > 0)
            {
                Health -= down;
            }
            else
            {
                Destroy();
            }
        }

        private void Destroy()
        {
            Health = 0;
            Destroyed = true;
        }
    }
}
