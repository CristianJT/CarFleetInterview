using FleetCar.Core;
using FleetCar.Core.Models;
using FleetCar.Core.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace FleetCar.Tests
{
    public class When_Add_Reservations
    {
        private readonly ICar _carService;

        public When_Add_Reservations()
        {
            _carService = new CarService(new MockData());
        }


        [Fact]
        public void And_Car_Not_Exists_Then_Throw()
        {
            var ex = Assert.Throws<KeyNotFoundException>(() => _carService.AddReservation(32222, 2, 100));

            Assert.Contains("Car does not exist", ex.Message);
        }

        [Fact]
        public void And_Department_Not_Exists_Then_Throw()
        {
            var ex = Assert.Throws<KeyNotFoundException>(() => _carService.AddReservation(1, 2454545, 100));

            Assert.Contains("Department does not exist", ex.Message);
        }

        [Fact]
        public void And_Car_Is_Available_Then_Can_Be_Reserved()
        {
            var car = new Car() { Id = 1, Code = "AB123QW", Make = "Ford", Model = "Mustang", Version = "GT Premium Fasback", Description = "Ford Mustang GT Premium Fasback", Year = 2016, ValueHour = 50, Reservations = Array.Empty<Reservation>() };

            var isAvailable = (!_carService.IsReserved(car) && !_carService.IsReturned(car));

            Assert.True(isAvailable);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(20)]
        public void And_Car_Is_Available_Then_Reservations_Should_30OrLonger(int timeMinutes)
        {
            var expected = timeMinutes >= 30 ? true : false;

            var isValid = _carService.IsValidReservationTime(timeMinutes);

            Assert.Equal(expected, isValid);
        }

        [Fact]
        public void And_Car_Is_Returned_Then_Cannot_Be_Reserved()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => _carService.AddReservation(3, 2, 100));

            Assert.Contains("Car is no longer available to reserve", ex.Message);
        }
    }
}
