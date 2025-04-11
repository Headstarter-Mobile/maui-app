using Headstarter.Protos;
using Microsoft.Maui.Storage;
using System.Text.Json;
using System.Threading.Tasks;

public class SessionService
{
    private const string UserKey = "loggedUser";
    private const string TokenKey = "authToken";

    private static SessionService? _instance;
    public static SessionService Instance => _instance ??= new SessionService();

    public User? CurrentUser { get; private set; }
    public AuthToken? CurrentToken { get; private set; }

    private SessionService() { }

    public async Task<bool> LoadSessionAsync()
    {
        try
        {
            var userJson = await SecureStorage.GetAsync(UserKey);
            var tokenJson = await SecureStorage.GetAsync(TokenKey);

            if (userJson == null || tokenJson == null)
                return false;

            CurrentUser = JsonSerializer.Deserialize<User>(userJson);
            CurrentToken = JsonSerializer.Deserialize<AuthToken>(tokenJson);

            return CurrentUser != null && CurrentToken != null;
        }
        catch
        {
            return false;
        }
    }

    public async Task SaveSessionAsync(User user, AuthToken token)
    {
        CurrentUser = user;
        CurrentToken = token;

        var userJson = JsonSerializer.Serialize(user);
        var tokenJson = JsonSerializer.Serialize(token);

        await SecureStorage.SetAsync(UserKey, userJson);
        await SecureStorage.SetAsync(TokenKey, tokenJson);
    }

    public void ClearSession()
    {
        CurrentUser = null;
        CurrentToken = null;
        SecureStorage.Remove(UserKey);
        SecureStorage.Remove(TokenKey);
    }

    public bool IsLoggedIn => CurrentUser != null && CurrentToken != null;
}
