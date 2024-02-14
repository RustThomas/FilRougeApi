using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FilRougeAPI.Models
{
    public partial class Livre
    {
        public int Id { get; set; }

        public string Titre { get; set; } = null!;

        public int NombrePages { get; set; }

        public int StatutDuLivre { get; set; }

        public int EtatDuLivre { get; set; }

        public int AuteurId { get; set; }

        public int DomaineId { get; set; }

        [JsonIgnore]
        public virtual Auteur? Auteur { get; set; } 

        [JsonIgnore]
        public virtual Domaine? Domaine { get; set; }

        [JsonIgnore]
        public virtual ICollection<Emprunt>? Emprunts { get; set; } = new List<Emprunt>();
    }
}