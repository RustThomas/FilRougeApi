using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FilRougeAPI.Models
{
    public partial class Adress
    {
        public int Id { get; set; } 

        public string Appartement { get; set; } = null!;

        public string Rue { get; set; } = null!;

        public string Ville { get; set; } = null!;

        public string CodePostal { get; set; } = null!;

        public string Pays { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Lecteur> Lecteurs { get; set; } = new List<Lecteur>();
    }
}