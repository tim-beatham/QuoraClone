using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace QuoraClone.Models
{

    public class QuoraCloneDbContext : DbContext
    {

        public virtual DbSet<User> Users { get; set; }
        
        public virtual DbSet<UserQuestion> UserQuestions { get; set; }
        
        public virtual DbSet<Question> Questions { get; set; }

        public QuoraCloneDbContext(DbContextOptions<QuoraCloneDbContext> options) : base(options) {}

        public async virtual Task<List<Question>> GetQuestionsAsync() =>
            await Questions.AsNoTracking().ToListAsync();
        
        
        public async virtual Task<List<UserQuestion>> GetUserQuestionsAsync() =>
            await UserQuestions.AsNoTracking().ToListAsync();
        

        public async virtual Task<List<User>> GetUsersAsync() =>
            await Users.AsNoTracking().ToListAsync();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

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
            
        }
    }
}