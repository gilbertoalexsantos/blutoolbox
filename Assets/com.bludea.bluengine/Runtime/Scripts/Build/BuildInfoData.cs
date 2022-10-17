using System;
using Newtonsoft.Json;

namespace BluEngine
{
    [Serializable]
    public class BuildInfoData
    {
        [JsonProperty("buildVersion")]
        public string BuildVersion;

        [JsonProperty("buildNumber")]
        public int BuildNumber;

        public BuildInfoData(string buildVersion, int buildNumber)
        {
            BuildVersion = buildVersion;
            BuildNumber = buildNumber;
        }

        public override string ToString()
        {
            return $"BuildInfoData => BuildVersion {BuildVersion} | BuildNumber {BuildNumber}";
        }
    }
}