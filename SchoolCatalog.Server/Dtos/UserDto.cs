namespace SchoolCatalog.Server.Dtos
{
    public class UserDto
    {
        public int IdUser { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; } 
        public int? IdElev { get; set; }
        public int? IdProfesor { get; set; }
    }
}