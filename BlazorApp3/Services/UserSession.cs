namespace BlazorApp3.Services;

/// <summary>
/// Class for registering user sessions
/// </summary>
public class UserSession
{
    public int? UserId { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }

    public bool IsAdmin { get; set; }


    public void Login(int? userId, string? userName, bool isAdmin, string? email)
    {
        UserId = userId;
        UserName = userName;
        IsAdmin = isAdmin;
        Email = email;
    }

    public bool isLoggedIn
    {
        get { return UserId != null; }
    }

    public void clear()
    {
        UserId = null;
        UserName = null;
        IsAdmin = false;
    }
}