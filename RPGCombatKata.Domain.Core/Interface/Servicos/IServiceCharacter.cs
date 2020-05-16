using RPGCombatKata.Domain.Entities;

namespace RPGCombatKata.Domain.Core.Interface.Servicos
{
    public interface IServiceCharacter
    {
        void MakeAttack(int powerful, Character opponent);

        void GetAttack(int demage, Character opponent);

        void BeCure(Character character);
    }
}