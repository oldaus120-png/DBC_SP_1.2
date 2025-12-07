using DataEntity.Data.Base;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntity.Models
{
    [AddINotifyPropertyChangedInterface()]
    [Table("Notebooky")]
    public class Notebook : BaseModel
    {
        [Key] // Označení primárního klíče
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDNotebooku { get; set; }
       
        [Required(ErrorMessage = "Název modelu je povinný.")]
        [StringLength(100)]
        public required string Model { get; set; }

        [Required(ErrorMessage = "Výrobce je povinný.")]
        [StringLength(50)]
        public required string Vyrobce { get; set; }

        [Required(ErrorMessage = "CPU je povinné.")]
        [StringLength(100)]
        public required string CPU { get; set; }

        [Required(ErrorMessage = "Velikost RAM je povinná.")]
        [Range(1, 1024, ErrorMessage = "RAM musí být v rozmezí 1 GB až 1024 GB.")]
        public int RAM_GB { get; set; }

        [Required(ErrorMessage = "Kapacita úložiště je povinná.")]
        [Range(1, 32000, ErrorMessage = "Kapacita musí být kladná.")]
        public int KapacitaUloziste_GB { get; set; }

        [Required(ErrorMessage = "Cena je povinná.")]
        [Range(0.01, 1000000.00, ErrorMessage = "Cena musí být kladná.")]
        public decimal Cena { get; set; } // Používáme decimal pro peněžní hodnoty

        [Required(ErrorMessage = "Počet kusů je povinný.")]
        [Range(0, int.MaxValue, ErrorMessage = "Počet kusů nesmí být záporný.")]
        public int PocetKusuSkladem { get; set; }
        public required string Detail { get; set; }

        // PŘIDÁNO: Zajistí hezký výpis (např. "Lenovo ThinkPad (25000 Kč)")
        public override string ToString()
        {
            return $"{Vyrobce} {Model} ({Cena:C})";
        }
    }
}

