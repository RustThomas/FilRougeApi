using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FilRougeAPI.Models
{
    public partial class Domaine
    {
        public int Id { get; set; }

        public string Nom { get; set; } = null!;

        public string Description { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Livre>? Livres { get; set; } = new List<Livre>();
    }
}