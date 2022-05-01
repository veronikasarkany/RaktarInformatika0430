using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaktarInformatika0430.Models
{
    public class Aru
    {
        public int Id { get; set; }


        [Display(Name="Megnevezés")]
        [StringLength(60)]
        public string Megnevezes { get; set; }

        [Display(Name = "Beszállító")]
        [StringLength(30)]
        public string Beszallito { get; set; }

        [Display(Name = "Beszerzési Ár")]
        [Column(TypeName="decimal(10,2")]
        public decimal Ar { get; set; }
    }
}
