using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hambakliinki.Models
{
    public class teenuseid
    {
        [Key]
        public int teenuseidId { set; get; }
        public string teenuse { get; set; }
        public int hind { get; set; }


    }
}
