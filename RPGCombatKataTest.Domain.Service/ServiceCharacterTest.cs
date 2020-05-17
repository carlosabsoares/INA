using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPGCombatKata.Domain.Entities;
using RPGCombatKata.Domain.Enums;
using RPGCombatKata.Domain.Service;
using System;

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
        public void ValidaAtaqueFeitoPersonagemContraOutroPersonagem()
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

            Assert.AreNotEqual(_otherCharacter.Factions, _character.Factions);

            Assert.AreEqual(_otherCharacter.KindOfFighter, TypeOfFighter.Melee);
        }

        [TestMethod]
        [TestCategory("ServiceCharacter")]
        public void ValidaAtaqueFeitoPersonagemContraOMesmoPersonagem()
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
                AssertFailedException.Equals(ex.Message, "You cant attack yourself.");

                Assert.AreEqual(_character.Health, 1000);
                Assert.AreEqual(_character.Level, 1);
                Assert.AreEqual(_character.Alive, true);
                Assert.AreNotEqual(_character.Alive, false);
                Assert.AreEqual(_character.KindOfFighter, TypeOfFighter.Melee);
            }
        }

        [TestMethod]
        [TestCategory("ServiceCharacter")]
        public void ValidaAtaqueSofridoPersonagemPorOutroPersonagem()
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
            Assert.AreEqual(_character.KindOfFighter, TypeOfFighter.Melee);
        }

        [TestMethod]
        [TestCategory("ServiceCharacter")]
        public void ValidaAtaqueSofridoPersonagemPorOutroPersonagemComMorte()
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
            Assert.AreEqual(_character.KindOfFighter, TypeOfFighter.Melee);
        }

        [TestMethod]
        [TestCategory("ServiceCharacter")]
        public void ValidaAtaqueSofridoPersonagemPeloMesmoPersonagem()
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
                Assert.AreEqual(_character.KindOfFighter, TypeOfFighter.Melee);
            }
        }

        [TestMethod]
        [TestCategory("ServiceCharacter")]
        public void ValidaCuraFeitaPeloProprioPersonagemComSucesso()
        {
            var _newHealth = 1000;

            _character.UpHealth(_newHealth);

            var serviceCharacter = new ServiceCharacter(_character);

            serviceCharacter.BeCure(_character);

            Assert.AreEqual(_character.Health, _originalHealt);
            Assert.AreEqual(_character.Level, _originalLevel);
            Assert.AreEqual(_character.Alive, true);
            Assert.AreNotEqual(_character.Alive, false);
            Assert.AreEqual(_character.KindOfFighter, TypeOfFighter.Melee);
        }

        [TestMethod]
        [TestCategory("ServiceCharacter")]
        public void ValidaCuraFeitaPeloProprioPersonagemComErro()
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
                AssertFailedException.Equals(ex.Message, "A character can only heal characters of his own faction.");

                _character.DownHealth(_newHealth);

                Assert.AreEqual(_character.Health, _originalHealt);
                Assert.AreEqual(_character.Level, _originalLevel);
                Assert.AreEqual(_character.Alive, true);
                Assert.AreNotEqual(_character.Alive, false);
                Assert.AreEqual(_character.KindOfFighter, TypeOfFighter.Melee);
            }
        }

        [TestMethod]
        [TestCategory("ServiceCharacter")]
        public void ValidaCuraFeitaPeloProprioPersonagemQueEstaMortoComErro()
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
                Assert.AreEqual(_character.KindOfFighter, TypeOfFighter.Melee);
            }
        }

        [TestMethod]
        [TestCategory("ServiceCharacter")]
        public void ValidaAtaqueFeitoPersonagemPorOutroPersonagemNivel5VezesMaior()
        {
            string otherName = "OtherPerson";
            var _potenciaAtaque = 10;

            var _otherCharacter = new Character() { Name = otherName };
            var _newHealth = 5000;

            _otherCharacter.UpHealth(_newHealth);

            var serviceCharacter = new ServiceCharacter(_character);

            serviceCharacter.MakeAttack(_potenciaAtaque, _otherCharacter);

            Assert.AreEqual(_otherCharacter.Health, ((_originalHealt + _newHealth) - (_potenciaAtaque / 2)));
            Assert.AreEqual(_character.Level, _originalLevel);
            Assert.AreEqual(_character.Alive, true);
            Assert.AreNotEqual(_character.Alive, false);
            Assert.AreEqual(_character.KindOfFighter, TypeOfFighter.Melee);
        }

        [TestMethod]
        [TestCategory("ServiceCharacter")]
        public void ValidaAtaqueFeitoPersonagemPorOutroPersonagemNivel5VezesMenor()
        {
            string otherName = "OtherPerson";
            var _potenciaAtaque = 10;

            var _otherCharacter = new Character() { Name = otherName };
            var _newHealth = 5000;

            _character.UpHealth(_newHealth);

            var serviceCharacter = new ServiceCharacter(_character);

            serviceCharacter.MakeAttack(_potenciaAtaque, _otherCharacter);

            Assert.AreEqual(_otherCharacter.Health, (_originalHealt - (_potenciaAtaque * 2)));
            Assert.AreEqual(_otherCharacter.Level, _originalLevel);
            Assert.AreEqual(_otherCharacter.Alive, true);
            Assert.AreNotEqual(_otherCharacter.Alive, false);
            Assert.AreEqual(_character.KindOfFighter, TypeOfFighter.Melee);
        }

        [TestMethod]
        [TestCategory("ServiceCharacter")]
        public void ValidaAtaqueFeitoPersonagemPorOutroPersonagemMeleeForaAlcance()
        {
            string otherName = "OtherPerson";
            var _potenciaAtaque = 10;

            var _otherCharacter = new Character() { Name = otherName, Position = 30 };
            var _newHealth = 5000;

            _character.UpHealth(_newHealth);

            var serviceCharacter = new ServiceCharacter(_character);

            try
            {
                serviceCharacter.MakeAttack(_potenciaAtaque, _otherCharacter);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
                AssertFailedException.Equals(ex.Message, "Opponent out of reach.");
                Assert.AreEqual(_otherCharacter.Health, _originalHealt);
                Assert.AreEqual(_otherCharacter.Level, _originalLevel);
                Assert.AreEqual(_otherCharacter.Alive, true);
                Assert.AreNotEqual(_otherCharacter.Alive, false);
                Assert.AreEqual(_character.KindOfFighter, TypeOfFighter.Melee);
            }
        }

        [TestMethod]
        [TestCategory("ServiceCharacter")]
        public void ValidaAtaqueFeitoPersonagemPorOutroPersonagemRangedForaAlcance()
        {
            string otherName = "OtherPerson";
            var _potenciaAtaque = 10;

            var _otherCharacter = new Character(TypeOfFighter.Ranged) { Name = otherName, Position = 30 };
            var _newHealth = 5000;

            _character.UpHealth(_newHealth);

            var serviceCharacter = new ServiceCharacter(_character);

            try
            {
                serviceCharacter.MakeAttack(_potenciaAtaque, _otherCharacter);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
                AssertFailedException.Equals(ex.Message, "Opponent out of reach.");
                Assert.AreEqual(_otherCharacter.Health, _originalHealt);
                Assert.AreEqual(_otherCharacter.Level, _originalLevel);
                Assert.AreEqual(_otherCharacter.Alive, true);
                Assert.AreNotEqual(_otherCharacter.Alive, false);
                Assert.AreEqual(_character.KindOfFighter, TypeOfFighter.Melee);
            }
        }

        [TestMethod]
        [TestCategory("ServiceCharacter")]
        public void ValidaAtaqueFeitoPersonagemPorOutroPersonagemRangedDentroAlcance()
        {
            string otherName = "OtherPerson";
            var _potenciaAtaque = 10;

            var _character = new Character(TypeOfFighter.Ranged) { Name = otherName };

            var _otherCharacter = new Character() { Name = otherName, Position = 10 };

            var serviceCharacter = new ServiceCharacter(_character);

            serviceCharacter.MakeAttack(_potenciaAtaque, _otherCharacter);

            Assert.AreEqual(_otherCharacter.Health, (_originalHealt - _potenciaAtaque));
            Assert.AreEqual(_otherCharacter.Level, _originalLevel);
            Assert.AreEqual(_otherCharacter.Alive, true);
            Assert.AreNotEqual(_otherCharacter.Alive, false);
            Assert.AreEqual(_character.KindOfFighter, TypeOfFighter.Ranged);
        }

        [TestMethod]
        [TestCategory("ServiceCharacter")]
        public void ValidaAtaqueFeitoPersonagemContraOutroPersonagemDiferenteFacao()
        {
            string _nameFaction = "Warriors";
            string _otherNameFaction = "Raptors";

            _character.JoinFaction(new IFaction(_nameFaction));

            string otherName = "OtherPerson";
            var _potenciaAtaque = 10;

            var _otherCharacter = new Character() { Name = otherName };

            _otherCharacter.JoinFaction(new IFaction(_nameFaction));
            _otherCharacter.JoinFaction(new IFaction(_otherNameFaction));

            _otherCharacter.LeaveFaction(new IFaction(_otherNameFaction));

            var serviceCharacter = new ServiceCharacter(_character);

            try
            {
                serviceCharacter.MakeAttack(_potenciaAtaque, _otherCharacter);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
                AssertFailedException.Equals(ex.Message, "You cant attack yours allies.");
                Assert.AreEqual(_otherCharacter.Health, _originalHealt);
                Assert.AreEqual(_otherCharacter.Level, _originalLevel);
                Assert.AreEqual(_otherCharacter.Alive, true);
                Assert.AreNotEqual(_otherCharacter.Alive, false);
                Assert.AreEqual(_character.KindOfFighter, TypeOfFighter.Melee);
                Assert.AreEqual(_otherCharacter.KindOfFighter, TypeOfFighter.Melee);
            }
        }

        [TestMethod]
        [TestCategory("ServiceCharacter")]
        public void ValidaAtaqueFeitoPersonagemContraOutroPersonagemMesmaFacao()
        {
            string _nameFaction = "Warriors";
            string _otherNameFaction = "Raptors";

            _character.JoinFaction(new IFaction(_nameFaction));

            string otherName = "OtherPerson";
            var _potenciaAtaque = 10;

            var _otherCharacter = new Character() { Name = otherName };
            _otherCharacter.JoinFaction(new IFaction(_otherNameFaction));

            var serviceCharacter = new ServiceCharacter(_character);

            serviceCharacter.MakeAttack(_potenciaAtaque, _otherCharacter);

            Assert.AreEqual(_otherCharacter.Health, (_character.Health - _potenciaAtaque));
            Assert.AreEqual(_otherCharacter.Name, otherName);
            Assert.AreEqual(_otherCharacter.Alive, true);
            Assert.AreNotEqual(_otherCharacter.Alive, false);

            Assert.AreNotEqual(_otherCharacter.Factions, _character.Factions);

            Assert.AreEqual(_otherCharacter.KindOfFighter, TypeOfFighter.Melee);
        }

        [TestMethod]
        [TestCategory("ServiceCharacter")]
        public void ValidaCuraFeitaPersonagemParaOutroComSucesso()
        {
            string _nameFaction = "Warriors";

            _character.JoinFaction(new IFaction(_nameFaction));

            string otherName = "OtherPerson";

            var _otherCharacter = new Character() { Name = otherName };
            _otherCharacter.JoinFaction(new IFaction(_nameFaction));

            var serviceCharacter = new ServiceCharacter(_character);

            var _newHealth = 1000;

            _character.UpHealth(_newHealth);

            serviceCharacter.BeCure(_otherCharacter);

            Assert.AreEqual(_character.Health, _originalHealt);
            Assert.AreEqual(_character.Level, _originalLevel);
            Assert.AreEqual(_character.Alive, true);
            Assert.AreNotEqual(_character.Alive, false);
            Assert.AreEqual(_character.KindOfFighter, TypeOfFighter.Melee);
        }

        [TestMethod]
        [TestCategory("ServiceCharacter")]
        public void ValidaCuraFeitaPersonagemParaOutroSemSucesso()
        {
            string _nameFaction = "Warriors";
            string _otherNameFaction = "Raptors";

            _character.JoinFaction(new IFaction(_nameFaction));

            string otherName = "OtherPerson";

            var _otherCharacter = new Character() { Name = otherName };
            _otherCharacter.JoinFaction(new IFaction(_otherNameFaction));

            var serviceCharacter = new ServiceCharacter(_character);

            var _newHealth = 1000;

            _character.UpHealth(_newHealth);

            try
            {
                serviceCharacter.BeCure(_otherCharacter);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
                AssertFailedException.Equals(ex.Message, "A character can only heal characters of his own faction.");

                _character.DownHealth(_newHealth);

                Assert.AreEqual(_character.Health, _originalHealt);
                Assert.AreEqual(_character.Level, _originalLevel);
                Assert.AreEqual(_character.Alive, true);
                Assert.AreNotEqual(_character.Alive, false);
                Assert.AreEqual(_character.KindOfFighter, TypeOfFighter.Melee);
            }
        }

        [TestMethod]
        [TestCategory("ServiceCharacter")]
        public void ValidaAtaqueFeitoAUmObjetoComSucesso()
        {
            //string otherName = "OtherPerson";
            var _potenciaAtaque = 10;

            var _object = new Objects(_originalHealt);

            //var _otherCharacter = new Character() { Name = otherName };

            var serviceCharacter = new ServiceCharacter(_character);

            serviceCharacter.MakeAttack(_potenciaAtaque, _object);

            Assert.AreEqual(_object.Health, (_originalHealt - _potenciaAtaque));
            Assert.AreEqual(_object.Destroyed, false);
        }

        [TestMethod]
        [TestCategory("ServiceCharacter")]
        public void ValidaAtaqueFeitoAUmObjetoSemSucesso()
        {
            var _potenciaAtaque = 10;

            var _object = new Objects(_originalHealt);

            _object.DownHealth(_originalHealt);

            var serviceCharacter = new ServiceCharacter(_character);

            try
            {
                serviceCharacter.MakeAttack(_potenciaAtaque, _object);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
                AssertFailedException.Equals(ex.Message, "Object is already destroyed.");

                Assert.AreEqual(_object.Destroyed, true);
            }
        }

        [TestMethod]
        [TestCategory("ServiceCharacter")]
        public void ValidaAtaqueFeitoAUmObjetoDestuindo()
        {
            var _potenciaAtaque = _originalHealt;

            var _object = new Objects(_originalHealt);

            var serviceCharacter = new ServiceCharacter(_character);

            serviceCharacter.MakeAttack(_potenciaAtaque, _object);

            Assert.AreEqual(_object.Destroyed, true);
            Assert.AreEqual(_object.Health, 0);
        }

        private class IFaction : Faction
        {
            public override string Name { get; }

            public IFaction(string nome)
            {
                Name = nome;
            }
        }
    }
}