using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvidencijaSastanka.Modeli;
using EvidencijaSastanka.Modeli.Modeli;
using Microsoft.EntityFrameworkCore;

namespace EvidencijaSastanka.Podaci.Kontekst
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opcije) : base(opcije)
        {

        }
        public DbSet<Korisnik> Korisnik { get; set; } //svaki je jedna tabela u bazi
        public DbSet<Zgrada> Zgrada { get; set; }
        public DbSet<Sastanak> Sastanak { get; set; }
        public DbSet<PrisustvoNaSastanku> PrisustvoNaSastanku { get; set; }
        public DbSet<Stanar> Stanar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); //def relacija izmedju tabela

            modelBuilder.Entity<PrisustvoNaSastanku>()
                .HasOne(p => p.Sastanak)
                .WithMany(s => s.Prisustva)
                .HasForeignKey(p => p.SastanakId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PrisustvoNaSastanku>()
                .HasOne(p => p.Stanar)
                .WithMany()
                .HasForeignKey(p => p.StanarId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Sastanak>()
                .HasOne(s => s.Zgrada)
                .WithMany(z => z.Sastanci)
                .HasForeignKey(s => s.ZgradaId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Stanar>()
                .HasOne(s => s.Zgrada)
                .WithMany(z => z.Stanari)
                .HasForeignKey(s => s.ZgradaId)
                .OnDelete(DeleteBehavior.NoAction);
        }


    }
}
