using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Documents.Client;

namespace TrainingBinus2
{
    public static class DeleteItemById
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="id">Bla222... </param>
        /// <param name="client"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("DeleteItemById")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "DeleteItemById/{id}")] HttpRequest req,
            string id,
            [CosmosDB(
                databaseName: "ToDoList",
                collectionName: "Items",
                ConnectionStringSetting = "CosmosDBConnection"
            )] DocumentClient client,
            ILogger log)
        {
            Uri deletedUri = UriFactory.CreateDocumentUri("ToDoList", "Items", id);
            if (deletedUri == null)
            {
                return new NotFoundObjectResult($"Data with id {id}, not found!");
            }
            await client.DeleteDocumentAsync(deletedUri);
            return new OkObjectResult("Data deleted!");
        }
    }
}
