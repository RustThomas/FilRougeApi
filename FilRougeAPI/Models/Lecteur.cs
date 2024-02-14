using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FilRougeAPI.Models
{
    public partial class Lecteur
    {
        public int Id { get; set; }

        public string Nom { get; set; } = null!;

        public string Prenom { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Telephone { get; set; } = null!;

        public string MotDePasse { get; set; } = null!;

        public int AdresseId { get; set; }

        [JsonIgnore]
        public virtual Adress? Adresse { get; set; }

        [JsonIgnore]
        public virtual ICollection<Emprunt>? Emprunts { get; set; } = new List<Emprunt>();
    }
}