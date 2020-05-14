using System;
using System.Collections.Generic;
using System.Text;

namespace RPGCombatKata.Domain.Entities
{
    public class Characters
    {
        public string Name { get; set; }

        public int Health { get; private set; }
        public int Level { get; private set; }
        public bool Alive { get; private set; }

        public Characters()
        {
            Health = 1000;
            Alive = true;
            Level = 1;
        }

    }
}
