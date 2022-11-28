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
    public class WeaponLogic : IWeaponLogic
    {
        IRepository<Weapon> wRepo;

        public WeaponLogic(IRepository<Weapon> repo)
        {
            wRepo = repo;
        }

        #region CRUD

        public void Create(Weapon weapon)
        {
            if (weapon.Name.Length < 4)
            {
                throw new ArgumentException("Invalid name length!");
            }
            else if (weapon.PeakDmg < 360)
            {
                throw new ArgumentException("Peak damage can't be smaller than 360!");
            }
            else
            {
                wRepo.Create(weapon);
            }
        }

        public void Delete(int id)
        {
            wRepo.Delete(id);
        }

        public Weapon Read(int id)
        {
            return wRepo.Read(id) ?? throw new ArgumentException("Weapon not found!");
        }

        public IEnumerable<Weapon> ReadAll()
        {
            return wRepo.ReadAll();
        }

        public void Update(Weapon weapon)
        {
            wRepo.Update(weapon);
        }

        #endregion

        #region NON-CRUD

        //Returns the name(s) of Character(s) using the weapon(s) with the biggest PeakDmg value.
        public IEnumerable<string> HighestDMGWeaponUser()
        {
            int? maxDMG = wRepo.ReadAll().Max(x => x.PeakDmg);
            var character = from x in wRepo.ReadAll()
                            where x.PeakDmg == maxDMG
                            select x.Character.Name;

            return character;       //Xiao, Diluc
        }

        //Returns the average PeakDmg of the weapons used by a given character
        public double WeaponAverageDMGByCharacterName(string name)
        {
            var avg = wRepo.ReadAll().Where(x => x.Character.Name == name).Average(x => x.PeakDmg);

            return avg;
        }

        //Returns the average PeakDMG and the highest peakDMG of all weapons
        public IEnumerable<KeyValuePair<double, int>> WeaponStatistics()
        {
            var helperQ1 = Math.Round((from x in wRepo.ReadAll()
                           select x.PeakDmg).Average(), 2);

            var helperQ2 = (from x in wRepo.ReadAll()
                            select x.PeakDmg).Max();

            var wStatistics = (from x in wRepo.ReadAll()
                               select new KeyValuePair<double, int>(helperQ1, helperQ2)).Take(1);


            return wStatistics;
        }

        #endregion

    }
}
