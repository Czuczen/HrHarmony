using AutoMapper;
using HrHarmony.Configuration.Database;
using HrHarmony.Models.Dto;
using HrHarmony.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HrHarmony.Repositories;

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

    public async Task<IEnumerable<TEntityDto>> GetWhere(string key, TPrimaryKey id)
    {
        var type = typeof(TEntity);

        var entities = (await _ctx.Set<TEntity>().ToListAsync()).Where(item => type.GetProperty(key).GetValue(item).Equals(id));

        return _mapper.Map<IEnumerable<TEntityDto>>(entities);
    }

    public async Task Create(TCreateDto entity)
    {
        await _ctx.Set<TEntity>().AddAsync(_mapper.Map<TEntity>(entity));
        await _ctx.SaveChangesAsync();
    }

    public async Task Update(TUpdateDto entity)
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

        //var ent = await _ctx.Set<TEntity>().FindAsync(entity.Id);

        //var entry = _ctx.Entry(ent);
        //entry.State = EntityState.Detached;

        //var mappedEntity = _mapper.Map<TEntity>(entity);
        //var updatedEntity = _mapper.Map(mappedEntity, ent);

        _ctx.Set<TEntity>().Update(existingEntity);
        await _ctx.SaveChangesAsync();
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
        {
            throw new InvalidOperationException("Encja nie istnieje!");
        }
    }

    public async Task Delete(TEntityDto entity)
    {
        _ctx.Set<TEntity>().Remove(_mapper.Map<TEntity>(entity));
        await _ctx.SaveChangesAsync();
    }
}