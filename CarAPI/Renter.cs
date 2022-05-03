using Newtonsoft.Json;
namespace CarAPI
{
    public class Renter
    {
        [JsonIgnore]
        public long Id { get; set; }
        [JsonProperty("surname")]
        public string Surname { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("number")]
        public string Number { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }

    }
}
