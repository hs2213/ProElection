using System.Security.Cryptography;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using ProElection.Entities;
using ProElection.Entities.Enums;
using ProElection.Repositories.Interfaces;
using ProElection.Services.Interfaces;

namespace ProElection.Services;

/// <inheritdoc/>
public sealed class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IElectionService _electionService;
    private readonly INotifyService _notifyService;

    private readonly IValidator<User> _userValidator;

    public UserService(
        IUserRepository userRepository,
        IElectionService electionService, 
        IValidator<User> userValidator, 
        INotifyService notifyService)
    {
        _userRepository = userRepository;
        _electionService = electionService;
        _userValidator = userValidator;
        _notifyService = notifyService;
    }

    /// <inheritdoc/>
    public async Task<User?> GetUserById(Guid id)
    {
        User? user = await _userRepository.GetUserById(id);

        if (user == null)
        {
            await _notifyService.ShowNotification("Failed to get user.");
        }

        return user;
    } 

    /// <inheritdoc/>
    public async Task<IEnumerable<User>> GetCandidates() => await _userRepository.GetCandidates();
    
    /// <inheritdoc/>
    public async Task<User?> Authenticate(string email, string password)
    {
        email = email.ToLower();
        
        User? user = await _userRepository.GetUserByEmail(email);
        
        if (user == null)
        {
            await _notifyService.ShowNotification("No account associated with that email");
            return null;
        }
        
        string hashedPassword = GetHashedPassword(password, user.PasswordSalt);

        if (hashedPassword != user.HashedPassword)
        {
            await _notifyService.ShowNotification("Password is incorrect.");
            return null;
        }

        await _notifyService.ShowNotification("Successfully Authenticated");

        return user;
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
        
        User returnedUser = await _userRepository.CreateUser(user);

        await _notifyService.ShowNotification("Successfully Created User");

        return returnedUser;
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
    public async Task AddElectionToUser(User user, Election election)
    {
        if (user.ParticipatingElections.Contains(election.Id))
        {
            await _notifyService.ShowNotification("User is already a part of the election");
            return;
        }
        
        user.ParticipatingElections.Add(election.Id);
        await _userRepository.UpdateUser(user);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<User>> GetUsersByEmailSearch(string searchQuery, UserType userType, Guid electionId)
    {
        return await _userRepository.GetUserBySearchForElection(searchQuery, userType, electionId);
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