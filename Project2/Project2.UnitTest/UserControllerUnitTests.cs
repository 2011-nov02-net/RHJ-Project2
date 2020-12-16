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
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Project2.UnitTest
{
    public class UserControllerUnitTests
    {
        [Fact]
        public void UserController_GetAllUsers()
        {
            // arrange
            var _mockRepo = new Mock<IUserRepo>();
            var _mapper = new Mock<IMapper>();
            var _logger = new NullLogger<UserController>();
            var userController = new UserController(_mockRepo.Object, _logger, _mapper.Object);
            _mockRepo.Setup(x => x.GetAllUsers()).ReturnsAsync
                (
                    new List<AppUser>
                    {
                        new AppUser
                        {
                        UserId = "cus1", First = "Kyle", Last = "Crane", Email = "KC@gmail.com",
                        UserRole = "User", NumPacksPurchased = 1, CurrencyAmount = 10,
                        },
                        new AppUser
                        {
                        UserId = "cus2", First = "NaN", Last = "Man", Email = "NN@gmail.com",
                        UserRole = "User", NumPacksPurchased = 2, CurrencyAmount = 20,
                        },
                    }
                );

            // act
            var actionResult = userController.Get();

            //asert
            var DTOs = Assert.IsAssignableFrom<ActionResult<IEnumerable<UserReadDTO>>>(actionResult);
            var objects = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            Assert.Equal(200, objects.StatusCode);
            //var users = DTOs.Value;            
            //Assert.Equal(2, users.Count);
            //Assert.Equal("cus1", users[0].UserId);
            //Assert.Equal("Kyle", users[0].First);
            //Assert.Equal("Crane", users[0].Last);         
        }

        string fake = "fakeID";
        [Fact]
        public void UserController_GetOneUser()
        {
            // arrange
            var _mockRepo = new Mock<IUserRepo>();
            var _mapper = new Mock<IMapper>();
            var _logger = new NullLogger<UserController>();
            var userController = new UserController(_mockRepo.Object, _logger, _mapper.Object);
            _mockRepo.Setup(x => x.GetOneUser(It.IsAny<string>())).ReturnsAsync
                (                   
                    new AppUser
                    {
                    UserId = "cus1", First = "Kyle", Last = "Crane", Email = "KC@gmail.com",
                    UserRole = "User", NumPacksPurchased = 1, CurrencyAmount = 10,
                    }                                        
                );

            // act
            var actionResult = userController.GetUserById(fake);

            //asert
            var DTOs = Assert.IsAssignableFrom<ActionResult<UserReadDTO>>(actionResult);
            var objects = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            Assert.Equal(200, objects.StatusCode);

        }

        
        [Fact]
        public void UserController_AddOneUser()
        {
            // arrange
            var _mockRepo = new Mock<IUserRepo>();
            var _mapper = new Mock<IMapper>();
            var _logger = new NullLogger<UserController>();
            AppUser appUser;
            var userController = new UserController(_mockRepo.Object, _logger, _mapper.Object);
            _mockRepo.Setup(x => x.GetOneUser(It.IsAny<string>())).ReturnsAsync((AppUser)null);
            // no duplicate exist

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
            var actionResult = userController.Post(newUser);

            // getting opposite result         
            _mockRepo.Verify(x => x.AddOneUser(It.IsAny<AppUser>()), Times.Once);           
            var DTOs = Assert.IsAssignableFrom<ActionResult<UserReadDTO>>(actionResult);
            var objects = Assert.IsAssignableFrom<CreatedAtActionResult>(actionResult.Result);
            Assert.Equal(201, objects.StatusCode);
            //Assert.Equal(appUser.UserId, 
        }
    }
}
