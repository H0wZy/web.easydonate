using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Api.Models;

[Table("donor")]
public class DonorModel
{
    [Key]
    [Column("DonorId")]
    public int DonorId { get; set; }

    [Column("UserId")]
    public int UserId { get; set; }

    [Column("FirstName")]
    [StringLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Column("LastName")]
    [StringLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Column("BirthDate")]
    public DateTime? BirthDate { get; set; }

    [Column("Phone")]
    [StringLength(20)]
    public string? Phone { get; set; }

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

    [Column("CreatedAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("ModifiedAt")]
    public DateTime? ModifiedAt { get; set; }

    // Relacionamentos
    [ForeignKey("UserId")]
    public virtual UserModel User { get; set; } = null!;
}
