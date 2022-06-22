using Newtonsoft.Json;
using Salesforce.Common.Models.Json;

namespace Salesforce.Common
{
    /// <summary>
    /// Interface enforcing implementation of Attributes Property for multiple record updates
    /// </summary>
    public interface IAttributedObject
    {
        [JsonProperty(PropertyName = "attributes")]
        ObjectAttributes Attributes { get; set; }
    }
}
