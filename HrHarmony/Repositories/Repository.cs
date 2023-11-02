﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using HrHarmony.Attributes;
using HrHarmony.Configuration.Database;
using HrHarmony.Configuration.Exceptions;
using HrHarmony.Models.Dto;
using HrHarmony.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HrHarmony.Repositories;

[RegisterOpenGenericClassInDI(typeof(Repository<,,,,>))]
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

    public Repository(ApplicationDbContext context, IMapper mapper)
    {
        _ctx = context;
        _mapper = mapper;
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
        return _ctx.Set<TEntity>().Where(predicate).Select(entity => _mapper.Map<TEntityDto>(entity));
    }

    public IQueryable<TEntityDto> GetQuery(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryBuilder)
    {
        var query = _ctx.Set<TEntity>().AsQueryable();
        query = queryBuilder(query);
   
        return query.Select(entity => _mapper.Map<TEntityDto>(entity));
    }

    public IQueryable<TEntityDto> GetQueryWithRelated(Expression<Func<TEntity, bool>> predicate)
    {
        return _ctx.Set<TEntity>().Where(predicate).ProjectTo<TEntityDto>(_mapper.ConfigurationProvider);
    }

    public IQueryable<TEntityDto> GetQueryWithRelated(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryBuilder)
    {
        var query = _ctx.Set<TEntity>().AsQueryable();
        query = queryBuilder(query);

        return query.ProjectTo<TEntityDto>(_mapper.ConfigurationProvider);
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

    public IEnumerable<TEntityDto> ExecuteQueryWithRelated(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryBuilder)
    {
        var query = _ctx.Set<TEntity>().AsQueryable();
        query = queryBuilder(query);

        return query.ProjectTo<TEntityDto>(_mapper.ConfigurationProvider).ToList();
    }

    public async Task<IEnumerable<TEntityDto>> ExecuteQueryWithRelatedAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryBuilder)
    {
        var query = _ctx.Set<TEntity>().AsQueryable();
        query = queryBuilder(query);

        return await query.ProjectTo<TEntityDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    // do wywalenia na koniec
    public async Task<IEnumerable<TEntityDto>> GetWhere(string key, TPrimaryKey id)
    {
        var type = typeof(TEntity);

        var entities = (await _ctx.Set<TEntity>().ToListAsync()).Where(item => type.GetProperty(key).GetValue(item).Equals(id));

        return _mapper.Map<IEnumerable<TEntityDto>>(entities);
    }

    public PaginatedResult<TEntityDto> GetPagedEntities(int pageNumber, int pageSize)
    {
        var skip = (pageNumber - 1) * pageSize;
        var totalCount = _ctx.Set<TEntity>().Count();

        var items = _ctx.Set<TEntity>().Skip(skip).Take(pageSize).ToList();
        var entities = _mapper.Map<IEnumerable<TEntityDto>>(items);

        return new PaginatedResult<TEntityDto>
        {
            Items = entities,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<PaginatedResult<TEntityDto>> GetPagedEntitiesAsync(int pageNumber, int pageSize)
    {
        var skip = (pageNumber - 1) * pageSize;
        var totalCount = await _ctx.Set<TEntity>().CountAsync();

        var items = await _ctx.Set<TEntity>().Skip(skip).Take(pageSize).ToListAsync();
        var entities = _mapper.Map<IEnumerable<TEntityDto>>(items);

        return new PaginatedResult<TEntityDto>
        {
            Items = entities,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public PaginatedResult<TEntityDto> GetPagedEntitiesWithRelated(int pageNumber, int pageSize)
    {
        var skip = (pageNumber - 1) * pageSize;
        var totalCount = _ctx.Set<TEntity>().Count();

        var query = _ctx.Set<TEntity>().Skip(skip).Take(pageSize);
        var entities = query.ProjectTo<TEntityDto>(_mapper.ConfigurationProvider).ToList();

        return new PaginatedResult<TEntityDto>
        {
            Items = entities,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<PaginatedResult<TEntityDto>> GetPagedEntitiesWithRelatedAsync(int pageNumber, int pageSize)
    {
        var skip = (pageNumber - 1) * pageSize;
        var totalCount = await _ctx.Set<TEntity>().CountAsync();

        var query = _ctx.Set<TEntity>().Skip(skip).Take(pageSize);
        var entities = await query.ProjectTo<TEntityDto>(_mapper.ConfigurationProvider).ToListAsync();

        return new PaginatedResult<TEntityDto>
        {
            Items = entities,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public PaginatedResult<TEntityDto> GetPagedEntitiesWithCustomFields(
     int pageNumber, int pageSize, Func<IQueryable<TEntity>, IQueryable<object>> customProjection)
    {
        var skip = (pageNumber - 1) * pageSize;
        var totalCount = _ctx.Set<TEntity>().Count();
        var query = _ctx.Set<TEntity>().Skip(skip).Take(pageSize);

        var customResult = customProjection(query);
        var entities = customResult.ToList();
        var mappedEntities = _mapper.Map<IEnumerable<TEntityDto>>(entities);

        return new PaginatedResult<TEntityDto>
        {
            Items = mappedEntities,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<PaginatedResult<TEntityDto>> GetPagedEntitiesWithCustomFieldsAsync(
    int pageNumber, int pageSize, Func<IQueryable<TEntity>, IQueryable<object>> customProjection)
    {
        var skip = (pageNumber - 1) * pageSize;
        var totalCount = await _ctx.Set<TEntity>().CountAsync();
        var query = _ctx.Set<TEntity>().Skip(skip).Take(pageSize);

        var customResult = customProjection(query);
        var entities = await customResult.ToListAsync();
        var mappedEntities = _mapper.Map<IEnumerable<TEntityDto>>(entities);

        return new PaginatedResult<TEntityDto>
        {
            Items = mappedEntities,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    // GetPagedEntitiesWithCustomFieldsAsync  dodać with related????

    public async Task<TEntityDto> GetEntityWithCustomFields(TPrimaryKey id, Func<IQueryable<TEntity>, IQueryable<object>> customProjection)
    {
        var query = _ctx.Set<TEntity>().Where(e => e.Id.Equals(id));
        var customResult = customProjection(query);
        var entity = (await customResult.ToListAsync()).Single();

        return _mapper.Map<TEntityDto>(entity); // da rade zmapować
    }

    //public async Task<IEnumerable<TEntity>> GetActiveEntitiesWithValue(int value)
    //{
    //    return await GetWhere(PredicateUtils<TEntity>.IsActive
    //                            .And(PredicateUtils<TEntity>.ByValue(value)));
    //}

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




    public async Task<TEntityDto> Create(TCreateDto entity)
    {
        var entityEntry = await _ctx.Set<TEntity>().AddAsync(_mapper.Map<TEntity>(entity));
        await _ctx.SaveChangesAsync();

        return _mapper.Map<TEntityDto>(entityEntry.Entity);
    }

    public async Task<TEntityDto> Update(TUpdateDto entity)
    {
        var updateType = typeof(TUpdateDto);
        var entityType = typeof(TEntity);
        var entityProperties = entityType.GetProperties().ToList();
        var existingEntity = await _ctx.Set<TEntity>().FindAsync(entity.Id);

        foreach (var property in updateType.GetProperties())
        {
            var newVal = property.GetValue(entity);
            var entProp = entityProperties.FirstOrDefault(item => item.Name == property.Name);

            if (newVal != null && entProp != null)
                entProp.SetValue(existingEntity, newVal);
        }

        var entityEntry = _ctx.Set<TEntity>().Update(existingEntity);
        await _ctx.SaveChangesAsync();

        return _mapper.Map<TEntityDto>(entityEntry.Entity);
    }

    public async Task Delete(TPrimaryKey id)
    {
        var entity = await _ctx.Set<TEntity>().FindAsync(id);
        if (entity == null)
            throw new EntityNotFoundException($"Entity of type {typeof(TEntity).FullName} with ID {id} was not found.");

        _ctx.Set<TEntity>().Remove(entity);
        await _ctx.SaveChangesAsync();
    }

    public async Task Delete(TEntityDto entity)
    {
        _ctx.Set<TEntity>().Remove(_mapper.Map<TEntity>(entity));
        await _ctx.SaveChangesAsync();
    }
}