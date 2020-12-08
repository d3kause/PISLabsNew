using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PISLabs.Models
{
    public class PassangersData
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; }
        public string Surename { get; set; }
        public string Patronomic { get; set; }
        public int Age { get; set; }
        public double BonusBalance { get; set; }


        // Простейшая модель валидации. Легко можно улучшить
        public BaseModelValidationResult Validate()
        {
            var validationResult = new BaseModelValidationResult();
            if (string.IsNullOrWhiteSpace(Name)) validationResult.Append($"Passanger's name cannot be empty");
            if (string.IsNullOrWhiteSpace(Surename)) validationResult.Append($"Passanger's surename cannot be empty");
            if (string.IsNullOrWhiteSpace(Patronomic)) validationResult.Append($"Passanger's patronomic cannot be empty");
            if (Age <= 0) validationResult.Append($"Passanger age cannot be negative number");
            if (BonusBalance < 0) validationResult.Append($"Bonus balance cannot be negative number");
            return validationResult;
        }

        public override string ToString()
        {
            return $"Passanger {Id}: {Name} {Surename} {Patronomic}, {Age} years old. Balance: {BonusBalance}";
        }
    }
}
