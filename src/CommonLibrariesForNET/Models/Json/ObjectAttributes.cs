using Newtonsoft.Json;

namespace Salesforce.Common.Models.Json
{
    public class ObjectAttributes
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "referenceId")]
        public string ReferenceId { get; set; }
    }
}
