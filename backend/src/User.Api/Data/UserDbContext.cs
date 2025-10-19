using Microsoft.EntityFrameworkCore;
using User.Api.Models;

namespace User.Api.Data;

public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
{
    public DbSet<UserModel> Users { get; set; }
    public DbSet<DonorModel> Donors { get; set; }
    public DbSet<OrganizationModel> Organizations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Remover schema específico - usar schema padrão 'public'
        // modelBuilder.HasDefaultSchema("user");

        modelBuilder.Entity<DonorModel>()
            .HasOne(d => d.User)
            .WithMany()
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrganizationModel>()
            .HasOne(o => o.User)
            .WithMany()
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}