using DataEntity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;


namespace DataEntity.Data
{
    public class InventarDbContext : DbContext
    {


        // Vlastnosti DbSet reprezentují tabulky v databázi
        public DbSet<Notebook> Notebooky { get; set; }
        public DbSet<Uzivatel> Uzivatele { get; set; }
        public DbSet<Objednavka> Objednavky { get; set; }

        // Metoda pro nastavení vazeb a konvencí


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Data Source=(localdb)\\MSSQLLocalDB;" +
                    "Initial Catalog=KocickaAPejsek;" +
                    "Integrated Security=True;" +
                    "TrustServerCertificate=True").UseLazyLoadingProxies();

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Vypnutí automatického generování ID (aby fungovala vaše náhodná čísla)
            modelBuilder.Entity<Notebook>().Property(n => n.IDNotebooku).ValueGeneratedNever();
            modelBuilder.Entity<Uzivatel>().Property(u => u.IDUzivatele).ValueGeneratedNever();
            modelBuilder.Entity<Objednavka>().Property(o => o.IDObjednavky).ValueGeneratedNever();
        }
    }

    // ---------------------------------------------------------
    // POMOCNÁ TŘÍDA PRO MIGRACE (FACTORY)
    // Je ve stejném souboru, aby "nepřekážela" v projektu.
    // ---------------------------------------------------------
    public class InventarDbContextFactory : IDesignTimeDbContextFactory<InventarDbContext>
    {
        public InventarDbContext CreateDbContext(string[] args)
        {
            // Tady říkáme nástroji pro migrace: "Takhle si vytvoř instanci databáze a ignoruj zbytek aplikace."
            return new InventarDbContext();
        }
    
    }
}
