using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace DotnetSupabase.Database.Entities;

[Table("users")]
public sealed class User:BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; } = string.Empty;
    [Column("email")]
    public string Email { get; set; } = string.Empty;
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
