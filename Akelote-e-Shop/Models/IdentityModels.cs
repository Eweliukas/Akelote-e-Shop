using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Akelote_e_Shop.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }

        public string Address { get; set; }
       
        public bool IsBlocked { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            await manager.UpdateSecurityStampAsync(this.Id);
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Property> Property { get; set; }
        public DbSet<ItemProperty> ItemProperty { get; set; }
        public DbSet<Cart> Cart { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var login = modelBuilder.Entity<IdentityUserLogin>().ToTable("Logins")
                .HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId });
            var claim = modelBuilder.Entity<IdentityUserClaim>().ToTable("Claims");

            var user = modelBuilder.Entity<IdentityUser>().ToTable("Users");
            user.HasMany(u => u.Roles).WithRequired().HasForeignKey(ur => ur.UserId);
            user.HasMany(u => u.Logins).WithRequired().HasForeignKey(l => l.UserId);
            user.HasMany(u => u.Claims).WithRequired().HasForeignKey(c => c.UserId);
            user.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("UserNameIndex") { IsUnique = true }));
            user.Property(u => u.Email).HasMaxLength(256);
            user.Ignore(c => c.AccessFailedCount)
                .Ignore(c => c.LockoutEnabled)
                .Ignore(c => c.LockoutEndDateUtc)
                .Ignore(c => c.EmailConfirmed)
                .Ignore(c => c.PhoneNumberConfirmed)
                .Ignore(c => c.TwoFactorEnabled);

            modelBuilder.Entity<IdentityUserRole>()
                        .HasKey(r => new { r.UserId, r.RoleId })
                        .ToTable("UserRoles");

            var role = modelBuilder.Entity<IdentityRole>()
                                   .ToTable("Roles");
            role.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("RoleNameIndex") { IsUnique = true }));
            role.HasMany(r => r.Users).WithRequired().HasForeignKey(ur => ur.RoleId);
        }
    }
}