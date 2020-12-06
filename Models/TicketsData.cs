using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PISLabs.Models
{
    public class TicketsData
    {
        public Guid Id { get; set; } = Guid.Empty;
        public int FlightNumber { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public double Cost { get; set; }
        public string Place { get; set; }

        // Простейшая модель валидации. Легко можно улучшить
        public BaseModelValidationResult Validate()
        {
            var validationResult = new BaseModelValidationResult();
            if (FlightNumber <= 0) validationResult.Append($"Flight number cannot be negative number");
            if (string.IsNullOrWhiteSpace(From)) validationResult.Append($"Point of departure cannot be empty");
            if (string.IsNullOrWhiteSpace(To)) validationResult.Append($"Point of arrival cannot be empty");
            if (Cost <= 0 ) validationResult.Append($"Flight cost cannot be negative number");
            if (DepartureTime < DateTime.Now.AddHours(-12)) validationResult.Append($"Incorrect DepartureTime");
            if (ArrivalTime < DateTime.Now.AddHours(-12)) validationResult.Append($"Incorrect ArrivalTime");
            if (string.IsNullOrWhiteSpace(Place)) validationResult.Append($"Place cannot be undefined");

            return validationResult;
        }


        public override string ToString()
        {
            return $"Flight Num {FlightNumber} from {From} to {To}. Departure time: {DepartureTime}, arrival time: {ArrivalTime}. Cost: {Cost}. Place: {Place}";
        }
    }
}
