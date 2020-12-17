using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Project2.Api.Controllers;
using Project2.Api.DTO;
using Project2.DataAccess.Entities.Repo;
using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Project2.UnitTest
{
    public class UserControllerUnitTests
    {
        private List<AppUser> GetTestSessions()
        {
            var sessions = new List<AppUser>();
            sessions.Add(new AppUser
            {
                UserId = "cus1",
                First = "Kyle",
                Last = "Crane",
                Email = "KC@gmail.com",
                UserRole = "User",
                NumPacksPurchased = 1,
                CurrencyAmount = 10,
            });
            sessions.Add(new AppUser
            {
                UserId = "cus2",
                First = "NaN",
                Last = "Man",
                Email = "NN@gmail.com",
                UserRole = "User",
                NumPacksPurchased = 2,
                CurrencyAmount = 20,
            });
            return sessions;
        }

        [Fact]
        public async Task UserController_GetAllUsers()
        {
            // arrange
            var _mockRepo = new Mock<IUserRepo>();
            var _mapper = new Mock<IMapper>();
            var _logger = new NullLogger<UserController>();
            var userController = new UserController(_mockRepo.Object, _logger, _mapper.Object);
            _mockRepo.Setup(x => x.GetAllUsers()).ReturnsAsync(GetTestSessions());
               
            // act
            var actionResult = await userController.Get();

            //asert           
            var DTOs = Assert.IsAssignableFrom<ActionResult<IEnumerable<UserReadDTO>>>(actionResult);
            var okObjects = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            Assert.Equal(200, okObjects.StatusCode);
          
            //var first = actionResult.Value.First();            
            //Assert.Equal("cus1", first.UserId);
            //Assert.Equal("Kyle", first.First);
            //Assert.Equal("Crane", first.Last);         
        }

        string fake = "fakeID";
        [Fact]
        public async Task UserController_GetOneUser()
        {
            // arrange
            var _mockRepo = new Mock<IUserRepo>();
            var _mapper = new Mock<IMapper>();
            var _logger = new NullLogger<UserController>();
            var userController = new UserController(_mockRepo.Object, _logger, _mapper.Object);
            _mockRepo.Setup(x => x.GetOneUser(It.IsAny<string>())).ReturnsAsync(GetTestSessions()[0]);
                
            // act
            var actionResult = await userController.GetUserById(fake);

            //asert
            var DTOs = Assert.IsAssignableFrom<ActionResult<UserReadDTO>>(actionResult);
            var okObjects = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            Assert.Equal(200, okObjects.StatusCode);
        }

        
        [Fact]
        public async Task UserController_AddOneUser()
        {
            // arrange
            var _mockRepo = new Mock<IUserRepo>();
            var _mapper = new Mock<IMapper>();
            var _logger = new NullLogger<UserController>();
            AppUser appUser= null;
            var userController = new UserController(_mockRepo.Object, _logger, _mapper.Object);
            _mockRepo.Setup(x => x.GetOneUser(It.IsAny<string>())).ReturnsAsync((AppUser)null);
          
            _mockRepo.Setup(x => x.AddOneUser(It.IsAny<AppUser>())).Callback<AppUser>((x) =>
            {
               appUser = x;
            });

            var newUser = new UserCreateDTO
            {
                UserId = "cus1",
                First = "Kyle",
                Last = "Crane",
                Email = "KC@gmail.com",
                NumPacksPurchased = 1,
                CurrencyAmount = 10,
            };

            // assert
            var actionResult = await userController.Post(newUser);

            // getting opposite result         
            _mockRepo.Verify(x => x.AddOneUser(It.IsAny<AppUser>()), Times.Once);           
            var DTOs = Assert.IsAssignableFrom<ActionResult<UserReadDTO>>(actionResult);
            var okObjects = Assert.IsAssignableFrom<CreatedAtActionResult>(actionResult.Result);
            Assert.Equal(201, okObjects.StatusCode);
            //Assert.Equal(appUser.UserId,);
        }
    }
}
