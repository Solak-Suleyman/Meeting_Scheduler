using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

using System.Security.Cryptography;
using System.Text;
using WebApplication4.Models.Entity;

namespace WebApplication4.Models.Context
{

    public class MeetingSchedulerContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public MeetingSchedulerContext() : base()
        {
        }

        public MeetingSchedulerContext(DbContextOptions<MeetingSchedulerContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(
                "User ID =postgres;Password=123456;Server=localhost;Port=5432;Database=MeetingSchedulerDb; Integrated Security=true;Pooling=true;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            SHA1 sha = new SHA1CryptoServiceProvider();
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.Entity<User>().Property(b => b.created_time).HasDefaultValueSql("now()");
            modelBuilder.Entity<User>().Property(b=> b.updated_time).HasDefaultValueSql("now()");
            modelBuilder.Entity<UserMeeting>().Property(b => b.created_time).HasDefaultValueSql("now()");
            modelBuilder.Entity<UserMeeting>().Property(b=> b.updated_time).HasDefaultValueSql("now()");
            modelBuilder.Entity<Room>().Property(b => b.created_time).HasDefaultValueSql("now()");
            modelBuilder.Entity<Room>().Property(b=> b.updated_time).HasDefaultValueSql("now()");
            modelBuilder.Entity<Meeting>().Property(b => b.created_time).HasDefaultValueSql("now()");
            //modelBuilder.Entity<Meeting>().Property(b => b.created_time).ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<Meeting>().Property(b=> b.updated_time).HasDefaultValueSql("now()");
            //modelBuilder.Entity<Meeting>().Property(b => b.updated_time).ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<User>().HasData(
            new User
            {
            id = 1,
            user_name = "suleymansolak",
            name = "Süleyman",
            surname = "Solak",
            password = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes("1234"))),
            status ='A',
            created_time = DateTime.UtcNow,
            updated_time = DateTime.UtcNow,
             }
            );
            modelBuilder.Entity<Room>().HasData(
                new Room
                {
                    Id = 1,
                    name = "A1",
                    status = 'A',
                    created_time = DateTime.UtcNow,
                    updated_time = DateTime.UtcNow,
                });
        }


        public Microsoft.EntityFrameworkCore.DbSet<User> Users { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Room> Roooms { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<UserMeeting> UserMeetings { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Meeting> Meetings { get; set; }

    }

}
