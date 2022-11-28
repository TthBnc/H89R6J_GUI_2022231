using Microsoft.EntityFrameworkCore;
using JEH01V_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JEH01V_HFT_2021222.Repository
{
    public partial class GenshinDbContext : DbContext
    {
        public virtual DbSet<Character> Characters { get; set; }
        public virtual DbSet<Artifact> Artifacts { get; set; }
        public virtual DbSet<Weapon> Weapons { get; set; }

        public GenshinDbContext()
        {
            this.Database.EnsureCreated();
        }
        public GenshinDbContext(DbContextOptions<GenshinDbContext> opt) : base(opt)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optBuilder)
        {
            if (!optBuilder.IsConfigured)
            {
                optBuilder
                    .UseInMemoryDatabase("GenshinDb")
                    .UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Weapon>(weapon => weapon
            .HasOne(weapon => weapon.Character)
            .WithMany(job => job.Weapons)
            .HasForeignKey(weapon => weapon.CharacterId)
            .OnDelete(DeleteBehavior.ClientSetNull));

            modelBuilder.Entity<Artifact>(artifact => artifact
            .HasOne(artifact => artifact.Character)
            .WithMany(job => job.Artifacts)
            .HasForeignKey(artifact => artifact.CharacterId)
            .OnDelete(DeleteBehavior.ClientSetNull)); 

            modelBuilder.Entity<Character>().HasData(new Character[]
            {
                new Character(1, "Itto", "Geo"),
                new Character(2, "Beidou", "Electro"),
                new Character(3, "Chongyun", "Cryo"),
                new Character(4, "Diluc", "Pyro"),
                new Character(5, "Ganyu", "Cryo"),
                new Character(6, "Fischl", "Electro"),
                new Character(7, "Tartaglia", "Hydro"),
                new Character(8, "Venti", "Anemo"),
                new Character(9, "Mona", "Hydro"),
                new Character(10, "Ningguang", "Geo"),
                new Character(11, "Lisa", "Electro"),
                new Character(12, "Yanfei", "Pyro"),
                new Character(13, "Hutao", "Pyro"),
                new Character(14, "Shogun", "Electro"),
                new Character(15, "Xiao", "Anemo"),
                new Character(16, "Zhongli", "Geo"),
                new Character(17, "Bennett", "Pyro"),
                new Character(18, "Kazuha", "Anemo"),
                new Character(19, "Ayaka", "Cryo"),
                new Character(20, "Xingqiu", "Hydro"),
            });

            modelBuilder.Entity<Weapon>().HasData(new Weapon[]
            {
                new Weapon(1, "Redhorn Stonethresher", 542, 1),        
                new Weapon(2, "Skyward Pride", 674, 1),        
                new Weapon(3, "The Unforged", 608, 2),        
                new Weapon(4, "Wolf's Gravestone", 608, 3),
                new Weapon(5, "Akuoumaru", 510, 3),
                new Weapon(6, "Song Of Broken Pines", 741, 4),
                new Weapon(7, "Polar Star", 608, 5),
                new Weapon(8, "Thundering Pulse", 608, 6),
                new Weapon(9, "Amos' Bow", 608, 7),
                new Weapon(10, "Skyward Harp", 674, 7),
                new Weapon(11, "Elegy for the End", 608, 8),
                new Weapon(12, "The Viridescent Hunt", 510, 8),
                new Weapon(13, "Favonius Codex", 510, 9),
                new Weapon(14, "Skyward Atlas", 674, 10),
                new Weapon(15, "Kagura's Verity", 608, 11),
                new Weapon(16, "Oathsworn Eye", 565, 11),
                new Weapon(17, "Memory of Dust", 608, 12),
                new Weapon(18, "The Widsith", 510, 12),
                new Weapon(19, "Staff of Homa", 608, 13),
                new Weapon(20, "Engulfing Lightning", 608, 14),
                new Weapon(21, "The Catch", 510, 14),
                new Weapon(22, "Calamity Queller", 741, 15),
                new Weapon(23, "Vortex Vanquisher", 608, 16),
                new Weapon(24, "Skyward Spine", 674, 16),
                new Weapon(25, "Freedom-Sworn", 608, 17),
                new Weapon(26, "Skyward Blade", 608, 18),
                new Weapon(27, "Aquila Favonia", 674, 18),
                new Weapon(28, "Amenoma Kageuchi", 454, 19),
                new Weapon(29, "Mistsplitter Reforged", 674, 19),
                new Weapon(30, "Sacrificial Sword", 454, 20),
            });

            modelBuilder.Entity<Artifact>().HasData(new Artifact[]
            {
                new Artifact(1, "Husk of Opulent Dreams", 60, 1),       
                new Artifact(2, "Retracing Bolide", 70, 1),    
                new Artifact(3, "Thundering Fury", 85, 2),
                new Artifact(4, "Noblesse Oblige", 100, 3),
                new Artifact(5, "Berserker", 15, 4),
                new Artifact(6, "Gambler", 10, 4),
                new Artifact(7, "Gladiator's Finale", 25, 5),
                new Artifact(8, "Bloodstained Chivalry", 80, 6),
                new Artifact(9, "The Exile", 15, 6),
                new Artifact(10, "Ocean-Hued Clam", 60, 7),
                new Artifact(11, "Wanderer's Troupe", 15, 8),
                new Artifact(12, "Echoes of an Offering", 65, 9),
                new Artifact(13, "Archaic Petra", 60, 10),
                new Artifact(14, "Tenacity of the Millelith", 40, 10),
                new Artifact(15, "Thundersoother", 45, 11),
                new Artifact(16, "Shimenawa's Reminiscence", 40, 11),
                new Artifact(17, "Crimson Witch of Flames", 55, 12),
                new Artifact(18, "Lavawalker", 50, 13),
                new Artifact(19, "Scholar", 20, 14),
                new Artifact(20, "Martial Artist", 30, 14),
                new Artifact(21, "Vermillion Hereafter", 70, 15),
                new Artifact(22, "Pale Flame", 55, 16),
                new Artifact(23, "Maiden Beloved", 55, 17),
                new Artifact(24, "Instructor", 10, 18),
                new Artifact(25, "Viridescent Venerer", 75, 18),
                new Artifact(26, "Blizzard Strayer", 40, 19),
                new Artifact(27, "Emblem of Severed Fate", 75, 20),
                new Artifact(28, "Heart of Depth", 80, 20),
            });
        }
    }
}
