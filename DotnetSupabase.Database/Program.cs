using Carter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<Supabase.Client>(_ =>
{
    var supabaseUrl = builder.Configuration["Supabase:Url"] ?? throw new InvalidOperationException("Supabase URL is not configured.");
    var supabaseKey = builder.Configuration["Supabase:Key"] ?? throw new InvalidOperationException("Supabase Key is not configured.");
    var options = new Supabase.SupabaseOptions
    {
        AutoConnectRealtime = false
    };
    return new Supabase.Client(supabaseUrl, supabaseKey, options);
});

builder.Services.AddCarter();

var app = builder.Build();

app.MapCarter();

app.Run();