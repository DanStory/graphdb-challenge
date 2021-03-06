// Code generated by Microsoft (R) AutoRest Code Generator 1.2.2.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Swagger.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class DomainNode
    {
        /// <summary>
        /// Initializes a new instance of the DomainNode class.
        /// </summary>
        public DomainNode()
        {
          CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the DomainNode class.
        /// </summary>
        public DomainNode(string name = default(string), string type = default(string))
        {
            Name = name;
            Type = type;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; private set; }

    }
}
