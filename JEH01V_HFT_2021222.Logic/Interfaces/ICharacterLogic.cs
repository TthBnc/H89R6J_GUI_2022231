using JEH01V_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JEH01V_HFT_2021222.Logic.Interfaces
{
    public interface ICharacterLogic
    {
        void Create(Character character);
        void Delete(int id);
        Character Read(int id);
        IEnumerable<Character> ReadAll();
        void Update(Character character);
        public IEnumerable<Weapon> WeaponsOfGivenElement(string element);
        public IEnumerable<Artifact> ArtifactsOfGivenElement(string element);
    }
}
