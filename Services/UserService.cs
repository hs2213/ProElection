using System.Security.Cryptography;
using System.Text;
using ProElection.Entities;
using ProElection.Repositories.Interfaces;
using ProElection.Services.Interfaces;

namespace ProElection.Services;

public sealed class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<User?> GetUserById(Guid id) => await _userRepository.GetUserByIdAsync(id);

    /// <summary>
    /// Used to authenticate a user by their email and password.
    /// </summary>
    /// <param name="email">input user email</param>
    /// <param name="password">raw input user password</param>
    /// <returns>null or <see cref="User"/> if email exists in the db and the password is correct.</returns>
    public async Task<User?> Authenticate(string email, string password)
    {
        email = email.ToLower();
        
        User? user = await _userRepository.GetUserByEmail(email);
        
        if (user == null)
        {
            return null;
        }
        
        string hashedPassword = GetHashedPassword(password, user.PasswordSalt);

        return hashedPassword != user.HashedPassword ? null : user;
    }
    
    /// <summary>
    /// Gets a hashed password from a plain text password and a salt.
    /// </summary>
    /// <param name="password">raw input password.</param>
    /// <param name="salt">randomised salt given to user.</param>
    /// <returns></returns>
    private string GetHashedPassword(string password, string salt)
    {
        byte[] hashedPassword = SHA256.HashData(Encoding.UTF8.GetBytes(password + salt));
        return Convert.ToBase64String(hashedPassword);
    }

}