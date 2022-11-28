using JEH01V_HFT_2021222.Logic.Interfaces;
using JEH01V_HFT_2021222.Models;
using JEH01V_HFT_2021222.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JEH01V_HFT_2021222.Logic.Classes
{
    public class ArtifactLogic : IArtifactLogic
    {
        IRepository<Artifact> aRepo;

        public ArtifactLogic(IRepository<Artifact> repo)
        {
            aRepo = repo;
        }

        #region CRUD

        public void Create(Artifact artifact)
        {
            if (artifact.Name.Length < 4)
            {
                throw new ArgumentException("Invalid name length!");
            }
            else if (artifact.Cost <= 0)
            {
                throw new ArgumentException("Artifact's cost invalid!");
            }
            else
            {
                aRepo.Create(artifact);
            }
        }

        public void Delete(int id)
        {
            aRepo.Delete(id);
        }

        public Artifact Read(int id)
        {
            return aRepo.Read(id) ?? throw new ArgumentException("Artifact not found!");
        }

        public IEnumerable<Artifact> ReadAll()
        {
            return aRepo.ReadAll();
        }

        public void Update(Artifact artifact)
        {
            aRepo.Update(artifact);
        }

        #endregion

        #region NON-CRUD

        //Returns the name(s) of Character(s) using the artifact(s) with the biggest Cost value.
        public IEnumerable<string> HighestCostArtifactUser()
        {
            int? maxCost = aRepo.ReadAll().Max(x => x.Cost);
            var character = from x in aRepo.ReadAll()
                            where x.Cost == maxCost
                            select x.Character.Name;

            return character;       //Chongyun
        }

        //Returns the average cost of the artifacts used by a given character
        public double ArtifactAverageCostByCharacterName(string name)
        {
            var avg = aRepo.ReadAll().Where(x => x.Character.Name == name).Average(x => x.Cost);

            return avg;
        }

        //Returns the average Cost and the highest Cost of all artifacts
        public IEnumerable<KeyValuePair<double, int>> ArtifactStatistics()
        {
            var helperQ1 = Math.Round((from x in aRepo.ReadAll()
                            select x.Cost).Average(), 2);

            var helperQ2 = (from x in aRepo.ReadAll()
                            select x.Cost).Max();

            var wStatistics = (from x in aRepo.ReadAll()
                               select new KeyValuePair<double, int>(helperQ1, helperQ2)).Take(1);


            return wStatistics;
        }

        #endregion
    }
}
