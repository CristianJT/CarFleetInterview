using FleetCar.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FleetCar.Core
{
    public class MockData : IMockData
    {
        public IEnumerable<Car> GetCars()
        {
            var cars = new List<Car>();

            cars.Add(new Car() { Id = 1, Code = "AB123QW", Make = "Ford", Model = "Mustang", Version = "GT Premium Fasback", Description = "Ford Mustang GT Premium Fasback", Year = 2016, ValueHour = 50, Reservations = Array.Empty<Reservation>() });

            cars.Add(new Car()
            {
                Id = 2,
                Code = "SD345GF",
                Make = "Nissan",
                Model = "Rogue",
                Version = "SV AWD",
                Description = "Nissan Rogue SV AWD",
                Year = 2018,
                ValueHour = 65,
                Reservations = new List<Reservation>() { new Reservation(carId: 2, departmentId: 2, timeMinutes: 1230, carValue: 65) }
            });

            cars.Add(new Car()
            {
                Id = 3,
                Code = "HG456YT",
                Make = "Nissan",
                Model = "Sentra",
                Version = "S CVD",
                Description = "Nissan Sentra S CVD",
                Year = 2019,
                ValueHour = 70,
                Reservations = new List<Reservation>() { new Reservation(carId: 3, departmentId: 1, timeMinutes: 500, carValue: 70) }
            });

            return cars;
        }

        public IEnumerable<Department> GetDepartments()
        {
            var departments = new List<Department>();

            departments.Add(new Department()
            {
                Id = 1,
                Name = "IT",
                Reservations = new List<Reservation>() { new Reservation(carId: 3, departmentId: 1, timeMinutes: 500, carValue: 70) }
            });

            departments.Add(new Department()
            {
                Id = 2,
                Name = "Finance",
                Reservations = new List<Reservation>() { new Reservation(carId: 2, departmentId: 2, timeMinutes: 1230, carValue: 65) }
            });

            departments.Add(new Department() { Id = 3, Name = "Sales", Reservations = Array.Empty<Reservation>() });

            return departments;
        }

        public IEnumerable<Department> GetDepartments(IEnumerable<int> ids)
        {
            var departments = GetDepartments().Where(x => ids.Contains(x.Id));

            return departments;
        }
    }
}
