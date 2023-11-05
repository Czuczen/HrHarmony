using AutoMapper;
using HrHarmony.Attributes;
using HrHarmony.Configuration.Database;
using HrHarmony.Configuration.Exceptions;
using HrHarmony.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HrHarmony.Repositories.EntityRepository;

[RegisterOpenGenericClassInDI(typeof(Repository<,>))]
public class Repository<TEntity, TPrimaryKey> :
    IRepository<TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>, new()
    where TPrimaryKey : struct
{
    private readonly ApplicationDbContext _ctx;
    private readonly IMapper _mapper;

    public Repository(ApplicationDbContext context, IMapper mapper)
    {
        _ctx = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TEntity>> GetAll()
    {
        return await _ctx.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity> GetById(TPrimaryKey id)
    {
        return await _ctx.Set<TEntity>().FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetWhere(string key, TPrimaryKey id)
    {
        var type = typeof(TEntity);

        return (await _ctx.Set<TEntity>().ToListAsync()).Where(item => type.GetProperty(key).GetValue(item).Equals(id));
    }

    public async Task<TEntity> Create(TEntity entity)
    {
        var entityEntry = await _ctx.Set<TEntity>().AddAsync(_mapper.Map<TEntity>(entity));
        await _ctx.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public async Task<TEntity> Update(TEntity entity)
    {
        var entityEntry = _ctx.Set<TEntity>().Update(entity);
        await _ctx.SaveChangesAsync();

        return entityEntry.Entity;
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

    public async Task Delete(TEntity entity)
    {
        _ctx.Set<TEntity>().Remove(_mapper.Map<TEntity>(entity));
        await _ctx.SaveChangesAsync();
    }
}