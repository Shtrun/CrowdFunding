using System.Data.Entity;

namespace CrowdFunding.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasMany(x => x.UserProfiles)
                .WithMany(x => x.Roles)
                .Map(x =>
                {
                    x.ToTable("webpages_UsersInRoles"); // third table is named Cookbooks
                    x.MapLeftKey("RoleId");
                    x.MapRightKey("UserId");
                });

            //modelBuilder.Entity<Role>()
            //    .HasRequired(x => x.UserProfiles)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);
        }

        public DbSet<Membership> Memberships { get; set; }
        public DbSet<OAuthMembership> OAuthMemberships { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        //public DbSet<UsersInRoles> UsersInRoles { get; set; }
    }
}