﻿using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HrHarmony.Attributes;
using HrHarmony.Data.Database;
using HrHarmony.Data.Models.Dto;
using HrHarmony.Data.Models.Entities;
using HrHarmony.Data.Models.Shared;
using HrHarmony.Data.Repositories.AccessLimiters.Selectable;
using HrHarmony.Data.Repositories.QueryBuilder.Pagination;
using HrHarmony.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HrHarmony.Data.Repositories.Dto;

[RegisterOpenGenericClassInDi(typeof(Repository<,,,,>))]
public class Repository<TEntity, TPrimaryKey, TEntityDto, TUpdateDto, TCreateDto> :
    IRepository<TEntity, TPrimaryKey, TEntityDto, TUpdateDto, TCreateDto>
    where TEntity : class, IEntity<TPrimaryKey>, new()
    where TPrimaryKey : struct
    where TEntityDto : class, IEntityDto<TPrimaryKey>, new()
    where TUpdateDto : class, IEntityDto<TPrimaryKey>, new()
    where TCreateDto : class, new()
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

    public TEntityDto GetById(TPrimaryKey id)
    {
        var entity = _ctx.Set<TEntity>().Find(id);
        if (entity == null)
            throw new EntityNotFoundException($"Entity of type {typeof(TEntity).FullName} with ID {id} was not found.");

        return _mapper.Map<TEntityDto>(entity);
    }

    public async Task<TEntityDto> GetByIdAsync(TPrimaryKey id)
    {
        var entity = await _ctx.Set<TEntity>().FindAsync(id);
        if (entity == null)
            throw new EntityNotFoundException($"Entity of type {typeof(TEntity).FullName} with ID {id} was not found.");

        return _mapper.Map<TEntityDto>(entity);
    }

    public TEntityDto GetByIdWithRelated(TPrimaryKey id)
    {
        var query = _ctx.Set<TEntity>().Where(e => e.Id.Equals(id));
        return query.ProjectTo<TEntityDto>(_mapper.ConfigurationProvider).Single();
    }

    public async Task<TEntityDto> GetByIdWithRelatedAsync(TPrimaryKey id)
    {
        var query = _ctx.Set<TEntity>().Where(e => e.Id.Equals(id));
        return await query.ProjectTo<TEntityDto>(_mapper.ConfigurationProvider).SingleAsync();
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

    public TEntityDto GetByIdWithCustomFields(TPrimaryKey id,
        Func<Selectable<TEntity, TEntityDto>, IQueryable<TEntityDto>> customProjection)
    {
        var query = _ctx.Set<TEntity>().Where(e => e.Id.Equals(id));
        var selectable = new Selectable<TEntity, TEntityDto>(query);

        var customResult = customProjection(selectable);
        var entity = customResult.ToList().Single();

        return entity;
    }

    public async Task<TEntityDto> GetByIdWithCustomFieldsAsync(TPrimaryKey id,
        Func<Selectable<TEntity, TEntityDto>, IQueryable<TEntityDto>> customProjection)
    {
        var query = _ctx.Set<TEntity>().Where(e => e.Id.Equals(id));
        var selectable = new Selectable<TEntity, TEntityDto>(query);

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

    public IEnumerable<TEntityDto> GetWhere(Expression<Func<TEntity, bool>> predicate)
    {
        var entities = _ctx.Set<TEntity>().Where(predicate).ToList();
        return _mapper.Map<IEnumerable<TEntityDto>>(entities);
    }

    public async Task<IEnumerable<TEntityDto>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var entities = await _ctx.Set<TEntity>().Where(predicate).ToListAsync();
        return _mapper.Map<IEnumerable<TEntityDto>>(entities);
    }

    public IEnumerable<TEntityDto> GetWhereWithRelated(Expression<Func<TEntity, bool>> predicate)
    {
        var query = _ctx.Set<TEntity>().Where(predicate);
        return query.ProjectTo<TEntityDto>(_mapper.ConfigurationProvider).ToList();
    }

    public async Task<IEnumerable<TEntityDto>> GetWhereWithRelatedAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var query = _ctx.Set<TEntity>().Where(predicate);
        return await query.ProjectTo<TEntityDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public IQueryable<TEntityDto> GetQuery(Expression<Func<TEntity, bool>> predicate)
    {
        var query = _ctx.Set<TEntity>().Where(predicate);
        return query.Select(entity => _mapper.Map<TEntityDto>(entity));
    }

    public IQueryable<TEntityDto> GetQuery(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryBuilder)
    {
        var query = _ctx.Set<TEntity>().AsQueryable();
        query = queryBuilder(query);

        return query.Select(entity => _mapper.Map<TEntityDto>(entity));
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

    public IEnumerable<TEntityDto> ExecuteQuery(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryBuilder)
    {
        var query = _ctx.Set<TEntity>().AsQueryable();
        query = queryBuilder(query);

        return _mapper.Map<IEnumerable<TEntityDto>>(query.ToList());
    }

    public async Task<IEnumerable<TEntityDto>> ExecuteQueryAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryBuilder)
    {
        var query = _ctx.Set<TEntity>().AsQueryable();
        query = queryBuilder(query);

        return _mapper.Map<IEnumerable<TEntityDto>>(await query.ToListAsync());
    }

    public PaginatedResult<TEntityDto> GetPagedEntities<TViewModel>(PaginationRequest req)
        where TViewModel : class, new()
    {
        var pagedQuery = _paginatedQueryBuilder.WithBaseQuery(_ctx.Set<TEntity>().AsQueryable()).Build<TViewModel>(req);
        var entities = _mapper.Map<IEnumerable<TEntityDto>>(pagedQuery.Query.ToList());

        var result = _mapper.Map<PaginatedResult<TEntityDto>>(pagedQuery);
        result.Items = entities;

        return result;
    }

    public async Task<PaginatedResult<TEntityDto>> GetPagedEntitiesAsync<TViewModel>(PaginationRequest req)
        where TViewModel : class, new()
    {
        var pagedQuery = await _paginatedQueryBuilder.WithBaseQuery(_ctx.Set<TEntity>().AsQueryable()).BuildAsync<TViewModel>(req);
        var entities = _mapper.Map<IEnumerable<TEntityDto>>(await pagedQuery.Query.ToListAsync());

        var result = _mapper.Map<PaginatedResult<TEntityDto>>(pagedQuery);
        result.Items = entities;

        return result;
    }

    public PaginatedResult<TEntityDto> GetPagedEntitiesWithRelated<TViewModel>(PaginationRequest req)
        where TViewModel : class, new()
    {
        var pagedQuery = _paginatedQueryBuilder.WithBaseQuery(_ctx.Set<TEntity>().AsQueryable()).Build<TViewModel>(req);
        var entities = pagedQuery.Query.ProjectTo<TEntityDto>(_mapper.ConfigurationProvider).ToList();

        var result = _mapper.Map<PaginatedResult<TEntityDto>>(pagedQuery);
        result.Items = entities;

        return result;
    }

    public async Task<PaginatedResult<TEntityDto>> GetPagedEntitiesWithRelatedAsync<TViewModel>(PaginationRequest req)
        where TViewModel : class, new()
    {
        var pagedQuery = await _paginatedQueryBuilder.WithBaseQuery(_ctx.Set<TEntity>().AsQueryable()).BuildAsync<TViewModel>(req);
        var entities = await pagedQuery.Query.ProjectTo<TEntityDto>(_mapper.ConfigurationProvider).ToListAsync();

        var result = _mapper.Map<PaginatedResult<TEntityDto>>(pagedQuery);
        result.Items = entities;

        return result;
    }

    public PaginatedResult<TEntityDto> GetPagedEntitiesWithCustomFields<TViewModel>(PaginationRequest req,
        Func<Selectable<TEntity, TEntityDto>, IQueryable<TEntityDto>> customProjection)
        where TViewModel : class, new()
    {
        var pagedQuery = _paginatedQueryBuilder.WithBaseQuery(_ctx.Set<TEntity>().AsQueryable()).Build<TViewModel>(req);

        var selectable = new Selectable<TEntity, TEntityDto>(pagedQuery.Query);
        var customResult = customProjection(selectable);

        var result = _mapper.Map<PaginatedResult<TEntityDto>>(pagedQuery);
        result.Items = customResult.ToList();

        return result;
    }

    public async Task<PaginatedResult<TEntityDto>> GetPagedEntitiesWithCustomFieldsAsync<TViewModel>(PaginationRequest req,
        Func<Selectable<TEntity, TEntityDto>, IQueryable<TEntityDto>> customProjection)
        where TViewModel : class, new()
    {
        var pagedQuery = await _paginatedQueryBuilder.WithBaseQuery(_ctx.Set<TEntity>().AsQueryable()).BuildAsync<TViewModel>(req);

        var selectable = new Selectable<TEntity, TEntityDto>(pagedQuery.Query);
        var customResult = customProjection(selectable);

        var result = _mapper.Map<PaginatedResult<TEntityDto>>(pagedQuery);
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

    public IEnumerable<TEntityDto> GetAll()
    {
        var entities = _ctx.Set<TEntity>().ToList();
        return _mapper.Map<IEnumerable<TEntityDto>>(entities);
    }

    public async Task<IEnumerable<TEntityDto>> GetAllAsync()
    {
        var entities = await _ctx.Set<TEntity>().ToListAsync();
        return _mapper.Map<IEnumerable<TEntityDto>>(entities);
    }

    public IEnumerable<TEntityDto> GeAllWithRelated()
    {
        var query = _ctx.Set<TEntity>().AsQueryable();
        return query.ProjectTo<TEntityDto>(_mapper.ConfigurationProvider).ToList();
    }

    public async Task<IEnumerable<TEntityDto>> GetAllWithRelatedAsync()
    {
        var query = _ctx.Set<TEntity>().AsQueryable();
        return await query.ProjectTo<TEntityDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public TEntityDto Create(TCreateDto entity)
    {
        var entityEntry = _ctx.Set<TEntity>().Add(_mapper.Map<TEntity>(entity));
        _ctx.SaveChanges();

        return _mapper.Map<TEntityDto>(entityEntry.Entity);
    }

    public async Task<TEntityDto> CreateAsync(TCreateDto entity)
    {
        var entityEntry = await _ctx.Set<TEntity>().AddAsync(_mapper.Map<TEntity>(entity));
        await _ctx.SaveChangesAsync();

        return _mapper.Map<TEntityDto>(entityEntry.Entity);
    }

    public TEntityDto Update(TUpdateDto entity)
    {
        var existingEntity = _ctx.Set<TEntity>().Find(entity.Id);
        if (existingEntity == null)
            throw new EntityNotFoundException($"Entity of type {typeof(TEntity).FullName} with ID {entity.Id} was not found.");

        var updateType = typeof(TUpdateDto);
        var entityType = typeof(TEntity);
        var entityProperties = entityType.GetProperties().ToList();

        foreach (var updateProp in updateType.GetProperties())
        {
            var newVal = updateProp.GetValue(entity);
            var entProp = entityProperties.Single(item => item.Name == updateProp.Name);
            entProp.SetValue(existingEntity, newVal);
        }

        var entityEntry = _ctx.Set<TEntity>().Update(existingEntity);
        _ctx.SaveChanges();

        return _mapper.Map<TEntityDto>(entityEntry.Entity);
    }

    public async Task<TEntityDto> UpdateAsync(TUpdateDto entity)
    {
        var existingEntity = await _ctx.Set<TEntity>().FindAsync(entity.Id);
        if (existingEntity == null)
            throw new EntityNotFoundException($"Entity of type {typeof(TEntity).FullName} with ID {entity.Id} was not found.");

        var updateType = typeof(TUpdateDto);
        var entityType = typeof(TEntity);
        var entityProperties = entityType.GetProperties().ToList();

        foreach (var updateProp in updateType.GetProperties())
        {
            var newVal = updateProp.GetValue(entity);
            var entProp = entityProperties.Single(item => item.Name == updateProp.Name);
            entProp.SetValue(existingEntity, newVal);
        }

        var entityEntry = _ctx.Set<TEntity>().Update(existingEntity);
        await _ctx.SaveChangesAsync();

        return _mapper.Map<TEntityDto>(entityEntry.Entity);
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

    public void Delete(TEntityDto entity)
    {
        _ctx.Set<TEntity>().Remove(_mapper.Map<TEntity>(entity));
        _ctx.SaveChanges();
    }

    public async Task DeleteAsync(TEntityDto entity)
    {
        _ctx.Set<TEntity>().Remove(_mapper.Map<TEntity>(entity));
        await _ctx.SaveChangesAsync();
    }
}