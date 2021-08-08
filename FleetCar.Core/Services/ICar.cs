using FleetCar.Core.Models;
using System.Collections.Generic;

namespace FleetCar.Core.Services
{
    public interface ICar
    {
        IEnumerable<Car> Get();

        IEnumerable<CarReservation> GetReservations(int carId);

        IEnumerable<CarReservation> GetNotReturned(SortProperty? prop = SortProperty.RETURN_DATE, SortCriteria? criteria = SortCriteria.ASC);

        CarReservation AddReservation(int carId, int departmentId, int timeMinutes);

        bool IsReserved(Car car);

        bool IsReturned(Car car);

        bool IsValidReservationTime(int timeMinutes);
    }
}
