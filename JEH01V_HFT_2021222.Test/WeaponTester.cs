using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JEH01V_HFT_2021222.Logic.Classes;
using JEH01V_HFT_2021222.Repository.Interfaces;
using JEH01V_HFT_2021222.Models;

namespace JEH01V_HFT_2021222.Test
{
    [TestFixture]
    public class WeaponTester
    {
        WeaponLogic wLogic;
        Mock<IRepository<Weapon>> mockWeaponRepository;

        [SetUp]
        public void Init()
        {
            mockWeaponRepository = new Mock<IRepository<Weapon>>();
            
            Character char1 = new Character(1, "Apple", "Pyro");
            Character char2 = new Character(2, "Berry", "Hydro");
            Character char3 = new Character(3, "Cherry", "Geo");

            mockWeaponRepository.Setup(w => w.ReadAll()).Returns(new List<Weapon>()
            {
                new Weapon()
                {
                    Id = 1,
                    Name = "Weapon1",
                    PeakDmg = 600,
                    Character = char1
                },
                new Weapon()
                {
                    Id = 2,
                    Name = "Weapon2",
                    PeakDmg = 500,
                    Character = char2
                },
                new Weapon()
                {
                    Id = 3,
                    Name = "Weapon3",
                    PeakDmg = 460,
                    Character = char3
                },
                new Weapon()
                {
                    Id = 4,
                    Name = "Weapon4",
                    PeakDmg = 400,
                    Character = char3
                }

            }.AsQueryable());
            wLogic = new WeaponLogic(mockWeaponRepository.Object);
        }

        #region Create Tests

        [TestCase("TestName", 1000, true)]
        [TestCase("TestName", 300, false)]
        [TestCase("TN", 400, false)]
        [TestCase("TN", 300, false)]
        public void CreateWeaponTests(string name, int dmg, bool shouldRun)
        {
            Weapon testWeapon = new Weapon() { Name = name , PeakDmg = dmg};
            if (shouldRun)
            {
                wLogic.Create(testWeapon);
                //Assert.That(() => wLogic.Create(testWeapon), Throws.Nothing);
                mockWeaponRepository.Verify(wr => wr.Create(testWeapon), Times.Once);
            }
            else
            {
                //Assert.That(() => wLogic.Create(testWeapon), Throws.Exception);
                mockWeaponRepository.Verify(wr => wr.Create(testWeapon), Times.Never());
            }
        }

        #endregion

        #region NON-CRUD Tests

        [Test]
        public void HighestDMGWeaponUser()
        {
            //ACT
            var actual = wLogic.HighestDMGWeaponUser();
            
            //ASSERT
            Assert.That(actual, Is.EquivalentTo(new List<string>()
            {
                "Apple"
            }));
        }

        [Test]
        public void WeaponAverageDMGByCharacterNameTest()
        {
            //ACT
            var actual = wLogic.WeaponAverageDMGByCharacterName("Cherry");

            //ASSERT
            Assert.That(actual, Is.EqualTo(430));
        }

        [Test]
        public void WeaponStatisticsTest()
        {
            //ACT
            var actual = wLogic.WeaponStatistics();
            var expected = new List<KeyValuePair<double, int>>()
            {
                new KeyValuePair<double, int>(490, 600)
            };

            //ASSERT
            Assert.That(actual, Is.EqualTo(expected));
        }

        #endregion
    }
}
