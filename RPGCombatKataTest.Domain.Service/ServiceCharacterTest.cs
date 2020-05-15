﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPGCombatKata.Domain.Entities;
using RPGCombatKata.Domain.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGCombatKataTest.Domain.Service
{
    [TestClass]
    public class ServiceCharacterTest
    {
     
        private Character _character = new Character() { Name = "Person" };

        private readonly int _originalHealt = 1000;
        private readonly int _originalLevel = 1;

        [TestMethod]
        [TestCategory("ServiceCharacter")]
        public void validaAtaqueFeitoPersonagemContraOutroPersonagem()
        {
            string otherName = "OtherPerson";
            var _potenciaAtaque = 10;

            var _otherCharacter = new Character() { Name = otherName };

            var serviceCharacter = new ServiceCharacter(_character);

            serviceCharacter.MakeAttack(_potenciaAtaque, _otherCharacter);

            Assert.AreEqual(_otherCharacter.Health, (_character.Health - _potenciaAtaque));
            Assert.AreEqual(_otherCharacter.Name, otherName);
            Assert.AreEqual(_otherCharacter.Alive, true);
            Assert.AreNotEqual(_otherCharacter.Alive, false);

        }

        [TestMethod]
        [TestCategory("ServiceCharacter")]
        public void validaAtaqueFeitoPersonagemContraOMesmoPersonagem()
        {
            var _otherCharacter = new Character();
            
            var serviceCharacter = new ServiceCharacter(_character);
            var _potenciaAtaque = 10;

            try
            {
                serviceCharacter.MakeAttack(_potenciaAtaque, _character);
            }
            catch (Exception ex)
            {

                Assert.IsNotNull(ex);
                AssertFailedException.Equals(ex.Message,"You cant attack yourself.");

                Assert.AreEqual(_character.Health, 1000);
                Assert.AreEqual(_character.Level, 1);
                Assert.AreEqual(_character.Alive, true);
                Assert.AreNotEqual(_character.Alive, false);

            }
        }

        [TestMethod]
        [TestCategory("ServiceCharacter")]
        public void validaAtaqueSofridoPersonagemPorOutroPersonagem()
        {
            string otherName = "OtherPerson";
            var _potenciaAtaque = 10;

            var _otherCharacter = new Character() { Name = otherName };


            var serviceCharacter = new ServiceCharacter(_character);

            serviceCharacter.GetAttack(_potenciaAtaque, _otherCharacter);

            Assert.AreEqual(_character.Health, (_originalHealt - _potenciaAtaque));
            Assert.AreEqual(_character.Level, _originalLevel);
            Assert.AreEqual(_character.Alive, true);
            Assert.AreNotEqual(_character.Alive, false);

        }

        [TestMethod]
        [TestCategory("ServiceCharacter")]
        public void validaAtaqueSofridoPersonagemPorOutroPersonagemComMorte()
        {
            string otherName = "OtherPerson";

            var _otherCharacter = new Character() { Name = otherName };
            var _potenciaAtaque = 1000;

            var serviceCharacter = new ServiceCharacter(_character);

            serviceCharacter.GetAttack(_potenciaAtaque, _otherCharacter);

            Assert.AreEqual(_character.Health, (_originalHealt - _potenciaAtaque));
            Assert.AreEqual(_character.Level, 0);
            Assert.AreEqual(_character.Alive, false);
            Assert.AreNotEqual(_character.Alive, true);

        }

        [TestMethod]
        [TestCategory("ServiceCharacter")]
        public void validaAtaqueSofridoPersonagemPeloMesmoPersonagem()
        {
            var _potenciaAtaque = 10;

            var serviceCharacter = new ServiceCharacter(_character);

            try
            {
                serviceCharacter.GetAttack(_potenciaAtaque, _character);
            }
            catch (Exception ex)
            {

                Assert.IsNotNull(ex);
                AssertFailedException.Equals(ex.Message, "You cant attack yourself.");

                Assert.AreEqual(_character.Health, _originalHealt);
                Assert.AreEqual(_character.Level, _originalLevel);
                Assert.AreEqual(_character.Alive, true);
                Assert.AreNotEqual(_character.Alive, false);

            }
        }

        [TestMethod]
        [TestCategory("ServiceCharacter")]
        public void validaCuraFeitaPeloProprioPersonagemComSucesso()
        {
            var _newHealth = 1000;

            _character.UpHealth(_newHealth);

            var serviceCharacter = new ServiceCharacter(_character);

            serviceCharacter.BeCure(_character);

            Assert.AreEqual(_character.Health, _originalHealt);
            Assert.AreEqual(_character.Level, _originalLevel);
            Assert.AreEqual(_character.Alive, true);
            Assert.AreNotEqual(_character.Alive, false);


        }

        [TestMethod]
        [TestCategory("ServiceCharacter")]
        public void validaCuraFeitaPeloProprioPersonagemComErro()
        {

            string otherName = "OtherPerson";

            var _otherCharacter = new Character() { Name = otherName };

            var _newHealth = 1000;

            _character.UpHealth(_newHealth);

            var serviceCharacter = new ServiceCharacter(_character);

            try
            {
                serviceCharacter.BeCure(_otherCharacter);
            }
            catch (Exception ex)
            {

                Assert.IsNotNull(ex);
                AssertFailedException.Equals(ex.Message, "One character cant heal another character.");

                _character.DownHealth(_newHealth);

                Assert.AreEqual(_character.Health, _originalHealt );
                Assert.AreEqual(_character.Level, _originalLevel);
                Assert.AreEqual(_character.Alive, true);
                Assert.AreNotEqual(_character.Alive, false);

            }
        }

        [TestMethod]
        [TestCategory("ServiceCharacter")]
        public void validaCuraFeitaPeloProprioPersonagemQueEstaMortoComErro()
        {
            var _newHealth = 1000;

            _character.DownHealth(_newHealth);

            var serviceCharacter = new ServiceCharacter(_character);

            try
            {
                serviceCharacter.BeCure(_character);
            }
            catch (Exception ex)
            {

                Assert.IsNotNull(ex);
                AssertFailedException.Equals(ex.Message, "We cant cure a dead character.");

                Assert.AreEqual(_character.Health, 0);
                Assert.AreEqual(_character.Level, 0);
                Assert.AreEqual(_character.Alive, false);
                Assert.AreNotEqual(_character.Alive, true);

            }


        }
    }
}
