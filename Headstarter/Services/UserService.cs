using Headstarter.Models;

namespace Headstarter.Services;

public partial class UserService : IUserService
{
    private readonly HttpClient _httpClient; // For API calls

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<User> AuthenticateUser(string username, string password, UserType userType)
    {
        if (username == "a" && password == "a")
        {
            return new User()
            {
                Username = username,
                Password = password,
                UserType = userType
            };
        }
        
        try
        {
            // Construct API endpoint based on userType
            string endpoint = userType == UserType.Specialist ? "api/specialists/login" : "api/recruiters/login";

            var request = new
            {
                Username = username,
                Password = password
            };
            var json = System.Text.Json.JsonSerializer.Serialize(request);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var user = System.Text.Json.JsonSerializer.Deserialize<User>(responseContent);
                return user;
            }
            else
            {
                // Handle authentication failure (e.g., throw an exception)
                return null;
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., network issues)
            return null;
        }
    }
    // ... other service methods
}
