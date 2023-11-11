namespace HrHarmony.Attributes;

[AttributeUsage(AttributeTargets.Interface)]
public class RegisterOpenGenericInterfaceInDiAttribute : Attribute
{
    public Type InterfaceType { get; }

    public RegisterOpenGenericInterfaceInDiAttribute(Type interfaceType)
    {
        InterfaceType = interfaceType;
    }
}