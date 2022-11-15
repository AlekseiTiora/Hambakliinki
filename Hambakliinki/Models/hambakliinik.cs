using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hambakliinki.Models
{
    public class hambakliinik
    {
        [Key]
        public int Id { set; get; }
        public int KlientId { get; set; }
        public klient? klient { get; set; }
        public DateTime kuupaev { get; set; }
        public int hambaarstId { get; set; }
        public hambaarst? hambaarst { get; set; }
        public int teenuseidId { get; set; }
        public teenuseid? teenuseid { get; set; }

    }
}
