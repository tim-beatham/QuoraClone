using Xunit;
using QuoraClone.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Linq;
using Moq;

namespace QuoraClone.Tests
{
    public static class Utilities
    {
        public static List<User> SeedQuoraUsers()
        {
            return new List<User>
            {
                new User { Username = "Tim", PasswordHash = "1234" },
                new User { Username = "Jeff", PasswordHash = "Alright" },
                new User { Username = "Steve", PasswordHash = "Heheheh" }
            };

        }

        public static List<Question> SeedQuoraQuestions()
        {
            return new List<Question>
            {
                new Question { QuestionID = 1, QuestionTitle = "Some title here" },
                new Question { QuestionID = 2, QuestionTitle = "Another title" },
                new Question { QuestionID = 3, QuestionTitle = "Another one eek" }
            };
        }

        public static List<UserQuestion> SeedQuoraUserAnswer()
        {
            return new List<UserQuestion>
            {
                new UserQuestion { 
                    UserQuestionID = 1, 
                    Username = "Tim", 
                    QuestionID = 2, 
                    Payload = "Some Answer" 
                },
                new UserQuestion {    
                    UserQuestionID = 2, 
                    Username = "Steve", 
                    QuestionID = 2,
                    Payload = "Another answer"
                },
                new UserQuestion {
                    UserQuestionID = 3,
                    Username = "Jeff",
                    QuestionID = 3,
                    Payload = "One more answer"
                },
                new UserQuestion {
                    UserQuestionID = 4,
                    Username = "Tim",
                    QuestionID = 1,
                    Payload = "An answer here"
                }
            };
        }

        public static DbSet<T> GetMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));
    
            return dbSet.Object;
        }


    }
}