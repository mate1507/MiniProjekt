using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

using shared.Model;

namespace Meddit.Data;

public class ApiService
{
    private readonly HttpClient http;
    private readonly IConfiguration configuration;
    private readonly string baseAPI = "";

    public ApiService(HttpClient http, IConfiguration configuration)
    {
        this.http = http;
        this.configuration = configuration;
        this.baseAPI = configuration["base_api"];
    }

    public async Task<Post[]> GetPosts()
    {
        string url = $"{baseAPI}posts/";
        Console.WriteLine(url);
        return await http.GetFromJsonAsync<Post[]>(url);
    }

    public async Task<Post> GetPost(int id)
    {
        string url = $"{baseAPI}posts/{id}/";
        return await http.GetFromJsonAsync<Post>(url);
    }

    public async Task<Comment> CreateComment(string text, int postId, int userId)
    {
        string url = $"{baseAPI}posts/{postId}/comments/";

        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PostAsJsonAsync(url, new { text, userId });

        // Get the JSON string from the response
        string json = msg.Content.ReadAsStringAsync().Result;

        // Deserialize the JSON string to a Comment object
        Comment? newComment = JsonSerializer.Deserialize<Comment>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties
            }
        );

        // Return the new comment
        return newComment;
    }

    public async Task<Post> CreatePost(string title, int userId)
    {
        string url = $"{baseAPI}posts/";

        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PostAsJsonAsync(url, new { title, userId });

        // Get the JSON string from the response
        string json = msg.Content.ReadAsStringAsync().Result;

        // Deserialize the JSON string to a Comment object
        Post? newPost = JsonSerializer.Deserialize<Post>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties
            }
        );

        // Return the new comment
        return newPost;
    }

    public async Task<Post> UpvotePost(int id)
    {
        string url = $"{baseAPI}posts/{id}/upvote/";

        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PutAsJsonAsync(url, "");

        // Get the JSON string from the response
        string json = msg.Content.ReadAsStringAsync().Result;

        // Deserialize the JSON string to a Post object
        Post? updatedPost = JsonSerializer.Deserialize<Post>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties
            }
        );

        // Return the updated post (vote increased)
        return updatedPost;
    }

    public async Task<Post> DownvotePost(int id)
    {
        string url = $"{baseAPI}posts/{id}/downvote/";

        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PutAsJsonAsync(url, "");

        // Get the JSON string from the response
        string json = msg.Content.ReadAsStringAsync().Result;

        // Deserialize the JSON string to a Post object
        Post? updatedPost = JsonSerializer.Deserialize<Post>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties
            }
        );

        // Return the updated post (vote increased)
        return updatedPost;
    }


    public async Task<Comment> UpvoteComment(int id)
    {
        string url = $"{baseAPI}comments/{id}/upvote/";

        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PutAsJsonAsync(url, "");

        // Get the JSON string from the response
        string json = msg.Content.ReadAsStringAsync().Result;

        // Deserialize the JSON string to a Post object
        Comment? updatedComment = JsonSerializer.Deserialize<Comment>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties
            }
        );

        // Return the updated post (vote increased)
        return updatedComment;
    }

    public async Task<Comment> DownvoteComment(int id)
    {
        string url = $"{baseAPI}comments/{id}/downvote/";

        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PutAsJsonAsync(url, "");

        // Get the JSON string from the response
        string json = msg.Content.ReadAsStringAsync().Result;

        // Deserialize the JSON string to a Post object
        Comment? updatedComment = JsonSerializer.Deserialize<Comment>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties
            }
        );

        // Return the updated post (vote increased)
        return updatedComment;
    }
}