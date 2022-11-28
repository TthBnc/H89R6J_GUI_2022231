﻿using JEH01V_HFT_2021222.Models;
using JEH01V_HFT_2021222.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JEH01V_HFT_2021222.Repository.Repositories
{
    public class WeaponRepository : Repository<Weapon>, IRepository<Weapon>
    {
        public WeaponRepository(GenshinDbContext dbCtx) : base(dbCtx) { } 
    }
}
