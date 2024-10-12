namespace ProjectIdentityServer.DTOs
{
    public class DTORegisterUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // Dashboard o LogErrores
    }
}
