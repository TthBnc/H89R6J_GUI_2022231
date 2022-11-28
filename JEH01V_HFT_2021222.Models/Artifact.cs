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
    public class Artifact : Entity
    {
        [Key]
        [Column("ArtifactID", TypeName = "int")]
        public override int Id { get; set; }

        [Required]
        [StringLength(60)]
        public string Name { get; set; }

        [Required]
        [Range(1, 101)]
        public int Cost { get; set; }

        [ForeignKey(nameof(Character))]
        public int CharacterId { get; set; }

        [JsonIgnore]
        public virtual Character Character { get; set; }

        public Artifact()
        {

        }

        public Artifact(int id, string name, int cost, int charId)
        {
            Id = id;
            Name = name;
            Cost = cost;
            CharacterId = charId;
        }
    }
}
