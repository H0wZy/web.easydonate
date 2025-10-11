using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Auth.Api.Enum;

namespace Auth.Api.Model;

[Table("user")]
public class UserModel
{
    [Key] [Column("UserId")] public int UserId { get; set; }

    [Column("Username")]
    [StringLength(255)]
    public string? Username { get; set; }

    [Column("Email")] [StringLength(255)] public string Email { get; set; } = string.Empty;

    [Column("Password")]
    [StringLength(60)] // BCrypt sempre gera 60 caracteres
    public string Password { get; set; } = string.Empty;

    [Column("UserType")] public UserType UserType { get; set; } // "Doador", "Ong", "ADM"

    [Column("CreatedAt")] public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("ModifiedAt")] public DateTime? ModifiedAt { get; set; }

    [Column("TermsConsentDate")] public DateTime? TermsConsentDate { get; set; }


    // Relacionamentos
    // public virtual Doador? Doador { get; set; }
    // public virtual Ong? Ong { get; set; }
}