using MessageCase.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageCase.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MessageHistory>()
             .HasOne(u => u.User)
             .WithMany(u => u.MessageHistories)
             .HasForeignKey(u => u.SenderId);


            builder.Entity<BlockingUser>()
                .HasOne(u => u.User)
                .WithMany(u => u.BlockingUsers)
                .HasForeignKey(u => u.UserId);


            base.OnModelCreating(builder);
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<MessageHistory> MessageHistory { get; set; }
        public virtual DbSet<BlockingUser> BlockingUser { get; set; }
    }
}
