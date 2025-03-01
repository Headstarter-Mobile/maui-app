namespace Headstarter.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public UserType UserType { get; set; }
    // ... other user properties
}