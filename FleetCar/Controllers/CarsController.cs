using FleetCar.Core.Models;
using FleetCar.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FleetCar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarsController : Controller
    {
        private readonly ICar _service;

        public CarsController(ICar service)
        {
            _service = service;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            var cars = _service.Get();

            return Ok(cars);
        }

        [HttpGet("notreturned")]
        public IActionResult GetNotReturned(SortProperty? property = SortProperty.RETURN_DATE, SortCriteria? criteria = SortCriteria.ASC)
        {
            var cars = _service.GetNotReturned(property, criteria);

            return Ok(cars);
        }

        [HttpGet("{carId}/reservations")]
        public IActionResult GetReservations(int carId)
        {
            try
            {
                var reservations = _service.GetReservations(carId);

                return Ok(reservations);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("{carId}/reserve")]
        public IActionResult PosReserve(int carId, [FromBody] CarReservationNew newReservation)
        {
            try
            {
                var reservation = _service.AddReservation(carId, newReservation.DepartmentId, newReservation.TimeMinutes);

                return Ok(reservation);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
