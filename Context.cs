using Microsoft.EntityFrameworkCore;

namespace Blog
{
    public class Context: DbContext
    {
        public DbSet<UserData> Users { get; set; } 
        
        public DbSet<Group> Groups { get; set; }
        
        public DbSet<GroupMessages> GroupsMessages { get; set; }
        
        public DbSet<PersonalMessages> UsersMessages { get; set; }
        
        public DbSet<Post> Posts { get; set; } 
        
        public DbSet<UserViewsPost> Views { get; set; }
        
        public DbSet<UserLikesPost> Likes { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=blog;Trusted_Connection=True;");
        }
    }
}