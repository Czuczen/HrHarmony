using HrHarmony.Configuration.Dependencies.DependencyLifecycleInterfaces;
using HrHarmony.Models.Entities;

namespace HrHarmony.Repositories.QueryBuilder.Entity.Base
{
    public interface IEntityQueryBuilder<TEntity, TPrimaryKey> : ITransientDependency
        where TEntity : class, IEntity<TPrimaryKey>, new()
        where TPrimaryKey : struct
    {
        public EntityQueryBuilder<TEntity, TPrimaryKey> WithBaseQuery(IQueryable<TEntity> baseQuery);

        public EntityQueryBuilder<TEntity, TPrimaryKey> WithTotalCount(int totalCount);

        public EntityQueryBuilder<TEntity, TPrimaryKey> WithOrderBy(string? orderBy);

        public EntityQueryBuilder<TEntity, TPrimaryKey> WithSearch(string searchString);

        public EntityQueryBuilder<TEntity, TPrimaryKey> WithIsDescending(bool isDescending);

        public EntityQueryBuilder<TEntity, TPrimaryKey> ApplySorting<T>()
           where T : class, new();

        public EntityQueryBuilder<TEntity, TPrimaryKey> ApplyOrdering();

        public IQueryable<TEntity> Build();
    }
}