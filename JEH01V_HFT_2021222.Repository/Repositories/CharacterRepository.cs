using JEH01V_HFT_2021222.Models;
using JEH01V_HFT_2021222.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JEH01V_HFT_2021222.Repository.Repositories
{
    public class CharacterRepository : Repository<Character>, IRepository<Character>
    {
        public CharacterRepository(GenshinDbContext dbCtx) : base(dbCtx) { } 
    }
}
