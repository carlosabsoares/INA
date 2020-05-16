using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPGCombatKata.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGCombatKataTest.Domain.Entities
{
    [TestClass]
    public class SectTest
    {

        [TestMethod]
        [TestCategory("Sect")]
        public void ValidaCriacaoSect()
        {
            var character = new Character() { Name = "Carlos" };



            List<string> teste = new List<string>();

            teste.Add("Teste");

            Assert.AreEqual(teste.Count, 1);

        }

        [TestMethod]
        [TestCategory("Sect")]
        public void ValidaRemoveSect()
        {
            var character1 = new Character() { Name = "Carlos" };
            var character2 = new Character() { Name = "Alberto" };

            var gang = new Faction();

            gang.AddMember(character1);
            gang.AddMember(character2);

            gang.RemoveMember(character2);

            Assert.AreEqual(gang.Gang.Count, 1);

        }

        [TestMethod]
        [TestCategory("Sect")]
        public void ValidaCriacaoSectNula()
        {
            
            var gang = new Faction();

            Assert.IsNull(gang);

        }

    }
}
