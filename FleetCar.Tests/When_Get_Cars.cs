using FleetCar.Core;
using FleetCar.Core.Models;
using FleetCar.Core.Services;
using System;
using System.Linq;
using Xunit;

namespace FleetCar.Tests
{
    public class When_Get_Cars
    {
        private readonly IMockData _mockData;

        private readonly ICar _carService;

        public When_Get_Cars()
        {
            _mockData = new MockData();

            _carService = new CarService(_mockData);
        }


        [Theory]
        [InlineData(SortProperty.RETURN_DATE, SortCriteria.ASC)]
        [InlineData(SortProperty.RETURN_DATE, SortCriteria.DESC)]
        public void And_Is_Not_Returned_Then_Is_Ordered_By_Property(SortProperty property, SortCriteria criteria)
        {
            var notReturned = (_mockData.GetCars().Where(x => x.Reservations.Any(r => DateTime.UtcNow < r.ReturnDate))).Select(x => new CarReservation(x, x.Reservations.First()));

            var notReturnedOrdered = _carService.GetNotReturned(property, criteria).ToArray();

            switch (property)
            {
                case SortProperty.RETURN_DATE:
                    if (criteria == SortCriteria.ASC)
                    {
                        notReturned = notReturned.OrderBy(x => x.ReturnDate).ToArray();
                    }
                    else
                    {
                        notReturned = notReturned.OrderByDescending(x => x.ReturnDate).ToArray();
                    }
                    break;
            }

            Assert.Equal(notReturned.First().CarId, notReturnedOrdered.First().CarId);

            Assert.Equal(notReturned.Last().CarId, notReturnedOrdered.Last().CarId);
        }
    }
}
