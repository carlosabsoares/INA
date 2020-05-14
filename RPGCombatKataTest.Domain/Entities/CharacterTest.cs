using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPGCombatKata.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGCombatKataTest.Domain.Entities
{
    [TestClass]
    public class CharacterTest
    {

        [TestMethod]
        [TestCategory("Character")]
        public void Valida_Criacao_Character()
        {

            string _name = "Carlos";

            int _health = 1000;
            bool _alive = true;
            int _level = 1;

            var character = new Character() { Name = _name };

            Assert.AreEqual(character.Name, _name);
            Assert.AreEqual(character.Health, _health);
            Assert.AreEqual(character.Level, _level);
            Assert.AreEqual(character.Alive, _alive);
            Assert.AreNotEqual(character.Alive, false);

        }

    }
}
