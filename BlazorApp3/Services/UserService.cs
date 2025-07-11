namespace BlazorApp3.Services;

using Microsoft.EntityFrameworkCore;
using BlazorApp3.Data;

/// <summary>
/// Class for communicating with the User table in the database
/// </summary>
public class UserService
{
    private readonly ApplicationDbContext _dbContext;

    public UserService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetUserByUserEmail(string emailAttempt)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == emailAttempt);
    }

    public async Task<bool> RegisterUser(User newUser)
    {
        _dbContext.Users.Add(newUser);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<User?> LoginUser(string emailAttempt, string passwordAttempt)
    {
        User? user = await GetUserByUserEmail(emailAttempt);
        if (user != null)
        {
            if (BCrypt.Net.BCrypt.Verify(passwordAttempt, user.PasswordHash))
            {
                return user; //Login successful
            }
        }

        return null; //Login unsuccessful.
    }

    public async Task DeleteUserByUsername(string username)
    {
        var userToDelete = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
        _dbContext.Users.Remove(userToDelete);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
    {
        var userToUpdate = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        if (userToUpdate == null)
        {
            return false; // User not found
        }

        if (!BCrypt.Net.BCrypt.Verify(currentPassword, userToUpdate.PasswordHash))
        {
            return false; // Incorrect current password
        }

        userToUpdate.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ChangeEmailAsync(int userId, string newEmail)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId); // Using UserId
        if (user == null)
        {
            return false; // User not found
        }

        if (await _dbContext.Users.AnyAsync(u => u.Email == newEmail && u.UserId != userId)) // Using UserId
        {
            return false; // New email is already taken by another user
        }

        user.Email = newEmail;
        await _dbContext.SaveChangesAsync();
        return true;
    }
}