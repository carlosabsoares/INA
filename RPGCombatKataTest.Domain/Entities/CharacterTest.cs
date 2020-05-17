using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPGCombatKata.Domain.Entities;
using RPGCombatKata.Domain.Enums;

namespace RPGCombatKataTest.Domain.Entities
{
    [TestClass]
    public class CharacterTest
    {
        [TestMethod]
        [TestCategory("Character")]
        public void ValidaCriacaoCharacter()
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
            Assert.AreEqual(character.KindOfFighter, TypeOfFighter.Melee);
            Assert.AreNotEqual(character.KindOfFighter, TypeOfFighter.Ranged);
        }

        [TestMethod]
        [TestCategory("Character")]
        public void ValidaEntradaFaction()
        {
            string _name = "Carlos";
            string _nomeFaction = "Titans";

            int _health = 1000;
            bool _alive = true;
            int _level = 1;

            var character = new Character() { Name = _name };
            var _gangs = new IFaction(_nomeFaction);


            character.JoinFaction(_gangs);

            Assert.AreEqual(character.Name, _name);
            Assert.AreEqual(character.Health, _health);
            Assert.AreEqual(character.Level, _level);
            Assert.AreEqual(character.Alive, _alive);
            Assert.AreEqual(character.Factions[0].Name, _gangs.Name);
            Assert.AreNotEqual(character.Alive, false);
            Assert.AreEqual(character.KindOfFighter, TypeOfFighter.Melee);
            Assert.AreNotEqual(character.KindOfFighter, TypeOfFighter.Ranged);
        }

        [TestMethod]
        [TestCategory("Character")]
        public void ValidaSaidaFaction()
        {
            string _name = "Carlos";
            string _nomeFaction = "Titans";

            int _health = 1000;
            bool _alive = true;
            int _level = 1;

            var character = new Character() { Name = _name };
            var _gangs = new IFaction(_nomeFaction);


            character.JoinFaction(_gangs);
            character.LeaveFaction(_gangs);

            Assert.AreEqual(character.Name, _name);
            Assert.AreEqual(character.Health, _health);
            Assert.AreEqual(character.Level, _level);
            Assert.AreEqual(character.Alive, _alive);
            Assert.AreEqual(character.Factions.Count,0);
            Assert.AreNotEqual(character.Alive, false);
            Assert.AreEqual(character.KindOfFighter, TypeOfFighter.Melee);
            Assert.AreNotEqual(character.KindOfFighter, TypeOfFighter.Ranged);
        }

        [TestMethod]
        [TestCategory("Character")]
        public void ValidaSubidaDeEnergia()
        {
            string _name = "Carlos";

            int _health = 1000;
            bool _alive = true;
            int _level = 1;

            var character = new Character() { Name = _name };

            character.UpHealth(_health);

            Assert.AreEqual(character.Name, _name);
            Assert.AreEqual(character.Health, (_health + _health));
            Assert.AreEqual(character.Level, (++_level));
            Assert.AreEqual(character.Alive, _alive);
            Assert.AreNotEqual(character.Alive, false);
            Assert.AreEqual(character.KindOfFighter, TypeOfFighter.Melee);
            Assert.AreNotEqual(character.KindOfFighter, TypeOfFighter.Ranged);
        }

        [TestMethod]
        [TestCategory("Character")]
        public void ValidaDescidaDeEnergia()
        {
            string _name = "Carlos";

            int _health = 1000;
            bool _alive = false;
            int _level = 1;

            var character = new Character() { Name = _name };

            character.DownHealth(_health);

            Assert.AreEqual(character.Name, _name);
            Assert.AreEqual(character.Health, (_health - _health));
            Assert.AreEqual(character.Level, (--_level));
            Assert.AreEqual(character.Alive, _alive);
            Assert.AreNotEqual(character.Alive, true);
            Assert.AreEqual(character.KindOfFighter, TypeOfFighter.Melee);
            Assert.AreNotEqual(character.KindOfFighter, TypeOfFighter.Ranged);
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