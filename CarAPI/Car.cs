namespace CarAPI
{
    public class Car
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public int Price { get; set; }
        public bool isRented { get; set; }
        public int? renterId { get; set; }
        public double? Engine { get; set; }

        public Car(CarWithRenter car)
        {
            this.Id = car.Id;
            this.Manufacturer = car.Manufacturer;
            this.Model = car.Model;
            this.Year = car.Year;
            this.Price = car.Price;
            this.isRented = car.isRented;
            if(car.isRented)
            {
                this.renterId = (int)car.renter.Id;
            }
            else
            {
                this.renterId = null;
            }
            
            this.Engine = car.Engine;
        }
        public Car()
        {


        }
        

    }
}
