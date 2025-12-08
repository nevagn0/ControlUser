using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
namespace ControlSystem.Infrastructure.Data;

public class ControlSystemDB : DbContext
{
    public ControlSystemDB(DbContextOptions<ControlSystemDB> options) : base(options)
    {
        
    }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasDefaultValueSql("uuid_generate_v4()")
                .ValueGeneratedOnAdd();
            
            entity.Property(e => e.Email)
                .HasColumnName("email")
                .HasMaxLength(255);
            
            entity.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(255);
            
            entity.Property(e => e.HashPassword)
                .HasColumnName("hash_password");
            
            entity.Property(e => e.Role)
                .HasColumnName("role")
                .HasMaxLength(255);
            
            entity.Property(e => e.DateCreate)
                .HasColumnName("date_create")
                .HasDefaultValueSql("now()");
            
            entity.Property(e => e.DateUpdate)
                .HasColumnName("date_update").
                HasDefaultValueSql("now()");
        });
    }
}