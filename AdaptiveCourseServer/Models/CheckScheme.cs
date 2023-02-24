using Newtonsoft.Json;

namespace AdaptiveCourseServer.Models
{
    public class CheckScheme
    {
        [JsonProperty("OrientedGraph")]
        public Dictionary<string, List<string>> OrientedGraph { get; set; }

        [JsonProperty("Id")]
        public int Id { get; set; }
    }
}
