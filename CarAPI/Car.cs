namespace CarAPI
{
    public class Car
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public double Engine { get; set; }
        public int Price { get; set; }

    }
}
