using System.ComponentModel.DataAnnotations;

namespace SchoolCatalog.Server.Model
{
    public class Profesor
        
    {
        [Key]
        public int IdProfesor { get; set; }
        [Required(ErrorMessage ="Numele este obligatoriu")]
        [StringLength(100, ErrorMessage ="Numele trebuie sa aiba maxim 100 de caractere")]
        public string NumeProfesor { get; set; } = string.Empty;
        [Required(ErrorMessage ="Prenumele este obligatoriu")]
        [StringLength(100,ErrorMessage ="Prenumele trebuie sa aiba maxim 100 de caractere")]
        public string PrenumeProfesor { get; set; } = string.Empty;
        [Required(ErrorMessage ="Emailul este obligatoriu")]
        [EmailAddress(ErrorMessage ="Emailul nu este valid ")]
        public string EmailProfesor { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Date)]
        public DateTime DataNasterii { get; set; }
        public ICollection<Materie>? Materii { get; set; } 
        public ICollection<Clasa>? Clase { get; set; }

    }
}
