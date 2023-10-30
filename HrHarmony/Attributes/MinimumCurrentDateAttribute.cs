using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class MinimumCurrentDateAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is DateTime date)
        {
            return date.Date >= DateTime.Now.Date;
        }

        return false;
    }
}