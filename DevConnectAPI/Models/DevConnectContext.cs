using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DevConnectAPI.Models
{
    public class DevConnectContext : DbContext
    {
        public DevConnectContext(DbContextOptions<DevConnectContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Friends>()
                .HasOne(x => x.user)
                .WithMany()
                .HasForeignKey(x => x.user_id)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Friends>()
                .HasOne(x => x.friend)
                .WithMany()
                .HasForeignKey(x => x.FriendId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BlockList>()
                .HasOne(x => x.user)
                .WithMany()
                .HasForeignKey(x => x.user_id)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<BlockList>()
                .HasOne(x => x.block)
                .WithMany()
                .HasForeignKey(x => x.BlockId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<FriendRequests>()
                .HasOne(x => x.user)
                .WithMany()
                .HasForeignKey(x => x.user_id)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<FriendRequests>()
                .HasOne(x => x.request)
                .WithMany()
                .HasForeignKey(x => x.RequestId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PhotoAlbum>()
                .HasOne(x => x.CoverPhoto)
                .WithOne(x => x.Album)
                .HasForeignKey<Photo>(c => c.album_id);

            modelBuilder.Entity<User>()
                .HasOne(x => x.userProfile)
                .WithOne(x => x.user)
                .HasForeignKey<UserProfile>(x => x.user_id);

        }


        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<WorkPlace> WorkPlaces { get; set; }
        public DbSet<UserStatus> UserStatuses { get; set; }
        public DbSet<UserLike> UserLikes { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> PostComments {get;set;}
        public DbSet<Photo> Photos { get; set; }
        public DbSet<PhotoAlbum> PhotoAlbums { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Friends> Friends { get; set; }
    }
}
