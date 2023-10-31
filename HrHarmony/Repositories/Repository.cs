using AutoMapper;
using HrHarmony.Attributes;
using HrHarmony.Configuration.Database;
using HrHarmony.Configuration.Exceptions;
using HrHarmony.Models.Dto;
using HrHarmony.Models.Entities;
using HrHarmony.Models.Entities.Main;
using HrHarmony.Utils;
using Microsoft.EntityFrameworkCore;
using System;
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

    public async Task<IEnumerable<TEntityDto>> GetAll()
    {
        var entities = await _ctx.Set<TEntity>().ToListAsync();
        return _mapper.Map<IEnumerable<TEntityDto>>(entities);
    }

    public async Task<TEntityDto> GetById(TPrimaryKey id)
    {
        var entity = await _ctx.Set<TEntity>().FindAsync(id);
        return _mapper.Map<TEntityDto>(entity);
    }

    public async Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate)
    {
        return await _ctx.Set<TEntity>().Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetWhere(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryBuilder)
    {
        var query = _ctx.Set<TEntity>().AsQueryable();
        query = queryBuilder(query);

        return await query.ToListAsync();
    }

    //public async Task<IEnumerable<TEntity>> GetActiveEntitiesWithValue(int value)
    //{
    //    return await GetWhere(PredicateUtils<TEntity>.IsActive
    //                            .And(PredicateUtils<TEntity>.ByValue(value)));
    //}

    //public async Task<IEnumerable<TEntity>> GetPagedEntities(int pageNumber, int pageSize)
    //{
    //    // Oblicz ile rekordów pominąć
    //    int skip = (pageNumber - 1) * pageSize;

    //    return await _ctx.Set<TEntity>()
    //        .Skip(skip)
    //        .Take(pageSize)
    //        .ToListAsync();
    //}

    //public async Task<PaginatedResult<TEntity>> GetPagedEntities(int pageNumber, int pageSize)
    //{
    //    int skip = (pageNumber - 1) * pageSize;
    //    int totalCount = await _ctx.Set<TEntity>().CountAsync();

    //    var items = await _ctx.Set<TEntity>()
    //        .Skip(skip)
    //        .Take(pageSize)
    //        .ToListAsync();

    //    return new PaginatedResult<TEntity>
    //    {
    //        Items = items,
    //        TotalCount = totalCount,
    //        PageNumber = pageNumber,
    //        PageSize = pageSize
    //    };
    //}

    public async Task<IEnumerable<TEntityDto>> GetWhere(string key, TPrimaryKey id)
    {
        var type = typeof(TEntity);

        var entities = (await _ctx.Set<TEntity>().ToListAsync()).Where(item => type.GetProperty(key).GetValue(item).Equals(id));

        return _mapper.Map<IEnumerable<TEntityDto>>(entities);
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
        if (entity != null)
        {
            _ctx.Set<TEntity>().Remove(entity);
            await _ctx.SaveChangesAsync();
        }
        else
            throw new EntityNotFoundException("Entity not found!");
    }

    public async Task Delete(TEntityDto entity)
    {
        _ctx.Set<TEntity>().Remove(_mapper.Map<TEntity>(entity));
        await _ctx.SaveChangesAsync();
    }
}