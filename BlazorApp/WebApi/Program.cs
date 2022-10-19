using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Json;

using shared.Model;
using Service;

var builder = WebApplication.CreateBuilder(args);

// Sætter CORS så API'en kan bruges fra andre domæner
var AllowSomeStuff = "_AllowSomeStuff";
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: AllowSomeStuff,
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        }
    );
});

// Tilføj DbContext factory som service.
builder.Services.AddDbContext<PostContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("ContextSQLite"))
);

// Viser flotte fejlbeskeder i browseren hvis der kommer fejl fra databasen
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Tilføj DataService så den kan bruges i endpoints
builder.Services.AddScoped<DataService>();

// Dette kode kan bruges til at fjerne "cykler" i JSON objekterne.

builder.Services.Configure<JsonOptions>(options =>
{
    // Her kan man fjerne fejl der opstår, når man returnerer JSON med objekter,
    // der refererer til hinanden i en cykel.
    // (altså dobbelrettede associeringer)
    options.SerializerOptions.ReferenceHandler = System
        .Text
        .Json
        .Serialization
        .ReferenceHandler
        .IgnoreCycles;
});

var app = builder.Build();

//Seed data hvis nødvendigt.
using (var scope = app.Services.CreateScope())
{
    var dataService = scope.ServiceProvider.GetRequiredService<DataService>();
    dataService.SeedData(); // Fylder data på, hvis databasen er tom. Ellers ikke.
}

app.UseHttpsRedirection();
app.UseCors(AllowSomeStuff);

// Middlware der kører før hver request. Sætter ContentType for alle responses til "JSON".
app.Use(
    async (context, next) =>
    {
        context.Response.ContentType = "application/json; charset=utf-8";
        await next(context);
    }
);

app.MapGet(
    "/api/posts/",
    (DataService service) =>
    {
        return service
            .GetPosts()
            .Select(
                b =>
                    new
                    {
                        PostId = b.PostId,
                        Title = b.Title,
                        user = new { b.User.UserId, b.User.Username }
                    }
            );
    }
);

app.MapGet(
    "/api/users/",
    (DataService service) =>
    {
        return service.GetUsers().Select(a => new { a.UserId, a.Username });
    }
);

app.MapGet(
    "/api/users/{id}/",
    (DataService service, int id) =>
    {
        return service.GetUser(id);
    }
);

app.MapPost(
    "/api/posts/",
    (DataService service, NewPostData data) =>
    {
        string result = service.CreatePost(data.Title, data.UserId);
        return new { message = result };
    }
);
app.MapGet(
    "/api/posts/{id}/",
    (DataService service, int id) =>
    {
        return service.GetPost(id);
    }
);

app.MapPost(
    "/api/posts/{PostId}/comments/",
    (DataService service, NewCommentData data, int PostId) =>
    {
        string result = service.CreateComment(data.Text, data.UserId, PostId);
        return new { message = result };
    }
);

app.MapPut(
    "/api/posts/{PostId}/upvote/",
    (DataService service, int PostId) =>
    {
        string result = service.AddUpvote(PostId);
        return new { message = result };
    }
);

app.MapPut(
    "/api/posts/{PostId}/downvote/",
    (DataService service, int PostId) =>
    {
        string result = service.AddDownvote(PostId);
        return new { message = result };
    }
);

app.MapPut(
    "/api/comments/{CommentId}/upvote/",
    (DataService service, int CommentId) =>
    {
        string result = service.AddCommentUpvote(CommentId);
        return new { message = result };
    }
);

app.MapPut(
"/api/comments/{CommentId}/downvote/",
(DataService service, int CommentId) =>
{
    string result = service.AddCommentDownvote(CommentId);
    return new { message = result };
}
);


app.Run();

record NewPostData(string Title, int UserId);

record NewCommentData(string Text, int UserId);
