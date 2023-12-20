using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class AgeRangeAttribute : ValidationAttribute
{
    private readonly int _minAge;
    private readonly int _maxAge;
    private readonly bool _allowNull;

    public AgeRangeAttribute(int minAge, int maxAge, bool allowNull = false)
    {
        _minAge = minAge;
        _maxAge = maxAge;
        _allowNull = allowNull;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        // Sprawdź czy wartość jest null
        if (value is null)
        {
            // Jeśli null jest dozwolony, zwróć sukces walidacji
            if (_allowNull)
            {
                return ValidationResult.Success;
            }

            // Jeśli null nie jest dozwolony, zwróć błąd
            return new ValidationResult("Pole jest wymagane.", new[] { validationContext.MemberName });
        }

        // Sprawdź czy wartość jest poprawną datą
        if (!DateTime.TryParse(value.ToString(), out var dateOfBirth))
        {
            return new ValidationResult("Nieprawidłowa data urodzenia.", new[] { validationContext.MemberName });
        }

        // Oblicz wiek na podstawie daty urodzenia
        var age = CalculateAge(dateOfBirth);

        // Sprawdź czy wiek znajduje się w zakresie
        if (age < _minAge || age > _maxAge)
        {
            return new ValidationResult($"Proszę wprowadzić wiek między {_minAge} a {_maxAge}.", new[] { validationContext.MemberName });
        }

        return ValidationResult.Success;
    }

    // Metoda do obliczenia wieku
    private static int CalculateAge(DateTime birthDate)
    {
        var today = DateTime.Today;
        var age = today.Year - birthDate.Year;

        // Jeśli osoba jeszcze nie obchodziła urodzin w bieżącym roku, odejmujemy 1 rok od wieku
        if (birthDate > today.AddYears(-age))
        {
            age--;
        }

        return age;
    }
}