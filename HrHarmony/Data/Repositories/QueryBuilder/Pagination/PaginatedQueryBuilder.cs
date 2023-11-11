using HrHarmony.Attributes;
using HrHarmony.Data.Models.Entities;
using HrHarmony.Data.Models.Shared;
using HrHarmony.Data.Repositories.QueryBuilder.Entity;
using HrHarmony.Data.Repositories.QueryBuilder.ValueFilters;
using Microsoft.EntityFrameworkCore;

namespace HrHarmony.Data.Repositories.QueryBuilder.Pagination;

[RegisterOpenGenericClassInDi(typeof(PaginatedQueryBuilder<,>))]
public class PaginatedQueryBuilder<TEntity, TPrimaryKey> :
    EntityQueryBuilder<TEntity, TPrimaryKey>,
    IPaginatedQueryBuilder<TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>, new()
    where TPrimaryKey : struct
{
    private int _pageNumber;
    private int _pageSize;

    public PaginatedQueryBuilder(IEnumerable<IValueFilterStrategy<TEntity>> valueFilterStrategies)
        : base(valueFilterStrategies)
    {
    }


    public PaginatedQueryBuilder<TEntity, TPrimaryKey> WithPageNumber(int pageNumber)
    {
        _pageNumber = pageNumber;
        return this;
    }

    public PaginatedQueryBuilder<TEntity, TPrimaryKey> WithPageSize(int pageSize)
    {
        _pageSize = pageSize;
        return this;
    }

    public override async Task<PaginatedQuery<TEntity>> BuildAsync<T>(PaginationRequest req)
        where T : class
    {
        WithTotalCount(await BaseQuery.CountAsync());
        WithPageNumber(req.PageNumber);
        WithPageSize(req.PageSize);
        WithIsDescending(req.IsDescending);
        WithOrderBy(req.OrderBy);
        WithSearch(req.SearchString);

        ApplyFieldSorting<T>();
        ApplySearchValueFilter<T>();
        ApplyOrdering();

        var searchedCount = await Query.CountAsync();
        var newTotalPages = (int)Math.Ceiling((double)searchedCount / _pageSize);
        var newPageNumber = (_pageNumber <= newTotalPages ? _pageNumber : newTotalPages);
        _pageNumber = newPageNumber <= 0 ? 1 : newPageNumber;
        var skip = (_pageNumber - 1) * _pageSize;

        Query = Query.Skip(skip).Take(_pageSize);

        return new PaginatedQuery<TEntity>
        {
            TotalCount = TotalCount,
            PageNumber = _pageNumber,
            PageSize = _pageSize,
            OrderBy = OrderBy!,
            IsDescending = IsDescending,
            SearchString = SearchString,
            SearchedCount = searchedCount,
            Query = Query,
        };
    }

    public override PaginatedQuery<TEntity> Build<T>(PaginationRequest req)
        where T : class
    {
        WithTotalCount(BaseQuery.Count());
        WithPageNumber(req.PageNumber);
        WithPageSize(req.PageSize);
        WithIsDescending(req.IsDescending);
        WithOrderBy(req.OrderBy);
        WithSearch(req.SearchString);

        ApplyFieldSorting<T>();
        ApplySearchValueFilter<T>();
        ApplyOrdering();

        var searchedCount = Query.Count();
        var newTotalPages = (int)Math.Ceiling((double)searchedCount / _pageSize);
        var newPageNumber = (_pageNumber <= newTotalPages ? _pageNumber : newTotalPages);
        _pageNumber = newPageNumber <= 0 ? 1 : newPageNumber;
        var skip = (_pageNumber - 1) * _pageSize;

        Query = Query.Skip(skip).Take(_pageSize);

        return new PaginatedQuery<TEntity>
        {
            TotalCount = TotalCount,
            PageNumber = _pageNumber,
            PageSize = _pageSize,
            OrderBy = OrderBy!,
            IsDescending = IsDescending,
            SearchString = SearchString,
            SearchedCount = searchedCount,
            Query = Query,
        };
    }
}