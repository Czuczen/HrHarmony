namespace HrHarmony.Data.Repositories.AccessLimiters.SyncQuery;

public class SyncQueryExecuter<TEntityDto> : ISyncQueryExecuter<TEntityDto>
{
    private readonly IQueryable<TEntityDto> _query;

    public SyncQueryExecuter(IQueryable<TEntityDto> query)
    {
        _query = query;
    }

    public List<TEntityDto> ToList() => _query.ToList();

    public List<TEntityDto> ToList(Func<TEntityDto, bool> predicate) => _query.AsEnumerable().Where(predicate).ToList();

    public int Count() => _query.Count();

    public long LongCount() => _query.LongCount();

    public TEntityDto First() => _query.First();

    public TEntityDto? FirstOrDefault() => _query.FirstOrDefault();

    public TEntityDto First(Func<TEntityDto, bool> predicate) => _query.First(predicate);

    public TEntityDto? FirstOrDefault(Func<TEntityDto, bool> predicate) => _query.FirstOrDefault(predicate);

    public TEntityDto Single() => _query.Single();

    public TEntityDto? SingleOrDefault() => _query.SingleOrDefault();

    public TEntityDto Single(Func<TEntityDto, bool> predicate) => _query.Single(predicate);

    public TEntityDto? SingleOrDefault(Func<TEntityDto, bool> predicate) => _query.SingleOrDefault(predicate);

    public TEntityDto Last() => _query.Last();

    public TEntityDto? LastOrDefault() => _query.LastOrDefault();

    public TEntityDto Last(Func<TEntityDto, bool> predicate) => _query.Last(predicate);

    public TEntityDto? LastOrDefault(Func<TEntityDto, bool> predicate) => _query.LastOrDefault(predicate);

    public TEntityDto? Min() => _query.Min();

    public TEntityDto? Max() => _query.Max();

    public decimal Min(Func<TEntityDto, decimal> selector) => _query.Min(selector);

    public decimal Max(Func<TEntityDto, decimal> selector) => _query.Max(selector);

    public int Sum(Func<TEntityDto, int> selector) => _query.Sum(selector);

    public decimal Sum(Func<TEntityDto, decimal> selector) => _query.Sum(selector);

    public bool Any() => _query.Any();

    public bool Any(Func<TEntityDto, bool> predicate) => _query.Any(predicate);

    public bool All(Func<TEntityDto, bool> predicate) => _query.All(predicate);

    public decimal Average(Func<TEntityDto, decimal> selector) => _query.Average(selector);

    public bool Contains(TEntityDto item) => _query.Contains(item);

    public bool Contains(TEntityDto item, IEqualityComparer<TEntityDto> comparer) => _query.Contains(item, comparer);

    public IEnumerable<TEntityDto> Where(Func<TEntityDto, bool> predicate) => _query.AsEnumerable().Where(predicate);

    public List<TEntityDto> Reverse() => _query.Reverse().ToList();

    public List<TEntityDto> Distinct() => _query.Distinct().ToList();

    public List<TEntityDto> OrderBy(Func<TEntityDto, object> keySelector) => _query.AsEnumerable().OrderBy(keySelector).ToList();

    public List<TEntityDto> OrderByDescending(Func<TEntityDto, object> keySelector) => _query.AsEnumerable().OrderByDescending(keySelector).ToList();

    public List<IGrouping<TKey, TEntityDto>> GroupBy<TKey>(Func<TEntityDto, TKey> keySelector) => _query.AsEnumerable().GroupBy(keySelector).ToList();

    // ==================================================
    // Dodaj inne synchroniczne operacje według potrzeb
    // ==================================================
}