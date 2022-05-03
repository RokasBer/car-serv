using Newtonsoft.Json;
namespace CarAPI
{
    public class RenterWithId
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("surname")]
        public string Surname { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("number")]
        public string Number { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }

        public RenterWithId(Renter renter)
        {
            this.Id = renter.Id;
            this.Surname = renter.Surname;
            this.Name = renter.Name;
            this.Number = renter.Number;
            this.Email = renter.Email;
        }
        public RenterWithId()
        {

        }
    }
}
