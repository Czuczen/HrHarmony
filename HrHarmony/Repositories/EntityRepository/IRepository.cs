﻿using HrHarmony.Models.Entities;

namespace HrHarmony.Repositories.EntityRepository;

public interface IRepository<TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>, new()
    where TPrimaryKey : struct
{
    public Task<IEnumerable<TEntity>> GetAll();

    public Task<TEntity> GetById(TPrimaryKey id);

    public Task<IEnumerable<TEntity>> GetWhere(string key, TPrimaryKey id);

    public Task<TEntity> Create(TEntity entity);

    public Task<TEntity> Update(TEntity entity);

    public Task Delete(TPrimaryKey id);

    public Task Delete(TEntity entity);
}