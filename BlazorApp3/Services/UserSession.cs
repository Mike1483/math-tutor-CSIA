namespace BlazorApp3.Services;

//class for registering user session
public class UserSession
{
    public int? UserId { get; set; }
    public string? UserName { get; set; }

    public bool IsAdmin { get; set; }


    public void Login(int? userId, string? userName, bool isAdmin)
    {
        UserId = userId;
        UserName = userName;
        IsAdmin = isAdmin;
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