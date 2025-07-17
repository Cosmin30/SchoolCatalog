using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace SchoolCatalog.Server.Model
{
    public class Elev
    {
        
        [Key]
        public int IdElev { get; set; }
        [Required(ErrorMessage = "Numele este obligatoriu"),
        MinLength(2,ErrorMessage ="Numele  elevului trebuie sa aiba minim 2 caractere"),
        MaxLength(50, ErrorMessage = "Numele elevului trebuie sa aiba maxim 50 de caractere"),
        RegularExpression(@"^(?=.{1,50}$)[a-zA-Z]+(?:[-'\s][a-zA-Z]+)*$", 
        ErrorMessage="Numele elevului trebuie sa contina doar litere si sau -")]
        public string NumeElev { get; set; } = string.Empty;
        [Required(ErrorMessage = "Prenumele este obligatoriu"),
        MinLength(2, ErrorMessage = "Prenumele trebuie sa aiba minim 2 de caractere "),
        MaxLength(50,ErrorMessage ="Prenumele trebuie sa aiba maxim 50 de caractere"),
        RegularExpression(@"^(?=.{1,50}$)[A-Z]([a-z]+)+(?:[-'\s][a-zA-Z]+)*$",
        ErrorMessage= "Prenumele trebuie să înceapă cu majusculă și să conțină doar litere și/sau: ' - ")]
        public string PrenumeElev { get; set; } = string.Empty;
        [Required(ErrorMessage = "Data nașterii este obligatorie")]
        [DataType(DataType.Date)]
        public DateTime DataNasterii { get; set; }
        
        [ForeignKey(nameof(Clasa))]
        public int? ClasaId { get; set; }
        public Clasa? Clasa { get; set; } 

        public ICollection<Nota>? Note { get; set; } 
        public ICollection<FisierTema>? FisiereTeme { get; set; }
        public ICollection<Media>? Medii { get; set; }

    }
}
