using System.ComponentModel.DataAnnotations;

namespace Hambakliinki.Models
{
    public class hambaarst
    {
        [Key]
        public int hambaarstId { set; get; }
        public string nimi { get; set; }
        public string perekonnanimi { get; set; }
    }
}
