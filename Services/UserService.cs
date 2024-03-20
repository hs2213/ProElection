using System.Security.Cryptography;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using ProElection.Entities;
using ProElection.Repositories.Interfaces;
using ProElection.Services.Interfaces;

namespace ProElection.Services;

/// <inheritdoc/>
public sealed class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IElectionService _electionService;

    private readonly IValidator<User> _userValidator;

    public UserService(
        IUserRepository userRepository,
        IElectionService electionService, 
        IValidator<User> userValidator)
    {
        _userRepository = userRepository;
        _electionService = electionService;
        _userValidator = userValidator;
    }
    
    /// <inheritdoc/>
    public async Task<User?> GetUserById(Guid id) => await _userRepository.GetUserById(id);

    /// <inheritdoc/>
    public async Task<IEnumerable<User>> GetCandidates() => await _userRepository.GetCandidates();
    
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

        await _userValidator.ValidateAndThrowAsync(user);
        
        return await _userRepository.CreateUser(user);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Election>?> GetUserElections(Guid userId)
    {
        User? user = await GetUserById(userId);

        if (user == null)
        {
            return null;
        }
        
        return await _electionService.GetElectionsByMultipleIds(user.ParticipatingElections);
    }
    
    /// <inheritdoc/>
    public async Task AddElectionToUser(User user, Guid electionId)
    {
        if (user.ParticipatingElections.Contains(electionId))
        {
            return;
        }
        
        user.ParticipatingElections.Add(electionId);
        await _userRepository.UpdateUser(user);
    }
    
    /// <inheritdoc/>
    public async Task RemoveElectionFromUser(User user, Guid electionId)
    {
        if (user.ParticipatingElections.Contains(electionId) == false)
        {
            return;
        }
        
        user.ParticipatingElections.Remove(electionId);
        await _userRepository.UpdateUser(user);
    }
    
    public async Task RemoveElectionFromUser(Guid userId, Guid electionId)
    {
        User? user = await GetUserById(userId);
        
        if (user == null)
        {
            return;
        }
        
        user.ParticipatingElections.Add(electionId);
        await _userRepository.UpdateUser(user);
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