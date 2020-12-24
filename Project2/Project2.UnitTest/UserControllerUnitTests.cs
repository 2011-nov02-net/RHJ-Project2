
using Microsoft.AspNetCore.Mvc;
using Moq;
using Project2.Api.Controllers;
using Project2.Api.DTO;
using Project2.DataAccess.Entities.Repo.Interfaces;
using Project2.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Project2.UnitTest
{
    public class UserControllerUnitTests
    {
        static readonly Mock<IUserRepo> _mockRepo = new Mock<IUserRepo>();
        static readonly UserController userController = new UserController(_mockRepo.Object);
        string fake = "fakeID";
        string fake2 = "fakeID2";

        // success scenarios
        [Fact]
        public async Task UserController_GetAllUsersSuccess()
        {
            // arrange 
            // reset func called times -> 0
            _mockRepo.Invocations.Clear();
            _mockRepo.Setup(x => x.GetAllUsers()).ReturnsAsync(Test.Users());
               
            // act
            var actionResult = await userController.Get();

            // assert           
            var DTOs = Assert.IsAssignableFrom<ActionResult<IEnumerable<UserReadDTO>>>(actionResult);
            var okObjects = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);

            // in debugger, DTOs -> result -> value -> result view
            //var first = actionResult.Value.First();            
            //Assert.Equal("cus1", first.UserId);
            //Assert.Equal("Kyle", first.First);
            //Assert.Equal("Crane", first.Last);         
        }
    
        [Fact]
        public async Task UserController_GetOneUserSuccess()
        {
            // arrange   
            _mockRepo.Invocations.Clear();
            _mockRepo.Setup(x => x.GetOneUser(It.IsAny<string>())).ReturnsAsync(Test.Users()[0]);
                
            // act
            var actionResult = await userController.GetUserById(fake);

            // assert
            var DTOs = Assert.IsAssignableFrom<ActionResult<UserReadDTO>>(actionResult);
            var okObjects = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            Assert.Equal(200, okObjects.StatusCode);
        }
       
        [Fact]
        public async Task UserController_AddOneUserSuccess()
        {
            // arrange
            _mockRepo.Invocations.Clear();
            AppUser appUser= null;
            _mockRepo.Setup(x => x.GetOneUser(It.IsAny<string>())).ReturnsAsync((AppUser)null);         
            _mockRepo.Setup(x => x.AddOneUser(It.IsAny<AppUser>())).Callback<AppUser>((x) =>
            {
               appUser = x;
            });         

            // act
            var actionResult = await userController.Post(Test.UsersDTO()[0]);

            // assert       
            _mockRepo.Verify(x => x.AddOneUser(It.IsAny<AppUser>()), Times.Once);           
            var DTOs = Assert.IsAssignableFrom<ActionResult<UserReadDTO>>(actionResult);
            var createObjects = Assert.IsAssignableFrom<CreatedAtActionResult>(actionResult.Result);
            Assert.Equal(201, createObjects.StatusCode);
            //Assert.Equal(appUser.UserId, );
        }

        [Fact]
        public async Task UserController_GetAllCardsOfOneUserSuccess()
        {
            // arrange
            _mockRepo.Invocations.Clear();
            _mockRepo.Setup(x => x.GetOneUser(It.IsAny<string>())).ReturnsAsync(Test.Users()[0]);
            _mockRepo.Setup(x => x.GetAllCardsOfOneUser(It.IsAny<string>())).ReturnsAsync(Test.Cards());

            // act
            var actionResult = await userController.GetUsersInventoryById(fake);

            // assert
            var DTOs = Assert.IsAssignableFrom<ActionResult<IEnumerable<CardReadDTO>>>(actionResult);
            var okObjects = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            Assert.Equal(200, okObjects.StatusCode);
        }

        [Fact]
        public async Task UserController_GetOneCardOfOneUserSuccess()
        {
            // arrange
            _mockRepo.Invocations.Clear();
            _mockRepo.Setup(x => x.GetOneUser(It.IsAny<string>())).ReturnsAsync(Test.Users()[0]);
            _mockRepo.Setup(x => x.GetOneCardOfOneUser(It.IsAny<string>(),It.IsAny<string>())).ReturnsAsync(Test.Cards()[0]);

            // act
            var actionResult = await userController.GetUsersCardById(fake,fake2);

            // assert
            var DTOs = Assert.IsAssignableFrom<ActionResult<CardReadDTO>>(actionResult);
            var okObjects = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            Assert.Equal(200, okObjects.StatusCode);
        }

        [Fact]
        public async Task UserController_AddOneNewCardToOneUserSuccess()
        {
            // arrange
            _mockRepo.Invocations.Clear();
            _mockRepo.Setup(x => x.GetOneUser(It.IsAny<string>())).Verifiable();
            _mockRepo.Setup(x => x.GetOneUser(It.IsAny<string>())).ReturnsAsync(Test.Users()[0]);         
            _mockRepo.Setup(x => x.AddOneCardToOneUser(It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            // act
            var actionResult = await userController.AddCardToUserInventory(fake,Test.CardsDTO()[0]);

            // assert
            _mockRepo.Verify(x => x.GetOneUser(It.IsAny<string>()), Times.Once);
            _mockRepo.Verify(x => x.AddOneCardToOneUser(It.IsAny<string>(), It.IsAny<string>()),Times.Once);
            var DTOs = Assert.IsAssignableFrom<ActionResult<CardReadDTO>>(actionResult);
            var createObjects = Assert.IsAssignableFrom<CreatedAtActionResult>(actionResult.Result);
            Assert.Equal(201, createObjects.StatusCode);
        }
       
        [Fact]
         
        public async Task UserController_DeleteOneCardOfOneUserSuccess()
        {
            // arrange     
            _mockRepo.Invocations.Clear();
            _mockRepo.Setup(x => x.GetOneUser(It.IsAny<string>())).ReturnsAsync(Test.Users()[0]);
            _mockRepo.Setup(x => x.DeleteOneCardOfOneUser(It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            // act
            var actionResult = await userController.DeleteUsersCardById(fake,fake2);

            // assert
            _mockRepo.Verify(x => x.GetOneUser(It.IsAny<string>()), Times.Once);
            _mockRepo.Verify(x => x.DeleteOneCardOfOneUser(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            var DTOs = Assert.IsAssignableFrom<ActionResult>(actionResult); // Delete is a "void", no .Result
            var createObjects = Assert.IsAssignableFrom<NoContentResult>(actionResult);
            Assert.Equal(204, createObjects.StatusCode);
        }

        // fail scenarios
        [Fact]
        public async Task UserController_AddOneUserFailure()
        {
            // arrange
            _mockRepo.Invocations.Clear();
            _mockRepo.Setup(x => x.GetOneUser(It.IsAny<string>())).ReturnsAsync(Test.Users()[0]);

            // act
            var actionResult = await userController.Post(Test.UsersDTO()[0]);

            // assert
            //Assert.IsAssignableFrom<ConflictObjectResult>(actionResult);
            var obj = Assert.IsAssignableFrom<ConflictResult>(actionResult.Result);
            Assert.Equal(409, obj.StatusCode);
        }

        [Fact]
        public async Task UserController_GetOneUserFailure()
        {
            // arrange
            _mockRepo.Invocations.Clear();
            _mockRepo.Setup(x => x.GetOneUser(It.IsAny<string>())).ReturnsAsync((AppUser)null);

            // act
            var actionResult = await userController.GetUserById(fake);

            // assert
            var action = Assert.IsAssignableFrom<NotFoundResult>(actionResult.Result);
            Assert.Equal(404, action.StatusCode);
        }

        [Fact]
        public async Task UserController_DeleteOneUserFailUserNotExist()
        {
            // arrange
            _mockRepo.Invocations.Clear();
            _mockRepo.Setup(x => x.GetOneUser(It.IsAny<string>())).ReturnsAsync((AppUser)null);

            // act
            var actionResult = await userController.DeleteUsersCardById(fake, fake2);

            // assert
            var action = Assert.IsAssignableFrom<NotFoundResult>(actionResult);
            Assert.Equal(404, action.StatusCode);
        }
    }
}
