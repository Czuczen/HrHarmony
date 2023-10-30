namespace HrHarmony.Attributes
{
    [AttributeUsage(AttributeTargets.Interface, Inherited = false, AllowMultiple = false)]
    public class RegisterOpenGenericInterfaceInDIAttribute : Attribute
    {
        public Type InterfaceType { get; }

        public RegisterOpenGenericInterfaceInDIAttribute(Type interfaceType)
        {
            InterfaceType = interfaceType;
        }
    }
}