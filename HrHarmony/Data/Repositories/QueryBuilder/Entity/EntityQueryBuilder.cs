using HrHarmony.Data.Repositories.QueryBuilder.Pagination;
using HrHarmony.Data.Repositories.QueryBuilder.ValueFilters;
using HrHarmony.Models.Entities;
using HrHarmony.Models.Shared;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace HrHarmony.Data.Repositories.QueryBuilder.Entity;

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

    private readonly IEnumerable<IValueFilterStrategy<TEntity>> _valueFilterStrategies;

    protected EntityQueryBuilder(IEnumerable<IValueFilterStrategy<TEntity>> valueFilterStrategies)
    {
        _valueFilterStrategies = valueFilterStrategies;
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

    public EntityQueryBuilder<TEntity, TPrimaryKey> ApplyFieldSorting<T>()
        where T : class, new()
    {
        var properties = typeof(T).GetProperties().Where(item => item.Name.ToLower() != "id");

        if (!string.IsNullOrWhiteSpace(OrderBy))
        {
            var orderProp = typeof(T).GetProperty(OrderBy);
            if (orderProp == null)
                OrderBy = properties.First().Name;
        }
        else
            OrderBy = properties.First().Name;

        return this;
    }

    public EntityQueryBuilder<TEntity, TPrimaryKey> ApplyOrdering()
    {
        Query = IsDescending ? Query.OrderByDescending(x =>
            EF.Property<TEntity>(x, OrderBy)) : Query.OrderBy(x => EF.Property<TEntity>(x, OrderBy));
        return this;
    }

    public EntityQueryBuilder<TEntity, TPrimaryKey> ApplySearchValueFilter<TViewModel>()
        where TViewModel : class, new()
    {
        if (string.IsNullOrWhiteSpace(SearchString)) return this;

        SearchString = SearchString.Trim().ToLower();
        var dbProperties = typeof(TViewModel).GetProperties()
            .Where(p => p.Name.ToLower() != "id" && !p.Name.ToLower().Contains("id")
                                                 && (p.PropertyType.IsValueType || p.PropertyType == typeof(string)
                                                     || Nullable.GetUnderlyingType(p.PropertyType) == typeof(string))
                                                 && !p.PropertyType.FullName.StartsWith("HrHarmony"));

        var filters = PredicateBuilder.New<TEntity>(e => false);
        foreach (var property in dbProperties)
            _valueFilterStrategies.Single(item => item.Types.Any(type => type == property.PropertyType)).ApplyFilter(filters, property, SearchString);

        Query = Query.Where(filters);
        return this;
    }

    public abstract Task<PaginatedQuery<TEntity>> BuildAsync<TViewModel>(PaginationRequest req) where TViewModel : class, new();

    public abstract PaginatedQuery<TEntity> Build<TViewModel>(PaginationRequest req) where TViewModel : class, new();

    public IQueryable<TEntity> Build()
    {
        ApplyFieldSorting<TEntity>();
        ApplySearchValueFilter<TEntity>();
        ApplyOrdering();

        return Query;
    }
}