﻿namespace CarAPI
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

        public Car(Car car)
        {
            this.Id = car.Id;
            this.Manufacturer = car.Manufacturer;
            this.Model = car.Model;
            this.Year = car.Year;
            this.Price = car.Price;
            this.isRented = car.isRented;
            this.renterId = car.renterId;
            this.Engine = car.Engine;
        }
        public Car()
        {

        }
        

    }
}
