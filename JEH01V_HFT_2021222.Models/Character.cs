using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JEH01V_HFT_2021222.Models
{
    public class Character : Entity
    {
        [Key]
        [Column("CharID", TypeName = "int")]
        public override int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        public string Element { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Weapon> Weapons { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Artifact> Artifacts { get; set; }

        public Character()
        {
            Weapons = new HashSet<Weapon>();
            Artifacts = new HashSet<Artifact>();
        }

        public Character(int id, string name, string element) : base()
        {
            Id = id;
            Name = name;
            Element = element;
            Weapons = new HashSet<Weapon>();
            Artifacts = new HashSet<Artifact>();
        }
    }
}
