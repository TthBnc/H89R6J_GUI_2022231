using JEH01V_HFT_2021222.Models;
using JEH01V_HFT_2021222.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JEH01V_HFT_2021222.Repository.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        protected GenshinDbContext dbCtx;

        public Repository(GenshinDbContext dbctx)
        {
            dbCtx = dbctx ?? throw new ArgumentNullException(nameof(dbctx));
        }

        public void Create(T item)
        {
            dbCtx.Set<T>().Add(item);
            dbCtx.SaveChanges();
        }

        public void Delete(int id)
        {
            dbCtx.Set<T>().Remove(Read(id));
            dbCtx.SaveChanges();
        }

        public T Read(int id)
        {
            return dbCtx.Set<T>().FirstOrDefault(t => t.Id == id);
        }

        public IQueryable<T> ReadAll()
        {
            return dbCtx.Set<T>();
        }

        public void Update(T item)
        {
            var old = Read(item.Id);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            dbCtx.SaveChanges();
        }
    }
}
