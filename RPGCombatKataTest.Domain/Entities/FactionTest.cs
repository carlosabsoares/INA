using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPGCombatKata.Domain.Entities;

namespace RPGCombatKataTest.Domain.Entities
{
    [TestClass]
    public class FactionTest
    {
        [TestMethod]
        [TestCategory("Character")]
        public void ValidaCriacaoDeFaccao()
        {
            string _nomeFaction = "Titans";
            var _gangs = new AbsFaction(_nomeFaction);

            Assert.AreEqual(_gangs.Name, _nomeFaction);
        }

        private class AbsFaction : Faction
        {
            public override string Name { get; }

            public AbsFaction(string nome)
            {
                Name = nome;
            }
        }
    }
}