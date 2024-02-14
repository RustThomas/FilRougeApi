using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilRougeAPI.Models
{
    public partial class Admin
    {
        public int Id { get; set; }

        public string Nom { get; set; } = null!;

        public string Prenom { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Telephone { get; set; } = null!;

        public string MotDePasse { get; set; } = null!;
    }
}