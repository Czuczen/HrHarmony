using HrHarmony.Attributes;
using HrHarmony.Models.Entities;
using HrHarmony.Repositories.Models;
using HrHarmony.Repositories.QueryBuilder.Entity.Base;
using HrHarmony.Repositories.QueryBuilder.Filters;
using LinqKit;

namespace HrHarmony.Repositories.QueryBuilder
{
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

        public override PaginatedQuery<TEntity> Build<TViewModel>(PaginationRequest req) 
            where TViewModel : class
        {
            WithPageNumber(req.PageNumber);
            WithPageSize(req.PageSize);
            WithIsDescending(req.IsDescending);
            WithOrderBy(req.OrderBy);
            WithSearch(req.SearchString);
            
            var newTotalPages = (int)Math.Ceiling((double) TotalCount / _pageSize);
            _pageNumber = _pageNumber <= newTotalPages ? _pageNumber : newTotalPages;
            var skip = (_pageNumber - 1) * _pageSize;

            ApplyFieldSorting<TViewModel>();
            ApplySearchValueFilter<TViewModel>();
            ApplyOrdering();

            var searchedCount = Query.Count();
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
}