using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FilRougeAPI.Models
{
    public partial class Emprunt
    {
        public int Id { get; set; }

        public DateTime DateEmprunt { get; set; }

        public DateTime DateRetour { get; set; }

        public int LecteurId { get; set; }

        public int LivreId { get; set; }

        [JsonIgnore]
        public virtual Lecteur? Lecteur { get; set; }

        [JsonIgnore]
        public virtual Livre? Livre { get; set; }
    }
}