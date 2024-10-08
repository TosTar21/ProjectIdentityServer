namespace Services.User
{
    public interface ISvUser
    {
        Task<(bool Succeeded, string[] Errors)> CreateUserAsync(string userName, string email, string password, int roleId);

}
}
