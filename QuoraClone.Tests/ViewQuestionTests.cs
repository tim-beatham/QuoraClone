using Xunit;
using QuoraClone.Models;
using Moq;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using QuoraClone.Pages_Question;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Routing;

namespace QuoraClone.Tests
{
    public class ViewQuestionTests
    {
        [Fact]
        public async Task OnGet_QuestionExists_RetrievesQuestions()
        {
            //Given
            var optionsBuilder = new DbContextOptionsBuilder<QuoraCloneDbContext>()
                .UseInMemoryDatabase("InMemoryDb");

            var mockQuoraDbContext = new Mock<QuoraCloneDbContext>(optionsBuilder.Options);

            mockQuoraDbContext.Setup(m => m.GetUserQuestionsAsync()).Returns(Task.FromResult(Utilities.SeedQuoraUserAnswer()));
            mockQuoraDbContext.Setup(m => m.GetQuestionsAsync()).Returns(Task.FromResult(Utilities.SeedQuoraQuestions()));
            mockQuoraDbContext.Setup(m => m.GetUsersAsync()).Returns(Task.FromResult(Utilities.SeedQuoraUsers()));
            
            var model = new ViewQuestionModel(mockQuoraDbContext.Object);

            var questionID = 2;
            
            var result = await model.OnGet(questionID);

            // Sort by the username
            var expectedResponses = Utilities.SeedQuoraUserAnswer().Where(q => q.QuestionID == questionID);

            var actualMessages = Assert.IsAssignableFrom<List<UserQuestion>>(model.Responses).OrderBy(q => q.Username);

            Assert.Equal(
                expectedResponses.OrderBy(q => q.UserQuestionID),
                actualMessages.OrderBy(q => q.UserQuestionID)  
            );

            Assert.IsType<PageResult>(result);
        }

        [Fact]
        public async Task OnGet_QuestionDoesNotExist_ReturnsNotFound()
        {
            //Given
            var optionsBuilder = new DbContextOptionsBuilder<QuoraCloneDbContext>()
                .UseInMemoryDatabase("InMemoryDb");

            var mockQuoraDbContext = new Mock<QuoraCloneDbContext>(optionsBuilder.Options);

            mockQuoraDbContext.Setup(m => m.GetUserQuestionsAsync()).Returns(Task.FromResult(Utilities.SeedQuoraUserAnswer()));
            mockQuoraDbContext.Setup(m => m.GetQuestionsAsync()).Returns(Task.FromResult(Utilities.SeedQuoraQuestions()));
            mockQuoraDbContext.Setup(m => m.GetUsersAsync()).Returns(Task.FromResult(Utilities.SeedQuoraUsers()));

            var model = new ViewQuestionModel(mockQuoraDbContext.Object);

            var questionID = 22;
            
            var result = await model.OnGet(questionID);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task OnPost_AddQuestion_IsAdded()
        {   
             var optionsBuilder = new DbContextOptionsBuilder<QuoraCloneDbContext>()
                .UseInMemoryDatabase("InMemoryDb");

            var mockQuoraDbContext = new Mock<QuoraCloneDbContext>(optionsBuilder.Options);

            mockQuoraDbContext.Setup(m => m.Questions).Returns(Utilities.GetMockDbSet<Question>(Utilities.SeedQuoraQuestions()));
            mockQuoraDbContext.Setup(m => m.UserQuestions).Returns(Utilities.GetMockDbSet<UserQuestion>(Utilities.SeedQuoraUserAnswer()));
            mockQuoraDbContext.Setup(m => m.Users).Returns(Utilities.GetMockDbSet<User>(Utilities.SeedQuoraUsers()));

            mockQuoraDbContext.Setup(m => m.GetUserQuestionsAsync()).Returns(Task.FromResult(Utilities.SeedQuoraUserAnswer()));
            mockQuoraDbContext.Setup(m => m.GetQuestionsAsync()).Returns(Task.FromResult(Utilities.SeedQuoraQuestions()));
            mockQuoraDbContext.Setup(m => m.GetUsersAsync()).Returns(Task.FromResult(Utilities.SeedQuoraUsers()));
            
            var httpContext = new DefaultHttpContext();
            var modelState = new ModelStateDictionary();
            var actionContext = new ActionContext(httpContext, new RouteData(), new PageActionDescriptor(), modelState);

            var modelMetaDataProvider = new EmptyModelMetadataProvider();
            var viewData = new ViewDataDictionary(modelMetaDataProvider, modelState);
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            var pageContext = new PageContext(actionContext)
            {
                ViewData = viewData
            };

            var pageModel = new ViewQuestionModel(mockQuoraDbContext.Object)
            {
                PageContext = pageContext,
                TempData = tempData,
                Url = new UrlHelper(actionContext),
                UserQuestion = new UserQuestion { UserQuestionID = 5, Username = "Tim", Payload = "dssddsds" }
            };
            
            var result = await pageModel.OnPostAddQuestion();

            Assert.IsType<RedirectToPageResult>(result);

        }
    }
}