using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Events;
using Domain.Events.Dto;
using MediatR;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Sandbox.Cart.Read.Function.EventIntegration
{
    public  class CartEventIntegrationFunc
    {
        private readonly ILogger<CartEventIntegrationFunc> _logger;
        private readonly IMediator _mediator;

        public CartEventIntegrationFunc(ILogger<CartEventIntegrationFunc> log, IMediator mediator)
        {
            _logger = log;
            _mediator = mediator;
        }
       
        [FunctionName("CartEventIntegartionFunc")]
        public async Task  Run([CosmosDBTrigger(
            databaseName: "%CosmosDbSettings:DatabaseName%",
            collectionName: "%CosmosDbSettings:CollectionName%",
            ConnectionStringSetting = "%CosmosDbSettings:ConnectionString%",
            LeaseCollectionName = "leases", 
            CreateLeaseCollectionIfNotExists = true,
            LeaseCollectionPrefix ="cartRead",
            FeedPollDelay =1)]
            IReadOnlyList<Document> input,
            ILogger log)
        {
            if (input == null || input.Count == 0)
            {
                return;
            }
            log.LogInformation("Documents modified " + input.Count);
          
            foreach (var item in input)
            {
                try
                {
                    var eventStream = JsonConvert.DeserializeObject<EventStream>(item.ToString());
                    var typedEvent = GetEvent(eventStream);
                    await _mediator.Send(typedEvent);

                }
                catch (Exception e)
                {
                    log.LogError(e, "Error processing Cart Events");
                    throw;
                }
            }
    
        }
        public  Event GetEvent(EventStream eventStream)
        {
            var fqdnEventType = eventStream.EventType;
            var data = eventStream.Data;
            var eventName = GetEventName(fqdnEventType, 0);
            var assemblyName = GetEventName(fqdnEventType, 1);
            var type = Type.GetType($"{eventName},{assemblyName}");
            if (type is null)
            {
                return null;
            }
            var typedEvent = (Event)JsonConvert.DeserializeObject(data, type);
            return typedEvent;
        }
        private  string GetEventName(string qualifiedTypeName, int index)
        {
            var typeName = qualifiedTypeName.Split(",")[index];
            return typeName.Split(",").Last();
        }
    }
}
