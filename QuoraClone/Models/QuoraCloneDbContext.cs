using Microsoft.EntityFrameworkCore;

namespace QuoraClone.Models
{

    public class QuoraCloneDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        
        public DbSet<UserQuestion> UserQuestions { get; set; }
        
        public DbSet<Question> Questions { get; set; }

        public QuoraCloneDbContext(DbContextOptions<QuoraCloneDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserQuestion>()
                .HasOne(qa => qa.QuestionAnswered)
                .WithMany(q => q.Responses)
                .HasForeignKey(qa => qa.QuestionID);


            modelBuilder.Entity<User>()
                .HasKey(u => u.Username);

            modelBuilder.Entity<Question>(entity => {
                entity.Property(e => e.QuestionID)
                      .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<UserQuestion>(entity => {
                entity.Property(e => e.QuestionID)
                    .ValueGeneratedOnAdd(); 
            });

                modelBuilder.Entity<User>().HasData(new User { Username = "Tim", PasswordHash = "123" });
                modelBuilder.Entity<User>().HasData(new User { Username = "Jeff", PasswordHash = "234"});
                modelBuilder.Entity<User>().HasData(new User { Username = "Bob", PasswordHash = "234233"});
                modelBuilder.Entity<User>().HasData(new User { Username = "Bill", PasswordHash = "23422"});

                modelBuilder.Entity<Question>().HasData(new Question { QuestionID = 1, QuestionTitle = "What sound do dogs make??" });
                modelBuilder.Entity<Question>().HasData(new Question { QuestionID = 2, QuestionTitle = "Are Leeds unreal??" });

                modelBuilder.Entity<UserQuestion>().HasData(new UserQuestion { UserQuestionID = 1, QuestionID = 1, Username = "Tim", Payload = "Whoof whoof"});
                modelBuilder.Entity<UserQuestion>().HasData(new UserQuestion { UserQuestionID = 2, QuestionID = 1, Username = "Jeff", Payload = "mooooo"});
                modelBuilder.Entity<UserQuestion>().HasData(new UserQuestion { UserQuestionID = 3, QuestionID = 1, Username = "Bill", Payload = "I am not too sure"});

                modelBuilder.Entity<UserQuestion>().HasData(new UserQuestion { UserQuestionID = 4, QuestionID = 2, Username = "Tim", Payload = "ofc"});
                modelBuilder.Entity<UserQuestion>().HasData(new UserQuestion { UserQuestionID = 5, QuestionID = 2, Username = "Bill", Payload = "Bielsa is goat"});
            
        }
    }
}