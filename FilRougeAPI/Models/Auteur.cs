using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FilRougeAPI.Models
{
    public partial class Auteur
    {
        public int Id { get; set; }

        public string Nom { get; set; } = null!; 

        public string Prenom { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Telephone { get; set; } = null!;

        public string Grade { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Livre>? Livres { get; set; } = new List<Livre>();

        [NotMapped]
        public string FullName
        {
            get
            {
                return Prenom + " " + Nom;
            }
        }
    }
}