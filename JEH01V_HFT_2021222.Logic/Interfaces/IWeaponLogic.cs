using JEH01V_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JEH01V_HFT_2021222.Logic.Interfaces
{
    public interface IWeaponLogic
    {
        void Create(Weapon weapon);
        void Delete(int id);
        Weapon Read(int id);
        IEnumerable<Weapon> ReadAll();
        void Update(Weapon weapon);
        public IEnumerable<string> HighestDMGWeaponUser();
        public double WeaponAverageDMGByCharacterName(string name);
        public IEnumerable<KeyValuePair<double, int>> WeaponStatistics();
    }
}
