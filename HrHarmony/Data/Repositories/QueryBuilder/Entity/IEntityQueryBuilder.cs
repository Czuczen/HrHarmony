using HrHarmony.Configuration.Dependencies.DependencyLifecycleInterfaces;
using HrHarmony.Data.Models.Entities;

namespace HrHarmony.Data.Repositories.QueryBuilder.Entity;

public interface IEntityQueryBuilder<TEntity, TPrimaryKey> : ITransientDependency
    where TEntity : class, IEntity<TPrimaryKey>, new()
    where TPrimaryKey : struct
{
    public EntityQueryBuilder<TEntity, TPrimaryKey> WithBaseQuery(IQueryable<TEntity> baseQuery);

    public EntityQueryBuilder<TEntity, TPrimaryKey> WithTotalCount(int totalCount);

    public EntityQueryBuilder<TEntity, TPrimaryKey> WithOrderBy(string? orderBy);

    public EntityQueryBuilder<TEntity, TPrimaryKey> WithSearch(string searchString);

    public EntityQueryBuilder<TEntity, TPrimaryKey> WithIsDescending(bool isDescending);

    public EntityQueryBuilder<TEntity, TPrimaryKey> ApplyFieldSorting<T>()
        where T : class, new();

    public EntityQueryBuilder<TEntity, TPrimaryKey> ApplyOrdering();

    public EntityQueryBuilder<TEntity, TPrimaryKey> ApplySearchValueFilter<TViewModel>()
        where TViewModel : class, new();

    public IQueryable<TEntity> Build();
}