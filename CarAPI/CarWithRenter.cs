namespace CarAPI
{
    public class CarWithRenter
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public int Price { get; set; }
        public bool isRented { get; set; }
        public Renter renter { get; set; }
        public double? Engine { get; set; }

        public CarWithRenter(Car car)
        {
            this.Id = car.Id;
            this.Manufacturer = car.Manufacturer;
            this.Model = car.Model;
            this.Year = car.Year;
            this.Price = car.Price;
            this.isRented = car.isRented;
            if(car.Engine != null)  this.Engine = car.Engine;
            
        }

        public CarWithRenter(CarWithRenter car)
        {
            this.Id = car.Id;
            this.Manufacturer = car.Manufacturer;
            this.Model = car.Model;
            this.Year = car.Year;
            this.Price = car.Price;
            this.isRented = car.isRented;
            if (car.Engine != null) this.Engine = car.Engine;
        }

        public CarWithRenter()
        {

        }
    }
}
