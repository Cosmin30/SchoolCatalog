namespace SchoolCatalog.Server.Dtos
{
    public class RegisterDto
    {
        public string Email { get; set; } = string.Empty;
        public string Parola { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty; 
        public string Nume { get; set; } = string.Empty;
        public string Prenume { get; set; } = string.Empty;
        public DateTime DataNasterii { get; set; }
    }
}
