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
            var _gangs = new IFaction(_nomeFaction);

            Assert.AreEqual(_gangs.Name, _nomeFaction);
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