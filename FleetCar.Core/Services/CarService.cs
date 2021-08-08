using FleetCar.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FleetCar.Core.Services
{
    public enum SortProperty : byte
    {
        RETURN_DATE
    }

    public enum SortCriteria : byte
    {
        ASC, DESC
    }

    public class CarService : ICar
    {
        private const int MIN_RESERVATION_TIME_MINUTES = 30;

        private readonly IMockData _dbContext;

        public CarService(IMockData dbContext)
        {
            _dbContext = dbContext;
        }

        public bool IsReserved(Car car)
        {
            return car.Reservations.FirstOrDefault() != null ? true : false;
        }

        public bool IsReturned(Car car)
        {
            var reservation = car.Reservations.FirstOrDefault(x => x.ReturnDate > DateTime.UtcNow.AddMinutes(-MIN_RESERVATION_TIME_MINUTES));

            if (reservation == null)
            {
                return false;
            }

            return true;
        }

        public bool IsValidReservationTime(int timeMinutes)
        {
            return timeMinutes >= MIN_RESERVATION_TIME_MINUTES;
        }

        public CarReservation AddReservation(int carId, int departmentId, int timeMinutes)
        {
            if (!IsValidReservationTime(timeMinutes))
            {
                throw new InvalidOperationException($"Reservations should be 30 minutes or longer. (Actual: {timeMinutes})");
            }

            var car = _dbContext.GetCars().FirstOrDefault(x => x.Id == carId);

            if (car == null)
            {
                throw new KeyNotFoundException($"Car does not exist. (Car: {carId})");
            }

            if (IsReturned(car) || IsReserved(car))
            {
                throw new InvalidOperationException($"Car is no longer available to reserve. (Car: {carId})");
            }

            var department = _dbContext.GetDepartments().FirstOrDefault(x => x.Id == departmentId);

            if (department == null)
            {
                throw new KeyNotFoundException($"Department does not exist. (Department: {departmentId})");
            }

            var reservation = new Reservation(carId, departmentId, timeMinutes, car.ValueHour);

            return new CarReservation(car, reservation, department);
        }

        public IEnumerable<Car> Get()
        {
            return _dbContext.GetCars();
        }

        public IEnumerable<CarReservation> GetReservations(int carId)
        {
            var car = _dbContext.GetCars().FirstOrDefault(x => x.Id == carId);

            if (car == null)
            {
                throw new KeyNotFoundException($"Car does not exist. (Car: {carId})");
            }

            if (!car.Reservations.Any())
            {
                return Array.Empty<CarReservation>();
            }

            var departments = _dbContext.GetDepartments(ids: car.Reservations.Select(x => x.DepartmentId)).ToDictionary(x => x.Id);

            return car.Reservations.Select(r => new CarReservation(car, r, departments[r.DepartmentId]));
        }

        public IEnumerable<CarReservation> GetNotReturned(SortProperty? prop = SortProperty.RETURN_DATE, SortCriteria? criteria = SortCriteria.ASC)
        {
            var cars = _dbContext.GetCars().Where(x => x.Reservations.Any(r => DateTime.UtcNow < r.ReturnDate));

            if (!cars.Any())
            {
                return Array.Empty<CarReservation>();
            }

            var result = cars.Select(x => new CarReservation(x, x.Reservations.First()));

            Func<CarReservation, object> orderExpression = null;
            switch (prop)
            {
                case SortProperty.RETURN_DATE:
                    orderExpression = x => x.ReturnDate.Value;

                    break;
            }

            if (criteria == SortCriteria.ASC)
            {
                return result.OrderBy(orderExpression);
            }

            return result.OrderByDescending(orderExpression);
        }
    }
}
