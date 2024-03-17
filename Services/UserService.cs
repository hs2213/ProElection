using System.Security.Cryptography;
using System.Text;
using ProElection.Entities;
using ProElection.Repositories.Interfaces;
using ProElection.Services.Interfaces;

namespace ProElection.Services;

/// <inheritdoc/>
public sealed class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    /// <inheritdoc/>
    public async Task<User?> GetUserById(Guid id) => await _userRepository.GetUserById(id);

    /// <inheritdoc/>
    public IEnumerable<User> GetCandidates() => _userRepository.GetCandidates();
    
    /// <inheritdoc/>
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
    
    /// <inheritdoc/>
    public async Task<User?> CreateUser(User user)
    {
        user.Email = user.Email.ToLower();
        user.PasswordSalt = Guid.NewGuid().ToString();
        user.HashedPassword = GetHashedPassword(user.HashedPassword, user.PasswordSalt);

        if (await _userRepository.CheckEmailExists(user.Email))
        {
            return null;
        }
        
        return await _userRepository.CreateUser(user);
    }
    
    /// <summary>
    /// Gets a hashed password from a plain text password and a salt.
    /// </summary>
    /// <param name="password">raw input password.</param>
    /// <param name="salt">randomised salt given to user.</param>
    /// <returns></returns>
    private static string GetHashedPassword(string password, string salt)
    {
        byte[] hashedPassword = SHA256.HashData(Encoding.UTF8.GetBytes(password + salt));
        return Convert.ToBase64String(hashedPassword);
    }
}