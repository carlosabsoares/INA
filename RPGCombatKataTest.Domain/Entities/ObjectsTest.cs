using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPGCombatKata.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGCombatKataTest.Domain.Entities
{
    [TestClass]
    public class ObjectsTest
    {
        [TestMethod]
        [TestCategory("Objects")]
        public void ValidaCriacaoObjet()
        {
            int _originalHealth = 1000;

            Objects _objects = new Objects(_originalHealth);

            Assert.AreEqual(_objects.Health,_originalHealth);
            Assert.IsFalse(_objects.Destroyed);

        }

        [TestMethod]
        [TestCategory("Objects")]
        public void ValidaDiminuirHealthObjet()
        {
            int _originalHealth = 1000;
            int _powerDown = 50;

            Objects _objects = new Objects(_originalHealth);

            _objects.DownHealth(_powerDown);

            Assert.AreEqual(_objects.Health, (_originalHealth - _powerDown));
            Assert.IsFalse(_objects.Destroyed);

        }

        [TestMethod]
        [TestCategory("Objects")]
        public void ValidaDestuirHealthObjet()
        {
            int _originalHealth = 1000;
            int _powerDown = 1000;

            Objects _objects = new Objects(_originalHealth);

            _objects.DownHealth(_powerDown);

            Assert.AreEqual(_objects.Health, (_originalHealth - _powerDown));
            Assert.IsTrue(_objects.Destroyed);

        }
    }
}
