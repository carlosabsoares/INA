using RPGCombatKata.Domain.Enums;
using System;
using System.Collections.Generic;

namespace RPGCombatKata.Domain.Entities
{
    public class Character: IHealth
    {
        public string Name { get; set; }
        public int Position { get; set; }

        public int Health { get; private set; }
        public int Level { get; private set; }
        public bool Alive { get; private set; }
        public IList<Faction> Factions { get; }
        public int InitialHealth { get; private set; }


        public TypeOfFighter KindOfFighter { get; private set; }

        private readonly int _initialHealth = 1000;
        private readonly int _initialLevel = 1;
        private readonly bool _alive = true;
        private readonly int _initialPosition = 1;


        public Character(TypeOfFighter typeOfFighter = TypeOfFighter.Melee)
        {
            Health = _initialHealth;
            Alive = _alive;
            Level = _initialLevel;
            KindOfFighter = typeOfFighter;
            Position = _initialPosition;
            Factions = new List<Faction>();

            InitialHealth = _initialHealth;

        }

        public void JoinFaction(Faction faction)
        {
            Factions.Add(faction);
        }

        public void LeaveFaction(Faction faction)
        {
            Factions.Remove(faction);
        }

        public void DownHealth(int down)
        {
            int _varCalculo = 0;

            if (down < 1000)
            {
                _varCalculo = (down + _initialHealth);
            }
            else
            {
                _varCalculo = down;
            }

            if ((Health - down) > 0)
            {
                Health -= down;
                Level = (Health < 1000) ? 1 : (int)(Math.Round(Convert.ToDecimal((Health / _varCalculo))));
            }
            else
            {
                Dead();
            }
        }

        private void Dead()
        {
            Health = 0;
            Level = 0;
            Alive = false;
        }

        public void UpHealth(int up)
        {
            Health += up;

            if (Health > _initialHealth)
            {
                Level = (int)(Math.Round(Convert.ToDecimal((Health / _initialHealth))));
            }
        }
    }


}