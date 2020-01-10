// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace TrainingBinus2
{
    public static class UpdateItemSubscriber
    {
        [FunctionName("UpdateItemSubscriber")]
        public static async Task Run([EventGridTrigger]EventGridEvent eventGridEvent, 
            [CosmosDB(
                databaseName: "ToDoList",
                collectionName: "Items",
                ConnectionStringSetting = "CosmosDBConnection"
            )] DocumentClient client,
            ILogger log)
        {
            ToDoItem item = JsonConvert.DeserializeObject<ToDoItem>(eventGridEvent.Data.ToString());
            await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri("ToDoList", "Items"), item);
            log.LogInformation("Done!");
        }
    }
}
