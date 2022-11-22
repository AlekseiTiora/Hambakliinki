using System.ComponentModel.DataAnnotations;

namespace Hambakliinki.Models
{
    public class klient
    {
        [Key]
        public int Id { set; get; }
        [Required(ErrorMessage = "On vaja sisesta oma nime!!")]
        public string nimi { get; set; }
        [Required(ErrorMessage = "On vaja sisesta oma perekonnanime!!")]
        public string perekonnanimi { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Sisesta oma tel.number!")]
        [RegularExpression(@"\+372.+", ErrorMessage = "Vale telefoni number, Alguses +372...")]
        public string Phone { get; set; }
        public int vanus { get; set; }    }
}
