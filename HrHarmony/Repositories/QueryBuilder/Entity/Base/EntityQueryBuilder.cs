using HrHarmony.Models.Entities;
using HrHarmony.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace HrHarmony.Repositories.QueryBuilder.Entity.Base
{
    public abstract class EntityQueryBuilder<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>, new()
        where TPrimaryKey : struct
    {
        protected IQueryable<TEntity> Query;
        protected IQueryable<TEntity> BaseQuery;
        protected int TotalCount;
        protected string? OrderBy;
        protected string SearchString;
        protected bool IsDescending;


        public EntityQueryBuilder()
        {
        }

        public EntityQueryBuilder<TEntity, TPrimaryKey> WithBaseQuery(IQueryable<TEntity> baseQuery)
        {
            BaseQuery = baseQuery;
            Query = baseQuery;
            return this;
        }

        public EntityQueryBuilder<TEntity, TPrimaryKey> WithTotalCount(int totalCount)
        {
            TotalCount = totalCount;
            return this;
        }

        public EntityQueryBuilder<TEntity, TPrimaryKey> WithOrderBy(string? orderBy)
        {
            OrderBy = orderBy;
            return this;
        }

        public EntityQueryBuilder<TEntity, TPrimaryKey> WithSearch(string searchString)
        {
            SearchString = searchString;
            return this;
        }

        public EntityQueryBuilder<TEntity, TPrimaryKey> WithIsDescending(bool isDescending)
        {
            IsDescending = isDescending;
            return this;
        }

        public EntityQueryBuilder<TEntity, TPrimaryKey> ApplySorting<T>()
           where T : class, new()
        {
            var properties = typeof(T).GetProperties();

            if (!string.IsNullOrWhiteSpace(OrderBy))
            {
                var orderProp = typeof(T).GetProperty(OrderBy);
                if (orderProp == null)
                    OrderBy = properties.First().Name;
            }

            OrderBy = properties.First().Name;
            return this;
        }

        public EntityQueryBuilder<TEntity, TPrimaryKey> ApplyOrdering()
        {
            Query = IsDescending ? Query.OrderByDescending(x =>
                    EF.Property<TEntity>(x, OrderBy)) : Query.OrderBy(x => EF.Property<TEntity>(x, OrderBy));
            return this;
        }

        public abstract PaginatedQuery<TEntity> Build<TViewModel>(PaginationRequest req) where TViewModel : class, new();

        public IQueryable<TEntity> Build()
        {
            ApplySorting<TEntity>();
            ApplyOrdering();

            return Query;
        }
    }
}