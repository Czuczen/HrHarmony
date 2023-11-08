using HrHarmony.Attributes;
using HrHarmony.Configuration.Dependencies.DependencyLifecycleInterfaces;
using HrHarmony.Data.Repositories.AccessLimiters.Selectable;
using HrHarmony.Models.Entities;
using HrHarmony.Models.Shared;
using System.Linq.Expressions;

namespace HrHarmony.Data.Repositories.Entity;

[RegisterOpenGenericInterfaceInDI(typeof(IRepository<,>))]
public interface IRepository<TEntity, TPrimaryKey> : IPerWebRequestDependency
    where TEntity : class, IEntity<TPrimaryKey>, new()
    where TPrimaryKey : struct
{
    public TEntity GetById(TPrimaryKey id);

    public Task<TEntity> GetByIdAsync(TPrimaryKey id);

    public TEntity GetByIdWithRelated(TPrimaryKey id);

    public Task<TEntity> GetByIdWithRelatedAsync(TPrimaryKey id);

    public TReturn GetByIdWithRelatedAsCustomObject<TReturn>(TPrimaryKey id)
        where TReturn : class, new();

    public Task<TReturn> GetByIdWithRelatedAsCustomObjectAsync<TReturn>(TPrimaryKey id)
        where TReturn : class, new();

    public TEntity GetByIdWithCustomFields(TPrimaryKey id, Func<Selectable<TEntity, TEntity>, IQueryable<TEntity>> customProjection);

    public Task<TEntity> GetByIdWithCustomFieldsAsync(TPrimaryKey id, Func<Selectable<TEntity, TEntity>, IQueryable<TEntity>> customProjection);

    public TReturn GetByIdAsCustomObject<TReturn>(TPrimaryKey id)
        where TReturn : class, new();

    public Task<TReturn> GetByIdAsCustomObjectAsync<TReturn>(TPrimaryKey id)
        where TReturn : class, new();

    public IEnumerable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate);

    public Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate);

    public IEnumerable<TEntity> GetWhereWithRelated(Expression<Func<TEntity, bool>> predicate);

    public Task<IEnumerable<TEntity>> GetWhereWithRelatedAsync(Expression<Func<TEntity, bool>> predicate);

    public IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> predicate);

    public IQueryable<TEntity> GetQuery(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryBuilder);

    public IQueryable<TQEntity> GetQuery<TQEntity>() 
        where TQEntity : class, IEntity<TPrimaryKey>, new();

    public IQueryable<TQEntity> GetQuery<TQEntity>(Expression<Func<TQEntity, bool>> predicate)
     where TQEntity : class, IEntity<TPrimaryKey>, new();

    public IQueryable<TQEntity> GetQuery<TQEntity>(Func<IQueryable<TQEntity>, IQueryable<TQEntity>> queryBuilder)
        where TQEntity : class, IEntity<TPrimaryKey>, new();

    public IQueryable<TReturn> GetQuery<TQEntity, TReturn>(Func<IQueryable<TQEntity>, IQueryable<TReturn>> queryBuilder)
        where TQEntity : class, IEntity<TPrimaryKey>, new()
        where TReturn : class, new();

    public IEnumerable<TEntity> ExecuteQuery(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryBuilder);

    public Task<IEnumerable<TEntity>> ExecuteQueryAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryBuilder);

    public PaginatedResult<TEntity> GetPagedEntities<TIndexViewModel>(PaginationRequest req)
        where TIndexViewModel : class, new();

    public Task<PaginatedResult<TEntity>> GetPagedEntitiesAsync<TViewModel>(PaginationRequest req)
       where TViewModel : class, new();

    public PaginatedResult<TEntity> GetPagedEntitiesWithRelated<TViewModel>(PaginationRequest req)
        where TViewModel : class, new();

    public Task<PaginatedResult<TEntity>> GetPagedEntitiesWithRelatedAsync<TViewModel>(PaginationRequest req)
        where TViewModel : class, new();

    public PaginatedResult<TEntity> GetPagedEntitiesWithCustomFields<TViewModel>(PaginationRequest req,
        Func<Selectable<TEntity, TEntity>, IQueryable<TEntity>> customProjection)
        where TViewModel : class, new();

    public Task<PaginatedResult<TEntity>> GetPagedEntitiesWithCustomFieldsAsync<TViewModel>(PaginationRequest req,
        Func<Selectable<TEntity, TEntity>, IQueryable<TEntity>> customProjection)
        where TViewModel : class, new();

    public PaginatedResult<TReturn> GetPagedEntitiesAsCustomObject<TReturn>(PaginationRequest req)
        where TReturn : class, new();

    public Task<PaginatedResult<TReturn>> GetPagedEntitiesAsCustomObjectAsync<TReturn>(PaginationRequest req)
        where TReturn : class, new();

    public IEnumerable<TEntity> GetAll();

    public Task<IEnumerable<TEntity>> GetAllAsync();

    public IEnumerable<TEntity> GeAllWithRelated();

    public Task<IEnumerable<TEntity>> GetAllWithRelatedAsync();

    public TEntity Create(TEntity entity);

    public Task<TEntity> CreateAsync(TEntity entity);

    public TEntity Update(TEntity entity);

    public Task<TEntity> UpdateAsync(TEntity entity);

    public void Delete(TPrimaryKey id);

    public Task DeleteAsync(TPrimaryKey id);

    public void Delete(TEntity entity);

    public Task DeleteAsync(TEntity entity);
}