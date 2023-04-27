using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region AddRoles

        modelBuilder.Entity<OperationClaim>().HasData(
            new OperationClaim { Id = 1, Name = "Admin" },
            new OperationClaim { Id = 2, Name = "User" }
        );

        #endregion

        #region AddUsers

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                FirstName = "Furkan",
                LastName = "Yılmaz",
                EmailHash = "XmdBrE/TwVfC1R5blXnrzWF6Ajw11AQaQ6P2wsj4iK0RCGUF4WM0iKbEgtDO9YMc",
                PhoneHash = "/+SsGevv8ctONg99z8OKvqMGROjKWlzKQOP9pj33R34=",
                CreatedAt = DateTime.Now,
                PasswordHash = Convert.FromBase64String("bVkhiCsrWcCzGiBGf14ipvRY0qI7CRBKrdCAH9ZvJ0OHG5KYlCtg6PlMsxSaiQzH02yIYTVDUm8Yh61Elb31Aw=="),
                PasswordSalt = Convert.FromBase64String("1Z8O+M2gj0WsUwJEIUfM3QxeXkq2oEFLgeIQ/PAm/PP6k3SzhYP2tyQjnrZQt72OpkrskoGc2KFQhMQvb9JJcRHXA9m2HKPnSmxhoqOCfinAGO6CNmzjAUACqH9CpqyZ7X1YoEjk22nIaTNy6BAQYGDOde4gvL512R4NqS8CWrY="),
                RefreshToken = "",
            }
        );

        #endregion

        #region AddUserRoles

        modelBuilder.Entity<UserOperationClaim>().HasData(
            new UserOperationClaim { Id = 1, OperationClaimId = 1, UserId = 1 }
        );
        #endregion
    }
}

//Add-Migration 001 --Select default proje
//Update-Database
//Remove-Migration