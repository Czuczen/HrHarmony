using System.Linq.Expressions;
using HrHarmony.Attributes;
using HrHarmony.Configuration.Dependencies.DependencyLifecycleInterfaces;
using HrHarmony.Data.Repositories.AccessLimiters.Selectable;
using HrHarmony.Models.Dto;
using HrHarmony.Models.Entities;
using HrHarmony.Models.Shared;

namespace HrHarmony.Data.Repositories.Dto;

[RegisterOpenGenericInterfaceInDI(typeof(IRepository<,,,,>))]
public interface IRepository<TEntity, TPrimaryKey, TEntityDto, in TUpdateDto, in TCreateDto> : IPerWebRequestDependency
    where TEntity : class, IEntity<TPrimaryKey>, new()
    where TPrimaryKey : struct
    where TEntityDto : class, IEntityDto<TPrimaryKey>, new()
    where TUpdateDto : class, IEntityDto<TPrimaryKey>, new()
    where TCreateDto : class, new()
{
    public TEntityDto GetById(TPrimaryKey id);

    public Task<TEntityDto> GetByIdAsync(TPrimaryKey id);

    public TEntityDto GetByIdWithRelated(TPrimaryKey id);

    public Task<TEntityDto> GetByIdWithRelatedAsync(TPrimaryKey id);

    public TReturn GetByIdWithRelatedAsCustomObject<TReturn>(TPrimaryKey id)
        where TReturn : class, new();

    public Task<TReturn> GetByIdWithRelatedAsCustomObjectAsync<TReturn>(TPrimaryKey id)
        where TReturn : class, new();

    public TEntityDto GetByIdWithCustomFields(TPrimaryKey id, Func<Selectable<TEntity, TEntityDto>, IQueryable<TEntityDto>> customProjection);

    public Task<TEntityDto> GetByIdWithCustomFieldsAsync(TPrimaryKey id, Func<Selectable<TEntity, TEntityDto>, IQueryable<TEntityDto>> customProjection);

    public TReturn GetByIdAsCustomObject<TReturn>(TPrimaryKey id)
        where TReturn : class, new ();

    public Task<TReturn> GetByIdAsCustomObjectAsync<TReturn>(TPrimaryKey id)
        where TReturn : class, new();

    public IEnumerable<TEntityDto> GetWhere(Expression<Func<TEntity, bool>> predicate);

    public Task<IEnumerable<TEntityDto>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate);

    public IEnumerable<TEntityDto> GetWhereWithRelated(Expression<Func<TEntity, bool>> predicate);

    public Task<IEnumerable<TEntityDto>> GetWhereWithRelatedAsync(Expression<Func<TEntity, bool>> predicate);

    public IQueryable<TEntityDto> GetQuery(Expression<Func<TEntity, bool>> predicate);

    public IQueryable<TEntityDto> GetQuery(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryBuilder);

    public IQueryable<TQEntity> GetQuery<TQEntity>() 
        where TQEntity : class, IEntity<TPrimaryKey>, new();

    public IQueryable<TQEntity> GetQuery<TQEntity>(Expression<Func<TQEntity, bool>> predicate)
        where TQEntity : class, IEntity<TPrimaryKey>, new();
    
    public IQueryable<TQEntity> GetQuery<TQEntity>(Func<IQueryable<TQEntity>, IQueryable<TQEntity>> queryBuilder)
        where TQEntity : class, IEntity<TPrimaryKey>, new();

    public IQueryable<TReturn> GetQuery<TQEntity, TReturn>(Func<IQueryable<TQEntity>, IQueryable<TReturn>> queryBuilder)
        where TQEntity : class, IEntity<TPrimaryKey>, new()
        where TReturn : class, new();

    public IEnumerable<TEntityDto> ExecuteQuery(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryBuilder);

    public Task<IEnumerable<TEntityDto>> ExecuteQueryAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryBuilder);

    public Task<IEnumerable<TEntityDto>> GetWhere(string key, TPrimaryKey id);

    public PaginatedResult<TEntityDto> GetPagedEntities<TIndexViewModel>(PaginationRequest req)
        where TIndexViewModel : class, new();

    public Task<PaginatedResult<TEntityDto>> GetPagedEntitiesAsync<TViewModel>(PaginationRequest req)
       where TViewModel : class, new();

    public PaginatedResult<TEntityDto> GetPagedEntitiesWithRelated<TViewModel>(PaginationRequest req)
        where TViewModel : class, new();

    public Task<PaginatedResult<TEntityDto>> GetPagedEntitiesWithRelatedAsync<TViewModel>(PaginationRequest req)
        where TViewModel : class, new();

    public PaginatedResult<TEntityDto> GetPagedEntitiesWithCustomFields<TViewModel>(PaginationRequest req,
        Func<Selectable<TEntity, TEntityDto>, IQueryable<TEntityDto>> customProjection)
        where TViewModel : class, new();

    public Task<PaginatedResult<TEntityDto>> GetPagedEntitiesWithCustomFieldsAsync<TViewModel>(PaginationRequest req,
        Func<Selectable<TEntity, TEntityDto>, IQueryable<TEntityDto>> customProjection)
        where TViewModel : class, new();

    public PaginatedResult<TReturn> GetPagedEntitiesAsCustomObject<TReturn>(PaginationRequest req)
        where TReturn : class, new();

    public Task<PaginatedResult<TReturn>> GetPagedEntitiesAsCustomObjectAsync<TReturn>(PaginationRequest req)
        where TReturn : class, new();

    public IEnumerable<TEntityDto> GetAll();

    public Task<IEnumerable<TEntityDto>> GetAllAsync();

    public IEnumerable<TEntityDto> GeAllWithRelated();

    public Task<IEnumerable<TEntityDto>> GetAllWithRelatedAsync();

    public TEntityDto Create(TCreateDto entity);

    public Task<TEntityDto> CreateAsync(TCreateDto entity);

    public TEntityDto Update(TUpdateDto entity);

    public Task<TEntityDto> UpdateAsync(TUpdateDto entity);

    public void Delete(TPrimaryKey id);

    public Task DeleteAsync(TPrimaryKey id);

    public void Delete(TEntityDto entity);

    public Task DeleteAsync(TEntityDto entity);
}