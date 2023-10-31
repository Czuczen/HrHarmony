using AutoMapper;
using HrHarmony.Attributes;
using HrHarmony.Configuration.Database;
using HrHarmony.Configuration.Dependencies.DependencyLifecycleInterfaces;
using HrHarmony.Configuration.Exceptions;
using HrHarmony.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HrHarmony.Repositories.EntityRepository;

[RegisterOpenGenericClassInDI(typeof(Repository<,>))]
public class Repository<TEntity, TPrimaryKey> :
    IRepository<TEntity, TPrimaryKey>, ITestedDependency<TEntity, TPrimaryKey>, ITransientDependency
    where TEntity : class, IEntity<TPrimaryKey>, new()
    where TPrimaryKey : struct
{
    private readonly ApplicationDbContext _ctx;
    private readonly IMapper _mapper;

    private Guid? _instanceGuid;
    public Guid InstanceGuid => (Guid)(_instanceGuid ?? (_instanceGuid = Guid.NewGuid()));

    public Repository(IMapper mapper)
    {
        _ctx = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());
        _mapper = mapper;
    }

    public void Aa() { }

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