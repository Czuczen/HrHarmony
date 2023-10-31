using HrHarmony.Configuration.Dependencies.DependencyLifecycleInterfaces;

namespace HrHarmony.Services;

public interface IEmployeeService : ISingletonDependency
{
    public Guid InstanceGuid { get; }
}