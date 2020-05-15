using RPGCombatKata.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGCombatKata.Domain.Core.Interface.Servicos
{
    public interface IServiceCharacter
    {
        void MakeAttack(int powerful, Character opponent);
        void GetAttack(int demage);

    }
}
