using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Storage.Blobs;

namespace AzAppDownload
{
    public static class DownloadBlob
    {
        [FunctionName("DownloadBlob")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string filename = req.Query["File"];
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=manish3;AccountKey=li95cTesSlKLRlpd8vr1VOceAlcaJHXXrisoCJCJ5NC0rlvSBsc/sKL734aI/0yoDiJB49442l4i+AStvq6YQg==;EndpointSuffix=core.windows.net";
            string containerName = "cont";
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(filename);
            MemoryStream memoryStream = new MemoryStream();
            blobClient.DownloadTo(memoryStream);
            memoryStream.Position = 0;
            string content = new StreamReader(memoryStream).ReadToEnd();
            return new OkObjectResult(content);
        }
    }
}
