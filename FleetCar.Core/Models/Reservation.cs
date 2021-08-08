using System;

namespace FleetCar.Core.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public int CarId { get; set; }

        public int DepartmentId { get; set; }

        public DateTime ReservationDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public int TimeMinutes { get; set; }

        public decimal Value { get; set; }

        public Reservation()
        {

        }

        public Reservation(int carId, int departmentId, int timeMinutes, decimal carValue)
        {
            Id = carId + departmentId + timeMinutes;

            CarId = carId;

            DepartmentId = departmentId;

            TimeMinutes = timeMinutes;

            ReservationDate = DateTime.UtcNow;

            ReturnDate = DateTime.UtcNow.AddMinutes(timeMinutes);

            Value = Math.Ceiling(((decimal)timeMinutes / 60)) * carValue;
        }
    }
}
