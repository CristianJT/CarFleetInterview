using System;

namespace FleetCar.Core.Models
{
    public class CarReservation
    {
        public int ReservationId { get; set; }

        public int CarId { get; set; }

        public string CarDescription { get; set; }

        public DateTime? ReservationDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public decimal? ReservationValue { get; set; }

        public string Department { get; set; }

        public CarReservation(Car car, Reservation reservation)
        {
            ReservationId = reservation.Id;

            CarId = car.Id;

            CarDescription = car.Description;

            ReservationDate = reservation.ReservationDate;

            ReturnDate = reservation.ReturnDate;

            ReservationValue = reservation.Value;
        }

        public CarReservation(Car car, Reservation reservation, Department department) : this(car, reservation)
        {
            Department = department.Name;
        }
    }
}
