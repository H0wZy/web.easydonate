using Auth.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.Api.Data;

public class AuthDbContext(DbContextOptions<AuthDbContext> options) : DbContext(options)
{
    public DbSet<UserModel> Users { get; set; }
    public DbSet<DonorModel> Donors { get; set; }
    public DbSet<OrganizationModel> Organizations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("auth");

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