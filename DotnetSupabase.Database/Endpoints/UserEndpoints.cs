using Carter;
using DotnetSupabase.Database.Contrats;
using DotnetSupabase.Database.Entities;
using Mapster;

namespace DotnetSupabase.Database.Endpoints;

public sealed class UserEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/users", async (Supabase.Client supabaseClient) =>
        {
            var users = await supabaseClient.From<User>().Get();
            
            List<UserResponse> response = users.Models.Adapt<List<UserResponse>>();

            return Results.Ok(response);
        });

        app.MapGet("/users/{id:int}", async (int id, Supabase.Client supabaseClient) =>
        {
            var user = await supabaseClient.From<User>().Where(u => u.Id == id).Single();
            if (user is null)
            {
                return Results.NotFound();
            }
            UserResponse response = user.Adapt<UserResponse>();
            return Results.Ok(response);
        });

        app.MapPost("/users", async (CreateUserRequest newUser, Supabase.Client supabaseClient) =>
        {
            User userEntity = newUser.Adapt<User>();
            var insertedUser = await supabaseClient.From<User>().Insert(userEntity);
            UserResponse response = insertedUser.Model.Adapt<UserResponse>();
            return Results.Created($"/users/{response.Id}", response);
        });


    }
}
