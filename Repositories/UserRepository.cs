﻿using Microsoft.EntityFrameworkCore;
using ProElection.Entities;
using ProElection.Entities.Enums;
using ProElection.Persistence;
using ProElection.Repositories.Interfaces;

namespace ProElection.Repositories;

/// <inheritdoc/>
public class UserRepository : IUserRepository
{
    private readonly ProElectionDbContext _dbContext;

    public UserRepository(ProElectionDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    /// <inheritdoc/>
    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(user => user.Id == id);
    }
    
    /// <inheritdoc/>
    public async Task<User?> GetUserByEmail(string email)
    {
        return await _dbContext.Users
            .SingleOrDefaultAsync(user => user.Email == email);
    }
    
    
    
    /// <inheritdoc/>
    public async Task<User> CreateUserAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }
    
    /// <inheritdoc/>
    public IEnumerable<User> GetCandidatesAsync()
    {
        return _dbContext.Users.Where(user => user.UserType == UserType.Candidate);
    }
    
    public async Task<bool> CheckEmailExistsAsync(string email)
    {
        return await _dbContext.Users.AnyAsync(user => user.Email == email);
    }
}