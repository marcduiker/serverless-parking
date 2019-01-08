using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace ServerlessParking
{
    public static class OrchestrationStarter
    {
        [FunctionName(nameof(OrchestrationStarter))]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "orchestration/{functionName}")]HttpRequestMessage req,
            [OrchestrationClient]DurableOrchestrationClient orchestrationClient,
            string functionName,
            ILogger log)
        {
            dynamic functionData = await req.Content.ReadAsAsync<object>();
            string instanceId = await orchestrationClient.StartNewAsync(functionName, functionData);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return await orchestrationClient.WaitForCompletionOrCreateCheckStatusResponseAsync(req, instanceId);
        }
    }
}
