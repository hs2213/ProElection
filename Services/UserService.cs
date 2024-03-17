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
    
    public async Task<User?> GetUserByIdAsync(Guid id) => await _userRepository.GetUserByIdAsync(id);
    
    
    
}