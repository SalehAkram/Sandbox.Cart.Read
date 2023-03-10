
using Microsoft.Azure.Documents;
using Microsoft.Extensions.Logging;
using Moq;
using Sandbox.Cart.Read.Function.EventIntegration;
using MediatR;
using Domain.Events.Cart;
using Domain.Events.Dto;
using Newtonsoft.Json;
using Domain.Events;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Cart.Read.Function
{
    public class ChangeFeedUnitTest
    {
        private readonly Mock<ILogger<CartEventIntegrationFunc>> _loggerMock;
        private readonly Mock<IMediator> _mockMediatR;
        private  CartEventIntegrationFunc _sut;
        public ChangeFeedUnitTest()
        {
            _loggerMock = new Mock<ILogger<CartEventIntegrationFunc>>();
            _mockMediatR = new Mock<IMediator>();

            //SetUp();
            _sut = new CartEventIntegrationFunc(_loggerMock.Object, _mockMediatR.Object);
        }
        [Fact]
        public async void ChangeFeedTriggersCorrectHandler()
        {
             //arrange
            var documenList = new List<Document>();
            _mockMediatR.Setup(x => x.Send(It.IsAny<Event>(), It.IsAny<CancellationToken>()));
            documenList.Add(GetCartCreatedDoc());

            //act
            await _sut.Run(documenList, _loggerMock.Object);

            // Assert
            _mockMediatR.Verify(x => x.Send(It.IsAny<Event>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        [Fact]
        public async void ChangeFeedTriggersCorrectHandlerV2()
        {
            //arrange
            var documenList = new List<Document>();
            _mockMediatR.Setup(x => x.Send(It.IsAny<Event>(), It.IsAny<CancellationToken>()));
            documenList.Add(GetItemAddedDoc());

            //act
            await _sut.Run(documenList, _loggerMock.Object);

            // Assert
            _mockMediatR.Verify(x => x.Send(It.IsAny<Event>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        private Document GetCartCreatedDoc()
        {
         
            var eventType = "Domain.Events.Cart.CartCreated,Domain.Events";
            var cartId = Guid.NewGuid();

            var cartCreated = new CartCreated(cartId, Guid.NewGuid(), Guid.NewGuid(), "User");
            var data = JsonConvert.SerializeObject(cartCreated);
            var eventStream = new EventStream(eventType, data, cartId.ToString(), cartId.ToString(), DateTime.UtcNow, 1);
            var eventStreamJson = JsonConvert.SerializeObject(eventStream);
            var document = new Document();
            document.LoadFrom(new JsonTextReader(new StringReader(eventStreamJson)));
            return document;

        }
        private Document GetItemAddedDoc()
        {

            var eventType = "Domain.Events.Cart.ItemAdded,Domain.Events";
            var itemId = Guid.NewGuid();

            var itemAdded = new ItemAdded(itemId, 2, "USD", 1);
            var data = JsonConvert.SerializeObject(itemAdded);
            var eventStream = new EventStream(eventType, data, itemId.ToString(), itemId.ToString(), DateTime.UtcNow, 1);
            var eventStreamJson = JsonConvert.SerializeObject(eventStream);
            var document = new Document();
            document.LoadFrom(new JsonTextReader(new StringReader(eventStreamJson)));
            return document;

        }
        private void SetUp()
        {
            var services = new ServiceCollection();
            var handlerAssembly = AppDomain.CurrentDomain.Load("Cart.Read.Core");
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(handlerAssembly));
            _sut = new CartEventIntegrationFunc(_loggerMock.Object, new Mediator(services.BuildServiceProvider()));
        }

    }
}