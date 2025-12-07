using DataEntity.Data.Base;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;                                         // Poskytuje typ DateTime pro vlastnost Datum.
using System.Collections.Generic;                     // (V tomto souboru se nepoužívá; nevadí, klidně může zůstat.)
using System.Linq;                                    // (Nepoužito zde; může zůstat kvůli konzistenci s ostatními.)
using System.Threading.Tasks;


namespace DataEntity.Models
{
    [AddINotifyPropertyChangedInterface()]
    [Table("Uživatel")]
    public class Uzivatel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDUzivatele { get; set; }


        [Required(ErrorMessage = "Jméno je povinné")]
        [StringLength(20)]
        public required string Jmeno { get; set; }

        [Required(ErrorMessage = "Příjmení je povinné")]
        [StringLength(100)]
        public required string Prijmeni { get; set; }

        [Required(ErrorMessage = "Email je povinný")]
        [StringLength(100)]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Telefón je povinný")] 
        [StringLength(20)]
        public required string Telefon { get; set; }

        [Required(ErrorMessage = "Adresa je povinná")]
        [StringLength(100)]
        public required string Adresa { get; set; }

    
    // PŘIDÁNO: Metoda pro hezký výpis v objednávkách
        public override string ToString()
        {
            return $"{Prijmeni} {Jmeno}";
        }
    }
}