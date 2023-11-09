using AutoMapper;
using AutoMapper.QueryableExtensions;
using HrHarmony.Attributes;
using HrHarmony.Data.Database;
using HrHarmony.Data.Repositories.AccessLimiters.Selectable;
using HrHarmony.Data.Repositories.QueryBuilder.Pagination;
using HrHarmony.Exceptions;
using HrHarmony.Models.Entities;
using HrHarmony.Models.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HrHarmony.Data.Repositories.Entity;

[RegisterOpenGenericClassInDI(typeof(Repository<,>))]
public class Repository<TEntity, TPrimaryKey> :
    IRepository<TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>, new()
    where TPrimaryKey : struct
{
    private readonly ApplicationDbContext _ctx;
    private readonly IMapper _mapper;
    private readonly IPaginatedQueryBuilder<TEntity, TPrimaryKey> _paginatedQueryBuilder;

    public Repository(
        ApplicationDbContext context,
        IMapper mapper,
        IPaginatedQueryBuilder<TEntity, TPrimaryKey> paginatedQueryBuilder
        )
    {
        _ctx = context;
        _mapper = mapper;
        _paginatedQueryBuilder = paginatedQueryBuilder;
    }

    public TEntity GetById(TPrimaryKey id)
    {
        var entity = _ctx.Set<TEntity>().Find(id);
        if (entity == null)
            throw new EntityNotFoundException($"Entity of type {typeof(TEntity).FullName} with ID {id} was not found.");

        return entity;
    }

    public async Task<TEntity> GetByIdAsync(TPrimaryKey id)
    {
        var entity = await _ctx.Set<TEntity>().FindAsync(id);
        if (entity == null)
            throw new EntityNotFoundException($"Entity of type {typeof(TEntity).FullName} with ID {id} was not found.");

        return entity;
    }

    public TEntity GetByIdWithRelated(TPrimaryKey id)
    {
        var query = _ctx.Set<TEntity>().Where(e => e.Id.Equals(id));
        return query.ProjectTo<TEntity>(_mapper.ConfigurationProvider).Single();
    }

    public async Task<TEntity> GetByIdWithRelatedAsync(TPrimaryKey id)
    {
        var query = _ctx.Set<TEntity>().Where(e => e.Id.Equals(id));
        return await query.ProjectTo<TEntity>(_mapper.ConfigurationProvider).SingleAsync();
    }

    public TReturn GetByIdWithRelatedAsCustomObject<TReturn>(TPrimaryKey id)
        where TReturn : class, new()
    {
        var query = _ctx.Set<TEntity>().Where(e => e.Id.Equals(id));
        return query.ProjectTo<TReturn>(_mapper.ConfigurationProvider).Single();
    }

    public async Task<TReturn> GetByIdWithRelatedAsCustomObjectAsync<TReturn>(TPrimaryKey id)
        where TReturn : class, new()
    {
        var query = _ctx.Set<TEntity>().Where(e => e.Id.Equals(id));
        return await query.ProjectTo<TReturn>(_mapper.ConfigurationProvider).SingleAsync();
    }

    public TEntity GetByIdWithCustomFields(TPrimaryKey id,
        Func<Selectable<TEntity, TEntity>, IQueryable<TEntity>> customProjection)
    {
        var query = _ctx.Set<TEntity>().Where(e => e.Id.Equals(id));
        var selectable = new Selectable<TEntity, TEntity>(query);

        var customResult = customProjection(selectable);
        var entity = customResult.ToList().Single();

        return entity;
    }

    public async Task<TEntity> GetByIdWithCustomFieldsAsync(TPrimaryKey id,
        Func<Selectable<TEntity, TEntity>, IQueryable<TEntity>> customProjection)
    {
        var query = _ctx.Set<TEntity>().Where(e => e.Id.Equals(id));
        var selectable = new Selectable<TEntity, TEntity>(query);

        var customResult = customProjection(selectable);
        var entity = (await customResult.ToListAsync()).Single();

        return entity;
    }

    public TReturn GetByIdAsCustomObject<TReturn>(TPrimaryKey id)
        where TReturn : class, new()
    {
        var query = _ctx.Set<TEntity>().Where(e => e.Id.Equals(id));
        var customResult = query.Select(e => _mapper.Map<TReturn>(e));
        var entity = customResult.ToList().Single();

        return entity;
    }

    public async Task<TReturn> GetByIdAsCustomObjectAsync<TReturn>(TPrimaryKey id)
        where TReturn : class, new()
    {
        var query = _ctx.Set<TEntity>().Where(e => e.Id.Equals(id));
        var customResult = query.Select(e => _mapper.Map<TReturn>(e));
        var entity = (await customResult.ToListAsync()).Single();

        return entity;
    }

    public IEnumerable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate)
    {
        return _ctx.Set<TEntity>().Where(predicate).ToList();
    }

    public async Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _ctx.Set<TEntity>().Where(predicate).ToListAsync();
    }

    public IEnumerable<TEntity> GetWhereWithRelated(Expression<Func<TEntity, bool>> predicate)
    {
        var query = _ctx.Set<TEntity>().Where(predicate);
        return query.ProjectTo<TEntity>(_mapper.ConfigurationProvider).ToList();
    }

    public async Task<IEnumerable<TEntity>> GetWhereWithRelatedAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var query = _ctx.Set<TEntity>().Where(predicate);
        return await query.ProjectTo<TEntity>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> predicate)
    {
        return _ctx.Set<TEntity>().Where(predicate);
    }

    public IQueryable<TEntity> GetQuery(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryBuilder)
    {
        var query = _ctx.Set<TEntity>().AsQueryable();
        return queryBuilder(query);
    }

    public IQueryable<TQEntity> GetQuery<TQEntity>() 
        where TQEntity : class, IEntity<TPrimaryKey>, new()
    {
        return _ctx.Set<TQEntity>().AsQueryable();
    }

    public IQueryable<TQEntity> GetQuery<TQEntity>(Expression<Func<TQEntity, bool>> predicate)
        where TQEntity : class, IEntity<TPrimaryKey>, new()
    {
        return _ctx.Set<TQEntity>().Where(predicate);
    }

    public IQueryable<TQEntity> GetQuery<TQEntity>(Func<IQueryable<TQEntity>, IQueryable<TQEntity>> queryBuilder)
        where TQEntity : class, IEntity<TPrimaryKey>, new()
    {
        var query = _ctx.Set<TQEntity>().AsQueryable();
        return queryBuilder(query);
    }

    public IQueryable<TReturn> GetQuery<TQEntity, TReturn>(Func<IQueryable<TQEntity>, IQueryable<TReturn>> queryBuilder)
        where TQEntity : class, IEntity<TPrimaryKey>, new()
        where TReturn : class, new()
    {
        var query = _ctx.Set<TQEntity>().AsQueryable();
        return queryBuilder(query);
    }

    public IEnumerable<TEntity> ExecuteQuery(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryBuilder)
    {
        var query = _ctx.Set<TEntity>().AsQueryable();
        query = queryBuilder(query);

        return query.ToList();
    }

    public async Task<IEnumerable<TEntity>> ExecuteQueryAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryBuilder)
    {
        var query = _ctx.Set<TEntity>().AsQueryable();
        query = queryBuilder(query);

        return await query.ToListAsync();
    }

    public PaginatedResult<TEntity> GetPagedEntities<TViewModel>(PaginationRequest req)
        where TViewModel : class, new()
    {
        var pagedQuery = _paginatedQueryBuilder.WithBaseQuery(_ctx.Set<TEntity>().AsQueryable()).Build<TViewModel>(req);
        var entities = pagedQuery.Query.ToList();

        var result = _mapper.Map<PaginatedResult<TEntity>>(pagedQuery);
        result.Items = entities;

        return result;
    }

    public async Task<PaginatedResult<TEntity>> GetPagedEntitiesAsync<TViewModel>(PaginationRequest req)
        where TViewModel : class, new()
    {
        var pagedQuery = await _paginatedQueryBuilder.WithBaseQuery(_ctx.Set<TEntity>().AsQueryable()).BuildAsync<TViewModel>(req);
        var entities = await pagedQuery.Query.ToListAsync();

        var result = _mapper.Map<PaginatedResult<TEntity>>(pagedQuery);
        result.Items = entities;

        return result;
    }

    public PaginatedResult<TEntity> GetPagedEntitiesWithRelated<TViewModel>(PaginationRequest req)
        where TViewModel : class, new()
    {
        var pagedQuery = _paginatedQueryBuilder.WithBaseQuery(_ctx.Set<TEntity>().AsQueryable()).Build<TViewModel>(req);
        var entities = pagedQuery.Query.ProjectTo<TEntity>(_mapper.ConfigurationProvider).ToList();

        var result = _mapper.Map<PaginatedResult<TEntity>>(pagedQuery);
        result.Items = entities;

        return result;
    }

    public async Task<PaginatedResult<TEntity>> GetPagedEntitiesWithRelatedAsync<TViewModel>(PaginationRequest req)
        where TViewModel : class, new()
    {
        var pagedQuery = await _paginatedQueryBuilder.WithBaseQuery(_ctx.Set<TEntity>().AsQueryable()).BuildAsync<TViewModel>(req);
        var entities = await pagedQuery.Query.ProjectTo<TEntity>(_mapper.ConfigurationProvider).ToListAsync();

        var result = _mapper.Map<PaginatedResult<TEntity>>(pagedQuery);
        result.Items = entities;

        return result;
    }

    public PaginatedResult<TEntity> GetPagedEntitiesWithCustomFields<TViewModel>(PaginationRequest req,
        Func<Selectable<TEntity, TEntity>, IQueryable<TEntity>> customProjection)
        where TViewModel : class, new()
    {
        var pagedQuery = _paginatedQueryBuilder.WithBaseQuery(_ctx.Set<TEntity>().AsQueryable()).Build<TViewModel>(req);

        var selectable = new Selectable<TEntity, TEntity>(pagedQuery.Query);
        var customResult = customProjection(selectable);

        var result = _mapper.Map<PaginatedResult<TEntity>>(pagedQuery);
        result.Items = customResult.ToList();

        return result;
    }

    public async Task<PaginatedResult<TEntity>> GetPagedEntitiesWithCustomFieldsAsync<TViewModel>(PaginationRequest req,
        Func<Selectable<TEntity, TEntity>, IQueryable<TEntity>> customProjection)
        where TViewModel : class, new()
    {
        var pagedQuery = await _paginatedQueryBuilder.WithBaseQuery(_ctx.Set<TEntity>().AsQueryable()).BuildAsync<TViewModel>(req);

        var selectable = new Selectable<TEntity, TEntity>(pagedQuery.Query);
        var customResult = customProjection(selectable);

        var result = _mapper.Map<PaginatedResult<TEntity>>(pagedQuery);
        result.Items = await customResult.ToListAsync();

        return result;
    }

    public PaginatedResult<TReturn> GetPagedEntitiesAsCustomObject<TReturn>(PaginationRequest req)
        where TReturn : class, new()
    {
        var pagedQuery = _paginatedQueryBuilder.WithBaseQuery(_ctx.Set<TEntity>().AsQueryable()).Build<TReturn>(req);
        var customResult = pagedQuery.Query.Select(e => _mapper.Map<TReturn>(e));

        var result = _mapper.Map<PaginatedResult<TReturn>>(pagedQuery);
        result.Items = customResult.ToList();

        return result;
    }

    public async Task<PaginatedResult<TReturn>> GetPagedEntitiesAsCustomObjectAsync<TReturn>(PaginationRequest req)
        where TReturn : class, new()
    {
        var pagedQuery = await _paginatedQueryBuilder.WithBaseQuery(_ctx.Set<TEntity>().AsQueryable()).BuildAsync<TReturn>(req);
        var customResult = pagedQuery.Query.Select(e => _mapper.Map<TReturn>(e));

        var result = _mapper.Map<PaginatedResult<TReturn>>(pagedQuery);
        result.Items = await customResult.ToListAsync();

        return result;
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _ctx.Set<TEntity>().ToList();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _ctx.Set<TEntity>().ToListAsync();
    }

    public IEnumerable<TEntity> GeAllWithRelated()
    {
        var query = _ctx.Set<TEntity>().AsQueryable();
        return query.ProjectTo<TEntity>(_mapper.ConfigurationProvider).ToList();
    }

    public async Task<IEnumerable<TEntity>> GetAllWithRelatedAsync()
    {
        var query = _ctx.Set<TEntity>().AsQueryable();
        return await query.ProjectTo<TEntity>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public TEntity Create(TEntity entity)
    {
        var entityEntry = _ctx.Set<TEntity>().Add(entity);
        _ctx.SaveChanges();

        return entityEntry.Entity;
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        var entityEntry = await _ctx.Set<TEntity>().AddAsync(entity);
        await _ctx.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public TEntity Update(TEntity entity)
    {
        var entityEntry = _ctx.Set<TEntity>().Update(entity);
        _ctx.SaveChanges();

        return entityEntry.Entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var entityEntry = _ctx.Set<TEntity>().Update(entity);
        await _ctx.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public void Delete(TPrimaryKey id)
    {
        var entity = _ctx.Set<TEntity>().Find(id);
        if (entity == null)
            throw new EntityNotFoundException($"Entity of type {typeof(TEntity).FullName} with ID {id} was not found.");

        _ctx.Set<TEntity>().Remove(entity);
        _ctx.SaveChanges();
    }

    public async Task DeleteAsync(TPrimaryKey id)
    {
        var entity = await _ctx.Set<TEntity>().FindAsync(id);
        if (entity == null)
            throw new EntityNotFoundException($"Entity of type {typeof(TEntity).FullName} with ID {id} was not found.");

        _ctx.Set<TEntity>().Remove(entity);
        await _ctx.SaveChangesAsync();
    }

    public void Delete(TEntity entity)
    {
        _ctx.Set<TEntity>().Remove(entity);
        _ctx.SaveChanges();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _ctx.Set<TEntity>().Remove(entity);
        await _ctx.SaveChangesAsync();
    }
}