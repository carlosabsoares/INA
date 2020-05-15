using RPGCombatKata.Domain.Core.Interface.Servicos;
using RPGCombatKata.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGCombatKata.Domain.Service
{
    public class ServiceCharacter : IServiceCharacter
    {

        private readonly Character _character = new Character();

        public ServiceCharacter(Character character)
        {
            _character = character;
        }

        public void GetAttack(int demage)
        {
            _character.DownHealth(demage);
        }

        public void MakeAttack(int powerful, Character opponent)
        {
            if (!(_character == opponent))
            {
                opponent.DownHealth(powerful);
            }

        }
    }
}
