using HrHarmony.Attributes;
using HrHarmony.Models.Entities;
using HrHarmony.Repositories.Models;
using HrHarmony.Repositories.QueryBuilder.Entity.Base;

namespace HrHarmony.Repositories.QueryBuilder
{
    [RegisterOpenGenericInterfaceInDI(typeof(IPaginatedQueryBuilder<,>))]
    public interface IPaginatedQueryBuilder<TEntity, TPrimaryKey> : IEntityQueryBuilder<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>, new()
        where TPrimaryKey : struct
    {
        public PaginatedQueryBuilder<TEntity, TPrimaryKey> WithPageNumber(int pageNumber);

        public PaginatedQueryBuilder<TEntity, TPrimaryKey> WithPageSize(int pageSize);

        public Task<PaginatedQuery<TEntity>> BuildAsync<TViewModel>(PaginationRequest req)
            where TViewModel : class, new();

        public PaginatedQuery<TEntity> Build<TViewModel>(PaginationRequest req)
            where TViewModel : class, new();
    }
}