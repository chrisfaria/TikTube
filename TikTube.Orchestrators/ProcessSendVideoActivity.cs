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
    public static class ProcessSendVideoActivity
    {
        [FunctionName("A_GenerateThumbnail")]
        public static string GenerateThumbnail([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation($">>>>> Generating thumbnail for {name}.");
            return $"Generated {name} thumbnail!";
        }

        [FunctionName("A_GenerateMetadata")]
        public static string GenerateMetadata([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation($">>>>> Generating metadata for {name}.");
            return $"Generated {name} metadata!";
        }

        [FunctionName("A_FormatVideos")]
        public static string FormatVideos([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation($">>>>> Formatting video {name}.");
            return $"Formatted {name} video!";
        }

        [FunctionName("A_ConcatenateVideos")]
        public static string ConcatenateVideos([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation($">>>>> Concatenating video {name}.");
            return $"Concatenated {name} video!";
        }

        [FunctionName("A_UploadYoutube")]
        public static string UploadYoutube([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation($">>>>> Uploading video {name} to YouTube.");
            return $"Uploaded {name} video to YouTube!";
        }

    }
}