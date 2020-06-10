using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace TikTube.Orchestrators
{
    public static class ProcessSendVideoOrchestrator
    {
        [FunctionName("O_ProcessSendVideo")]
        public static async Task<List<string>> ProcessSendVideo(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var outputs = new List<string>();

            //var videoEditResults = 
            //    await context.CallSubOrchestratorAsync<VideoFileInfo[]>("O_EditVideo", videoLocation);

            outputs.Add(await context.CallActivityAsync<string>("A_GenerateThumbnail", "Tokyo.mp4"));
            outputs.Add(await context.CallActivityAsync<string>("A_GenerateMetadata", "Seattle.mp4"));

            var YouTubeUploadResults = 
                await context.CallSubOrchestratorAsync<string>("O_Upload", "VideoFile.NFO");

            outputs.Add(YouTubeUploadResults);

            return outputs;
        }

        [FunctionName("O_EditVideo")]
        public static async Task<List<string>> EditVideo(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var outputs = new List<string>();

            outputs.Add(await context.CallActivityAsync<string>("A_FormatVideos", "Tokyo.mp4"));
            outputs.Add(await context.CallActivityAsync<string>("A_ConcatenateVideos", "Seattle.mp4"));

            return outputs;
        }

        [FunctionName("O_Upload")]
        public static async Task<string> Upload(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var videoLocation = context.GetInput<string>();
            return await context.CallActivityAsync<string>("A_UploadYoutube", videoLocation);
        }

    }
}