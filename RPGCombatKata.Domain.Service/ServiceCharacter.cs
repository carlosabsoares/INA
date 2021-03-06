﻿using RPGCombatKata.Domain.Core.Interface.Servicos;
using RPGCombatKata.Domain.Entities;
using System;
using System.Linq;

namespace RPGCombatKata.Domain.Service
{
    public class ServiceCharacter : IServiceCharacter
    {
        private readonly Character _character = new Character();

        public ServiceCharacter(Character character)
        {
            _character = character;
        }

        private bool ValidateCure(Character character)
        {
            bool _retorno = false;

            if ((_character == character) && character.Alive && character.Level > 1)
                _retorno = true;

            if ((ValidateFaction(character)) && _character.Alive && _character.Level > 1)
                _retorno = true;

            return _retorno;
        }

        private bool ValidateGetAttack(int demage, Character opponent)
        {
            bool _retorno = false;

            if ((_character != opponent))
                _retorno = true;

            return _retorno;
        }

        private bool ValidateMakeAttack(int demage, Character opponent)
        {
            bool _retorno = false;

            if ((_character != opponent) &&
                ValidatePositionAttack(opponent) &&
                (!ValidateFaction(opponent))
                )
                _retorno = true;

            return _retorno;
        }

        private bool ValidateFaction(Character opponent)
        {
            bool _retorno = false;

            var teste = opponent.Factions.Where(x => _character.Factions.Any(c => c.Name == x.Name));

            if (teste.Count() > 0)
                _retorno = true;

            return _retorno;
        }

        private bool ValidatePositionAttack(Character opponent)
        {
            bool _retorno = false;

            var posicaoAtacante = (int)_character.KindOfFighter;

            if (posicaoAtacante >= opponent.Position)
                _retorno = true;

            return _retorno;
        }

        public void BeCure(Character character)
        {
            if (ValidateCure(character))
            {
                if (_character == character)
                {
                    character.DownHealth(character.InitialHealth);
                }
                else
                {
                    character.UpHealth(character.InitialHealth);
                    _character.DownHealth(character.InitialHealth);
                }
            }
            else
            {
                if (!character.Alive)
                    throw new Exception("We cant cure a dead character.");

                if ((_character != character))
                    throw new Exception("A character can only heal characters of his own faction.");
            }
        }

        public void GetAttack(int demage, Character opponent)
        {
            if (ValidateGetAttack(demage, opponent))
            {
                _character.DownHealth(demage);
            }
            else
            {
                MakeAttack(demage, opponent);
            }
        }

        public void MakeAttack(int powerful, object opponent)
        {
            if (opponent is Character)
            {
                var opponetChar = (Character)opponent;

                MakeAttackCharacter(powerful, opponetChar);
            }
            else if (opponent is Objects)
            {
                var opponetObj = (Objects)opponent;
                MakeAttackObjects(powerful, opponetObj);
            }
        }

        private void MakeAttackCharacter(int powerful, Character opponent)
        {
            if (ValidateMakeAttack(powerful, opponent))
            {
                opponent.DownHealth(VerifyDamageAttack(powerful, opponent));
            }
            else
            {
                if (_character == opponent)
                    throw new Exception("You cant attack yourself.");

                if (!ValidatePositionAttack(opponent))
                    throw new Exception("Opponent out of reach.");

                if (!ValidateFaction(opponent))
                    throw new Exception("You cant attack yours allies.");
            }
        }

        private void MakeAttackObjects(int powerful, Objects opponent)
        {
            if (!opponent.Destroyed)
            {
                opponent.DownHealth(powerful);
            }
            else
            {
                throw new Exception("Object is already destroyed.");
            }
        }

        private int VerifyDamageAttack(int powerful, Character opponent)
        {
            int _demagePowerful = powerful;

            var _levelDiff = opponent.Level - _character.Level;

            if (_levelDiff > 4)
            {
                _demagePowerful = (powerful / 2);
            }

            if (_levelDiff < 4 && _levelDiff < 0)
            {
                _demagePowerful = (powerful * 2);
            }

            return _demagePowerful;
        }
    }
}