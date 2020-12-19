using Microsoft.AspNetCore.Mvc;
using Moq;
using Project2.Api.Controllers;
using Project2.Api.DTO;
using Project2.DataAccess.Entities.Repo;
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
    public class StoreControllerUnitTests
    {
        // success scenarios
        string fake = "";
        [Fact]
        public async Task StoreController_GetAllItemsSuccess()
        {
            // arrange
            var _mockRepo = new Mock<IStoreRepo>();
            var storeController = new StoreController(_mockRepo.Object);
            _mockRepo.Setup(x => x.GetAllStoreItems()).ReturnsAsync(Test.Items());

            // act
            var actionResult = await storeController.Get();

            // assert
            var result = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            Assert.Equal(200, result.StatusCode);     
        }

        [Fact]
        public async Task StoreController_GetOneItemSuccess()
        {
            // arrange
            var _mockRepo = new Mock<IStoreRepo>();
            var storeController = new StoreController(_mockRepo.Object);
            _mockRepo.Setup(x => x.GetStoreItemById(It.IsAny<string>())).Verifiable();
            _mockRepo.Setup(x => x.GetStoreItemById(It.IsAny<string>())).ReturnsAsync(Test.Items()[0]);

            // act
            var actionResult = await storeController.GetStoreItemById(fake);

            //
            _mockRepo.Verify(x => x.GetStoreItemById(It.IsAny<string>()), Times.Once);
            var result = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            Assert.Equal(200, result.StatusCode);
        }

        
        [Fact]
        public async Task StoreController_UpdateOneItemSuccess()
        {
            // arrange
            var _mockRepo = new Mock<IStoreRepo>();
            var storeController = new StoreController(_mockRepo.Object);
            _mockRepo.Setup(x => x.GetStoreItemById(It.IsAny<string>())).Verifiable();
            _mockRepo.Setup(x => x.UpdateStoreItemById(It.IsAny<string>(),It.IsAny<int>())).Verifiable();
            _mockRepo.Setup(x => x.GetStoreItemById(It.IsAny<string>())).ReturnsAsync(Test.Items()[0]);
            _mockRepo.Setup(x => x.UpdateStoreItemById(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(true);
            // repo return type determines mock return type

            // act
            var actionResult = await storeController.UpdateStoreItemById(fake,Test.ItemsDTO()[0]);

            // arrange
            _mockRepo.Verify(x => x.GetStoreItemById(It.IsAny<string>()), Times.Once);
            _mockRepo.Verify(x => x.UpdateStoreItemById(It.IsAny<string>(), It.IsAny<int>()),Times.Once);
            var result = Assert.IsAssignableFrom<NoContentResult>(actionResult.Result);
            Assert.Equal(204, result.StatusCode);
        }
        
        // fail scenarios
        [Fact]
        public async Task StoreController_GetAllItemsFail()
        {
            // arrange
            var _mockRepo = new Mock<IStoreRepo>();
            var storeController = new StoreController(_mockRepo.Object);
            _mockRepo.Setup(x => x.GetAllStoreItems()).ReturnsAsync((IEnumerable<AppStoreItem>)null);          
            // act
            var actionResult = await storeController.Get();

            // assert
            var result = Assert.IsAssignableFrom<NotFoundResult>(actionResult.Result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task StoreController_GetOneItemFail()
        {
            // arrange
            var _mockRepo = new Mock<IStoreRepo>();
            var storeController = new StoreController(_mockRepo.Object);
            _mockRepo.Setup(x => x.GetStoreItemById(It.IsAny<string>())).Verifiable();
            _mockRepo.Setup(x => x.GetStoreItemById(It.IsAny<string>())).ReturnsAsync((AppStoreItem)null);

            // act
            var actionResult = await storeController.GetStoreItemById(fake);

            //
            _mockRepo.Verify(x => x.GetStoreItemById(It.IsAny<string>()), Times.Once);
            var result = Assert.IsAssignableFrom<NotFoundResult>(actionResult.Result);
            Assert.Equal(404, result.StatusCode);
        }
    }
}
