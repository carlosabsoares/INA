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

        private bool CureValidate(Character character)
        {
            bool _retorno = false;

            if((_character == character) && character.Alive && character.Level > 1)
                _retorno = true;

            return _retorno;
        }

        public bool GetAttackValidate(int demage, Character opponent)
        {

            bool _retorno = false;

            if (!(_character == opponent))
                _retorno = true;

            return _retorno;

        }

        public bool MakeAttackValidate(int demage, Character opponent)
        {

            bool _retorno = false;

            if (!(_character == opponent))
                _retorno = true;

            return _retorno;

        }

        public void BeCure(Character character)
        {

            if (CureValidate(character))
            {
                _character.BeCure();
            }
            else
            {
                if(!character.Alive)
                    throw new Exception("We cant cure a dead character.");

                if(!(_character == character))
                    throw new Exception("One character cant heal another character.");
            }
        }

        public void GetAttack(int demage, Character opponent)
        {
            if (GetAttackValidate(demage, opponent))
            {
                _character.DownHealth(demage);
            }
            else
            {
                MakeAttack(demage, opponent);
            }
        }

        public void MakeAttack(int powerful, Character opponent)
        {
            if (MakeAttackValidate(powerful, opponent))
            {
                opponent.DownHealth(VerifyDamageAttack(powerful, opponent));
            }
            else
            {
                if (_character == opponent)
                    throw new Exception("You cant attack yourself.");

            }
        }

        private int VerifyDamageAttack(int powerful, Character opponent)
        {

            int _demagePowerful = powerful;

            var _levelDiff = opponent.Level - _character.Level;

            if(_levelDiff > 4)
            {
                _demagePowerful = (powerful / 2);
            }

            if (_levelDiff < 4 && _levelDiff < 0 )
            {
                _demagePowerful = (powerful * 2);
            }

            return _demagePowerful;

        }

    }
}
