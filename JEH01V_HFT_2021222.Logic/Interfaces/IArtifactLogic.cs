using JEH01V_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JEH01V_HFT_2021222.Logic.Interfaces
{
    public interface IArtifactLogic
    {
        void Create(Artifact artifact);
        void Delete(int id);
        Artifact Read(int id);
        IEnumerable<Artifact> ReadAll();
        void Update(Artifact artifact);
        public IEnumerable<string> HighestCostArtifactUser();
        public double ArtifactAverageCostByCharacterName(string name);
        public IEnumerable<KeyValuePair<double, int>> ArtifactStatistics();
    }
}
