using FilRougeAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace FilRougeAPI.Configs
{

    public partial class BibliothequeDbContext : DbContext
    {
        public virtual DbSet<Admin> Admins { get; set; }

        public virtual DbSet<Adress> Adresses { get; set; }

        public virtual DbSet<Auteur> Auteurs { get; set; }

        public virtual DbSet<Domaine> Domaines { get; set; }

        public virtual DbSet<Emprunt> Emprunts { get; set; }

        public virtual DbSet<Lecteur> Lecteurs { get; set; }

        public virtual DbSet<Livre> Livres { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder opt)
        {
            opt.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=FilRougeAPI;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(NEXT VALUE FOR [PersonneSequence])");
            });

            /*
            //Ci dessous ajouté
            modelBuilder.Entity<Adress>(entity =>
            {
                //Version où l'adresse est suprimée si tous les lecteurs sont suprimé
                //entity.HasMany(e => e.Lecteurs).WithOne(d => d.Adresse).HasForeignKey(d => d.AdresseId).OnDelete(DeleteBehavior.Cascade);

                //Version où l'adresse est suprimée si tous les lecteurs avec cette adresse sont suprimée (par défaut)
                //entity.HasMany(e => e.Lecteurs).WithOne(d => d.Adresse).HasForeignKey(d => d.AdresseId).OnDelete(DeleteBehavior.Cascade); 
                
                //On empeche la supression des lecteurs si l'adresse qui lui est associé existe toujours, on garde les donnée lecteur
                //entity.HasMany(e => e.Lecteurs).WithOne(d => d.Adresse).HasForeignKey(d => d.AdresseId).OnDelete(DeleteBehavior.Restrict); 

                //On n'empeche pas la supression si l'autre entité si l'adresse existe toujours, mais on garde les données Lecteur
                //entity.HasMany(e => e.Lecteurs).WithOne(d => d.Adresse).HasForeignKey(d => d.AdresseId).OnDelete(DeleteBehavior.NoAction); 

                //On met l'addrese des lecteur à null si l'autre l'addresse est suprimé
                //entity.HasMany(e => e.Lecteurs).WithOne(d => d.Adresse).HasForeignKey(d => d.Adresse).OnDelete(DeleteBehavior.SetNull); 

            });
            //Ci dessus ajouté
            */
            

            modelBuilder.Entity<Auteur>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(NEXT VALUE FOR [PersonneSequence])");

            });


            modelBuilder.Entity<Emprunt>(entity =>
            {
                entity.HasIndex(e => e.LecteurId, "IX_Emprunts_LecteurId");

                entity.HasIndex(e => e.LivreId, "IX_Emprunts_LivreId");

                entity.HasOne(d => d.Lecteur).WithMany(p => p.Emprunts).HasForeignKey(d => d.LecteurId).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Livre).WithMany(p => p.Emprunts).HasForeignKey(d => d.LivreId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Lecteur>(entity =>
            {
                entity.HasIndex(e => e.AdresseId, "IX_Lecteurs_AdresseId");

                entity.Property(e => e.Id).HasDefaultValueSql("(NEXT VALUE FOR [PersonneSequence])");

                //Adresse ne peut être suprimé tant que au moins un lecteur existe toujours
                entity.HasOne(d => d.Adresse).WithMany(p => p.Lecteurs).HasForeignKey(d => d.AdresseId).OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<Livre>(entity =>
            {
                entity.HasIndex(e => e.AuteurId, "IX_Livres_AuteurId");

                entity.HasIndex(e => e.DomaineId, "IX_Livres_DomaineId");

                //Auteur ne peut pas être suprimé si au moins un livre associé existe
                entity.HasOne(d => d.Auteur).WithMany(p => p.Livres).HasForeignKey(d => d.AuteurId).OnDelete(DeleteBehavior.Restrict);

                //Domaine ne peut pas être suprimé si au moins un Livre associé existe
                entity.HasOne(d => d.Domaine).WithMany(p => p.Livres).HasForeignKey(d => d.DomaineId).OnDelete(DeleteBehavior.Restrict);

            });
            modelBuilder.HasSequence("PersonneSequence");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        
        
    }

}