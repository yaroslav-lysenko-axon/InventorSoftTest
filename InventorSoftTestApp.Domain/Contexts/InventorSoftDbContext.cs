using InventorSoftTestApp.Domain.Models.DbEntities;
using InventorSoftTestApp.Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InventorSoftTestApp.Domain.Contexts;


public class InventorSoftDbContext(
    DbContextOptions<InventorSoftDbContext> contextOptions) : DbContext(contextOptions)
{
    public DbSet<User> Users { get; set; }
    public DbSet<TaskModel> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("user");
        modelBuilder.Entity<User>().HasKey(user => user.Id);
        modelBuilder.Entity<User>().Property(user => user.Id).HasColumnName("id");
        modelBuilder.Entity<User>().HasIndex(user => user.Name).IsUnique();
        modelBuilder.Entity<User>().Property(user => user.Name).HasColumnName("name");
        modelBuilder.Entity<User>().HasMany(user => user.Tasks);
        
        modelBuilder.Entity<TaskModel>().ToTable("task");
        modelBuilder.Entity<TaskModel>().HasKey(task => task.Id);
        modelBuilder.Entity<TaskModel>().Property(task => task.Id).HasColumnName("id");
        modelBuilder.Entity<TaskModel>().Property(task => task.UserId).HasColumnName("user_id");
        modelBuilder.Entity<TaskModel>().Property(task => task.Description).HasColumnName("description");
        modelBuilder.Entity<TaskModel>().Property(task => task.State).HasColumnName("state")
            .HasConversion(new EnumToStringConverter<TaskState>());
        modelBuilder.Entity<TaskModel>().Property(task => task.TransferCounter).HasColumnName("transfer_counter");
        modelBuilder.Entity<TaskModel>().Property(task => task.CreatedAt).HasColumnName("created_at");
        modelBuilder.Entity<TaskModel>().HasOne(task => task.User).WithMany(user => user.Tasks)
            .HasForeignKey(task => task.UserId);
    }
}