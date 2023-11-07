using HrHarmony.Attributes;
using HrHarmony.Data.QueryBuilder.Entity;
using HrHarmony.Data.QueryBuilder.ValueFilters;
using HrHarmony.Models.Entities;
using HrHarmony.Models.Shared;
using Microsoft.EntityFrameworkCore;

namespace HrHarmony.Data.QueryBuilder.Pagination;

[RegisterOpenGenericClassInDI(typeof(PaginatedQueryBuilder<,>))]
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

    public override async Task<PaginatedQuery<TEntity>> BuildAsync<TViewModel>(PaginationRequest req)
        where TViewModel : class
    {
        WithTotalCount(await BaseQuery.CountAsync());
        WithPageNumber(req.PageNumber);
        WithPageSize(req.PageSize);
        WithIsDescending(req.IsDescending);
        WithOrderBy(req.OrderBy);
        WithSearch(req.SearchString);

        ApplyFieldSorting<TViewModel>();
        ApplySearchValueFilter<TViewModel>();
        ApplyOrdering();

        var searchedCount = await Query.CountAsync();
        var newTotalPages = (int)Math.Ceiling((double)searchedCount / _pageSize);
        _pageNumber = _pageNumber <= newTotalPages ? _pageNumber : newTotalPages;
        var skip = (_pageNumber - 1) * _pageSize;

        Query = Query.Skip(skip).Take(_pageSize);

        return new PaginatedQuery<TEntity>
        {
            TotalCount = TotalCount,
            PageNumber = _pageNumber,
            PageSize = _pageSize,
            OrderBy = OrderBy,
            IsDescending = IsDescending,
            SearchString = SearchString,
            SearchedCount = searchedCount,
            Query = Query,
        };
    }

    public override PaginatedQuery<TEntity> Build<TViewModel>(PaginationRequest req) 
        where TViewModel : class
    {
        WithTotalCount(BaseQuery.Count());
        WithPageNumber(req.PageNumber);
        WithPageSize(req.PageSize);
        WithIsDescending(req.IsDescending);
        WithOrderBy(req.OrderBy);
        WithSearch(req.SearchString);

        ApplyFieldSorting<TViewModel>();
        ApplySearchValueFilter<TViewModel>();
        ApplyOrdering();

        var searchedCount = Query.Count();
        var newTotalPages = (int)Math.Ceiling((double) searchedCount / _pageSize);
        _pageNumber = _pageNumber <= newTotalPages ? _pageNumber : newTotalPages;
        var skip = (_pageNumber - 1) * _pageSize;

        Query = Query.Skip(skip).Take(_pageSize);

        return new PaginatedQuery<TEntity> 
        {
            TotalCount = TotalCount,
            PageNumber = _pageNumber,
            PageSize = _pageSize,
            OrderBy = OrderBy,
            IsDescending = IsDescending,
            SearchString = SearchString,
            SearchedCount = searchedCount,
            Query = Query,
        };
    }
}