using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Api.Model;

[Table("organization")]
public class OrganizationModel
{
    [Key]
    [Column("OrganizationId")]
    public int OrganizationId { get; set; }

    [Column("UserId")]
    public int UserId { get; set; }

    [Column("OrganizationName")]
    [StringLength(200)]
    public string OrganizationName { get; set; } = string.Empty;

    [Column("CNPJ")]
    [StringLength(18)]
    public string? CNPJ { get; set; }

    [Column("Description")]
    [StringLength(1000)]
    public string? Description { get; set; }

    [Column("Phone")]
    [StringLength(20)]
    public string? Phone { get; set; }

    [Column("Website")]
    [StringLength(255)]
    public string? Website { get; set; }

    [Column("Address")]
    [StringLength(500)]
    public string? Address { get; set; }

    [Column("City")]
    [StringLength(100)]
    public string? City { get; set; }

    [Column("State")]
    [StringLength(50)]
    public string? State { get; set; }

    [Column("ZipCode")]
    [StringLength(20)]
    public string? ZipCode { get; set; }

    [Column("IsVerified")]
    public bool IsVerified { get; set; } = false;

    [Column("CreatedAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("ModifiedAt")]
    public DateTime? ModifiedAt { get; set; }

    // Relacionamentos
    [ForeignKey("UserId")]
    public virtual UserModel User { get; set; } = null!;
}
