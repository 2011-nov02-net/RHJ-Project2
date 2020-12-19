using Microsoft.AspNetCore.Mvc;
using Moq;
using Project2.Api.Controllers;
using Project2.Api.DTO;
using Project2.DataAccess.Entities.Repo.Interfaces;
using Project2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Project2.UnitTest
{
    public class CardControllerUnitTests
    {
        string fake = "fake";

        // success scenarios
        [Fact]
        public async Task CardController_GetAllCardsSuccess()
        {
            // arrange
            var _mockRepo = new Mock<ICardRepo>();
            var cardController = new CardController(_mockRepo.Object);
            _mockRepo.Setup(x => x.GetAllCards()).ReturnsAsync(Test.Cards);

            // act
            var actionResult = await cardController.Get();

            // assert
            var action = Assert.IsAssignableFrom<ActionResult<IEnumerable<CardReadDTO>>>(actionResult);
            var result = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            Assert.Equal(200, result.StatusCode);
        }
      
        [Fact]
        public async Task CardController_PostOneCardSuccess()
        {
            // arrange
            var _mockRepo = new Mock<ICardRepo>();
            var cardController = new CardController(_mockRepo.Object);            
            _mockRepo.Setup(x => x.GetOneCard(It.IsAny<string>())).ReturnsAsync((AppCard)null);           
            _mockRepo.Setup(x => x.GetOneCard(It.IsAny<string>())).Verifiable();
            _mockRepo.Setup(x => x.AddOneCard(It.IsAny<AppCard>())).Verifiable();

            // act
            var actionResult = await cardController.Post(Test.CardsDTO()[0]);

            // assert
            _mockRepo.Verify(x => x.GetOneCard(It.IsAny<string>()), Times.Once);
            _mockRepo.Verify(x => x.AddOneCard(It.IsAny<AppCard>()), Times.Once);
            var action = Assert.IsAssignableFrom<ActionResult<CardReadDTO>>(actionResult);
            var result = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            Assert.Equal(201, result.StatusCode);
        }
        
        [Fact]
        public async Task CardController_GetOneCardSuccess()
        {
            // arrange
            var _mockRepo = new Mock<ICardRepo>();
            var cardController = new CardController(_mockRepo.Object);
            _mockRepo.Setup(x => x.GetOneCard(It.IsAny<string>())).ReturnsAsync(Test.Cards()[0]);

            // act
            var actionResult = await cardController.GetCardById(fake);

            // assert
            var action = Assert.IsAssignableFrom<ActionResult<CardReadDTO>>(actionResult);
            var result = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            Assert.Equal(200, result.StatusCode);
        }
    
        [Fact]
        public async Task CardController_GetOneCardFailure()
        {
            // arrange
            var _mockRepo = new Mock<ICardRepo>();
            var cardController = new CardController(_mockRepo.Object);
            _mockRepo.Setup(x => x.GetOneCard(It.IsAny<string>())).ReturnsAsync((AppCard)null);

            // act
            var actionResult = await cardController.GetCardById(fake);

            // assert
            var result = Assert.IsAssignableFrom<NotFoundResult>(actionResult.Result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task CardController_PostOneCardFailure()
        {
            // arrange
            var _mockRepo = new Mock<ICardRepo>();
            var cardController = new CardController(_mockRepo.Object);
            _mockRepo.Setup(x => x.GetOneCard(It.IsAny<string>())).Verifiable();
            _mockRepo.Setup(x => x.AddOneCard(It.IsAny<AppCard>())).Verifiable();
            _mockRepo.Setup(x => x.GetOneCard(It.IsAny<string>())).ReturnsAsync(Test.Cards()[0]);
            
            // act 
            var actionResult = await cardController.Post(Test.CardsDTO()[0]);

            // assert
            _mockRepo.Verify(x => x.GetOneCard(It.IsAny<string>()), Times.Once);
            _mockRepo.Verify(x => x.AddOneCard(It.IsAny<AppCard>()), Times.Never);
            var result = Assert.IsAssignableFrom<ConflictResult>(actionResult.Result);
            Assert.Equal(409, result.StatusCode);
        }
    }
}
