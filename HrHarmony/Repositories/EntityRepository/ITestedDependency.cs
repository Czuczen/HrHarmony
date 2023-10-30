using HrHarmony.Attributes;
using HrHarmony.Configuration.Dependencies.DependencyLifecycleInterfaces;

namespace HrHarmony.Repositories.EntityRepository
{
    [RegisterOpenGenericInterfaceInDI(typeof(ITestedDependency<,>))]
    public interface ITestedDependency<TEntity, TPrimaryKey>
    {
        public void Aa();

        public Guid InstanceGuid { get; }
    }
}