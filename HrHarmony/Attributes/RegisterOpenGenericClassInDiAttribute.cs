namespace HrHarmony.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
sealed class RegisterOpenGenericClassInDIAttribute : Attribute
{
    public Type ImplementationType { get; }

    public RegisterOpenGenericClassInDIAttribute(Type implementationType)
    {
        ImplementationType = implementationType;
    }
}