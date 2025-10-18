using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using User.Api.Enum;

namespace User.Api.Models;

[Table("user")]
public class UserModel
{
    [Key] [Column("Id")] public int Id { get; set; }

    [Column("Username")]
    [StringLength(255)]
    public required string Username { get; set; }

    [Column("Email")] [StringLength(255)] public required string Email { get; set; }

    [Column("Firstname")]
    [StringLength(255)] 
    public required string Firstname { get; set; }

    [Column("Lastname")]
    [StringLength(255)]
    public required string Lastname { get; set; }

    [Column("Fullname")] public string Fullname => $"{Firstname} {Lastname}";

    [Column("Password")]
    [StringLength(60)] // BCrypt sempre gera 60 caracteres
    public required string Password { get; set; } = string.Empty;

    [Column("UserType")] public required UserType UserType { get; set; } // "Doador", "Ong", "ADM"

    [Column("CreatedAt")] public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("UpdatedAt")] public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [Column("LastLoginAt")] public DateTime? LastLoginAt { get; set; }

    [Column("TermsConsentDate")] public DateTime? TermsConsentDate { get; set; }

    [Column("IsUserDisabled")] public bool IsUserDisabled { get; set; } = false;

    [Column("AcceptedTerms")] public bool AcceptedTerms { get; set; }

    // Relacionamentos
    // public virtual Doador? Doador { get; set; }
    // public virtual Ong? Ong { get; set; }
}