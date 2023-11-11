namespace HrHarmony.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class RegisterOpenGenericClassInDiAttribute : Attribute
{
    public Type ImplementationType { get; }

    public RegisterOpenGenericClassInDiAttribute(Type implementationType)
    {
        ImplementationType = implementationType;
    }
}