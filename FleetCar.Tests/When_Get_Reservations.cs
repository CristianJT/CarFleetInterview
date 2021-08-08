using FleetCar.Core;
using FleetCar.Core.Services;
using System.Collections.Generic;
using Xunit;

namespace FleetCar.Tests
{
    public class When_Get_Reservations
    {
        private readonly ICar _carService;

        public When_Get_Reservations()
        {
            _carService = new CarService(new MockData());
        }


        [Fact]
        public void And_Car_Not_Exists_Then_Throw()
        {
            var ex = Assert.Throws<KeyNotFoundException>(() => _carService.GetReservations(32222));

            Assert.Contains("Car does not exist", ex.Message);
        }

        [Fact]
        public void And_Car_With_Reservations_The_Result_Is_NotEmpty_List()
        {
            var result = _carService.GetReservations(3);

            Assert.NotNull(result);

            Assert.NotEmpty(result);
        }

        [Fact]
        public void And_Car_Without_Reservations_The_Result_Is_Empty_List()
        {
            var result = _carService.GetReservations(1);

            Assert.NotNull(result);

            Assert.Empty(result);
        }
    }
}
