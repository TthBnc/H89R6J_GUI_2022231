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
    public class ArtifactTester
    {
        ArtifactLogic aLogic;
        Mock<IRepository<Artifact>> mockArtifactRepository;

        [SetUp]
        public void Init()
        {
            mockArtifactRepository = new Mock<IRepository<Artifact>>();

            Character char1 = new Character(1, "Apple", "Pyro");
            Character char2 = new Character(2, "Berry", "Hydro");
            Character char3 = new Character(3, "Cherry", "Geo");

            mockArtifactRepository.Setup(a => a.ReadAll()).Returns(new List<Artifact>()
            {
                new Artifact()
                {
                    Id = 1,
                    Name = "Artifact1",
                    Cost = 100,
                    Character = char1,
                },
                new Artifact()
                {
                    Id = 2,
                    Name = "Artifact2",
                    Cost = 80,
                    Character = char2,
                },
                new Artifact()
                {
                    Id = 3,
                    Name = "Artifact3",
                    Cost = 60,
                    Character = char3,
                },
                new Artifact()
                {
                    Id = 4,
                    Name = "Artifact4",
                    Cost = 40,
                    Character = char3,
                }
            }.AsQueryable());
            aLogic = new ArtifactLogic(mockArtifactRepository.Object);
        }

        [TestCase("TestName",60,true)]
        [TestCase("TestName",-50,false)]
        [TestCase("TN",50,false)]
        [TestCase("TN",-10,false)]
        public void CreateArtifactTests(string name, int cost, bool shouldRun)
        {
            Artifact testArtifact = new Artifact() { Name = name, Cost = cost };
            if (shouldRun)
            {
                //aLogic.Create(testArtifact);
                //mockArtifactRepository.Verify(ar => ar.Create(testArtifact), Times.Once);
                Assert.That(() => aLogic.Create(testArtifact), Throws.Nothing);
            }
            else
            {
                //mockArtifactRepository.Verify(ar => ar.Create(testArtifact), Times.Never);
                Assert.That(() => aLogic.Create(testArtifact), Throws.Exception);
            }
        }

        #region NON-CRUD Tests

        [Test]
        public void HighestCostArtifactUserTest()
        {
            var actual = aLogic.HighestCostArtifactUser();
            
            Assert.That(actual, Is.EquivalentTo(new List<string>()
            {
                "Apple"
            }));
        }

        [Test]
        public void ArtifactAverageCostByCharacterNameTest()
        {
            //ACT
            var actual = aLogic.ArtifactAverageCostByCharacterName("Cherry");

            //ASSERT
            Assert.That(actual, Is.EqualTo(50));
        }

        [Test]
        public void ArtifactStatisticsTest()
        {
            //ACT
            var actual = aLogic.ArtifactStatistics();
            var expected = new List<KeyValuePair<double, int>>()
            {
                new KeyValuePair<double, int>(70, 100)
            };

            //ASSERT
            Assert.That(actual, Is.EqualTo(expected));
        }

        #endregion
    }
}
