using HrHarmony.Configuration.Dependencies.DependencyLifecycleInterfaces;

namespace HrHarmony.Services
{
    public interface ITestEmloyee : ISingletonDependency
    {
        public Guid InstanceGuid { get; }
    }
}