using HrHarmony.Models.Dto;
using HrHarmony.Models.Entities;

namespace HrHarmony.Repositories;

public interface IRepository<TEntity, TPrimaryKey, TEntityDto, TUpdateDto, TCreateDto>
    where TEntity : class, IEntity<TPrimaryKey>, new()
    where TPrimaryKey : struct
    where TEntityDto : class, IEntityDto<TPrimaryKey>, new()
    where TUpdateDto : class, IEntityDto<TPrimaryKey>, new()
    where TCreateDto : class, new()
{
    public Task<IEnumerable<TEntityDto>> GetAll();

    public Task<TEntityDto> GetById(TPrimaryKey id);

    public Task<IEnumerable<TEntityDto>> GetWhere(string key, TPrimaryKey id);

    public Task Create(TCreateDto entity);

    public Task Update(TUpdateDto entity);

    public Task Delete(TPrimaryKey id);

    public Task Delete(TEntityDto entity);
}