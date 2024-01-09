using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HrHarmony.Data.Database.Converters;

public class DateOnlyToDateTimeConverter : ValueConverter<DateOnly, DateTime>
{
    public DateOnlyToDateTimeConverter() : base(
        dateOnly => new DateTime(dateOnly.Year, dateOnly.Month, dateOnly.Day),
        dateTime => DateOnly.FromDateTime(dateTime)
        )
    {

    }
}
