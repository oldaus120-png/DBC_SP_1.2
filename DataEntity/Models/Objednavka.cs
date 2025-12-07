using DataEntity.Data.Base;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntity.Models
{
    [AddINotifyPropertyChangedInterface()]
    [Table("Objednavky")]
    public class Objednavka : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDObjednavky { get; set; }

        public int IDUzivatele { get; set; } // Cizí klíč

        public int IDNotebooku { get; set; } // Cizí klíč

        public DateTime DatumObjdenavky { get; set; }

        [Required(ErrorMessage = "Povinné pole")]
        [StringLength(100)]
        public decimal CenaSDPH { get; set; }

        [Required(ErrorMessage = "Povinné pole")]
        [StringLength(100)]
        public decimal CenaBezDPH { get; set; }

        [StringLength(100)]
        public string? ICO { get; set; } // Může být null v databázi (nepovinné)

        // Navigační vlastnost pro Uzivatele (pro vazbu N:1)
        [ForeignKey(nameof(IDUzivatele))]
        public virtual Uzivatel Uzivatel { get; set; } = null!;

        [ForeignKey(nameof(IDNotebooku))]
        public virtual Notebook Notebook { get; set; } = null!;


    }
}