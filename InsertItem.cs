using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;

namespace TrainingBinus2
{
    public static class InsertItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="newData"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("InsertItem")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequestMessage req,
            [CosmosDB(
                databaseName: "ToDoList",
                collectionName: "Items",
                ConnectionStringSetting = "CosmosDBConnection"
            )] out ToDoItem newData,
            ILogger log)
        {
            var content = req.Content;
            string jsonContent = content.ReadAsStringAsync().Result;
            newData = JsonConvert.DeserializeObject<ToDoItem>(jsonContent);

            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}
