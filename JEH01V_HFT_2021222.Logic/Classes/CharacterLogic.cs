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
    public class CharacterLogic : ICharacterLogic
    {
        IRepository<Character> cRepo;

        public CharacterLogic(IRepository<Character> repo)
        {
            cRepo = repo;
        }

        #region CRUD

        public void Create(Character character)
        {
            if (character.Name.Length < 4)
            {
                throw new ArgumentException("Invalid name length!");
            }
            else if (character.Element.Length == 0)
            {
                throw new ArgumentException("The character must have an element!");
            }
            else
            {
                cRepo.Create(character);
            }
        }

        public void Delete(int id)
        {
            cRepo.Delete(id);
        }

        public Character Read(int id)
        {
            return cRepo.Read(id) ?? throw new ArgumentException("Character not found!");
        }

        public IEnumerable<Character> ReadAll()
        {
            return cRepo.ReadAll();
        }

        public void Update(Character character)
        {
            cRepo.Update(character);
        }

        #endregion

        #region NON-CRUD

        //Returns the weapons used by the given elemental characters.
        public IEnumerable<Weapon> WeaponsOfGivenElement(string element)
        {
            var weapons = (from x in cRepo.ReadAll()
                           where x.Element == element
                           select x.Weapons).SelectMany(t => t);
            return weapons;
        }


        //Returns the artifacts used by the given elemental characters.
        public IEnumerable<Artifact> ArtifactsOfGivenElement(string element)
        {
            var artifacts = (from x in cRepo.ReadAll()
                             where x.Element == element
                             select x.Artifacts).SelectMany(t => t);
            return artifacts;
        }

        

        #endregion


    }
}
