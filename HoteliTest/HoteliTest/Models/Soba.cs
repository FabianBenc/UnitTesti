using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HoteliTest.Models
{
    public class Soba
    {
        [Key]
        public int SobaID { get; set; }

        [ForeignKey("Hotel")]
        public int HotelID { get; set; }

        [Required,Display(Name="Broj sobe")]
        public int BrojSobe { get; set; }

        [Display(Name = "Tip Sobe ID")]
        public int TipSobeID { get; set; }

        public virtual Hotel Hotel { get; set; }

        [Display(Name = "Tip Sobe")]
        public virtual TipSobe TipSobe { get; set; }
    }
}