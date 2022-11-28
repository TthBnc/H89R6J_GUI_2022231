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
    public class CharacterTester
    {
        CharacterLogic cLogic;
        Mock<IRepository<Character>> mockCharacterRepository;

        [SetUp]
        public void Init()
        {
            mockCharacterRepository = new Mock<IRepository<Character>>();

            Weapon weapon1 = new Weapon(1, "Weapon1", 700, 1);
            Weapon weapon2 = new Weapon(2, "Weapon2", 600, 1);
            Weapon weapon3 = new Weapon(3, "Weapon3", 500, 2);
            Weapon weapon4 = new Weapon(4, "Weapon4", 400, 3);

            Artifact artifact1 = new Artifact(1, "Artifact1", 80, 1);
            Artifact artifact2 = new Artifact(2, "Artifact2", 60, 1);
            Artifact artifact3 = new Artifact(3, "Artifact3", 40, 2);
            Artifact artifact4 = new Artifact(4, "Artifact4", 20, 3);

            Character character1 = new Character(1, "Apple", "Pyro");
            Character character2 = new Character(2, "Berry", "Pyro");
            Character character3 = new Character(3, "Cherry", "Hydro");

            character1.Weapons.Add(weapon1);
            character1.Weapons.Add(weapon2);
            character2.Weapons.Add(weapon3);
            character3.Weapons.Add(weapon4);

            character1.Artifacts.Add(artifact1);
            character1.Artifacts.Add(artifact2);
            character2.Artifacts.Add(artifact3);
            character3.Artifacts.Add(artifact4);

            mockCharacterRepository.Setup(c => c.ReadAll()).Returns(new List<Character>()
            {
                character1, character2, character3
            }.AsQueryable());

            cLogic = new CharacterLogic(mockCharacterRepository.Object);
        }

        #region Create Tests

        [TestCase("TestName", "Pyro", true)]
        [TestCase("TestName", "", false)]
        [TestCase("TN", "Pyro", false)]
        [TestCase("TN", "", false)]
        public void CreateCharacterTests(string name, string element, bool shouldRun)
        {
            //ACT
            Character testCharacter = new Character() { Name = name, Element = element };

            //ASSERT
            if (shouldRun)
            {
                cLogic.Create(testCharacter);
                mockCharacterRepository.Verify(cr => cr.Create(testCharacter), Times.Once);
            }
            else
            {
                mockCharacterRepository.Verify(cr => cr.Create(testCharacter), Times.Never);
            }
        }

        #endregion

        #region NON-CRUD Tests

        [Test]
        public void WeaponsOfGivenElementTest()
        {
            //ACT
            var result = cLogic.WeaponsOfGivenElement("Pyro").ToArray();

            //ASSERT
            Assert.That(result[0].Name == "Weapon1" 
                        && result[1].Name == "Weapon2" 
                        && result[2].Name == "Weapon3" 
                        && result.Length == 3);
        }

        [Test]
        public void ArtifactsOfGivenElement()
        {
            //ACT
            var result = cLogic.ArtifactsOfGivenElement("Pyro").ToArray();

            //ASSERT
            Assert.That(result[0].Name == "Artifact1"
                        && result[1].Name == "Artifact2"
                        && result[2].Name == "Artifact3"
                        && result.Length == 3);
        }



        #endregion


    }
}
