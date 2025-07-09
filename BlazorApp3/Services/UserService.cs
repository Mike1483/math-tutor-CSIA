namespace BlazorApp3.Services;
using Microsoft.EntityFrameworkCore;
using BlazorApp3.Data; 
//Class for communicating with the data base
public class UserService 
{
    private readonly ApplicationDbContext _dbContext;

    public UserService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetUserByUserName(string username)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
    }

    public async Task<bool> RegisterUser(User newUser)
    {
        _dbContext.Users.Add(newUser);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<User?> LoginUser(string usernameAttempt, string passwordAttempt)
    {
      User? user = await GetUserByUserName(usernameAttempt);
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
        if (userToDelete == null)
        {
            // User not found
        }
        _dbContext.Users.Remove(userToDelete);
        await _dbContext.SaveChangesAsync();
       
    }
    

}