namespace HrHarmony.Data.Repositories.AccessLimiters.SyncQuery;

public interface ISyncQueryExecuter<TEntityDto>
{
    public List<TEntityDto> ToList();

    public List<TEntityDto> ToList(Func<TEntityDto, bool> predicate);

    public int Count();

    public long LongCount();

    public TEntityDto First();

    public TEntityDto? FirstOrDefault();

    public TEntityDto First(Func<TEntityDto, bool> predicate);

    public TEntityDto? FirstOrDefault(Func<TEntityDto, bool> predicate);

    public TEntityDto Single();

    public TEntityDto? SingleOrDefault();

    public TEntityDto Single(Func<TEntityDto, bool> predicate);

    public TEntityDto? SingleOrDefault(Func<TEntityDto, bool> predicate);

    public TEntityDto Last();

    public TEntityDto? LastOrDefault();

    public TEntityDto Last(Func<TEntityDto, bool> predicate);

    public TEntityDto? LastOrDefault(Func<TEntityDto, bool> predicate);

    public TEntityDto? Min();

    public TEntityDto? Max();

    public decimal Min(Func<TEntityDto, decimal> selector);

    public decimal Max(Func<TEntityDto, decimal> selector);

    public int Sum(Func<TEntityDto, int> selector);

    public decimal Sum(Func<TEntityDto, decimal> selector);

    public bool Any();

    public bool Any(Func<TEntityDto, bool> predicate);

    public bool All(Func<TEntityDto, bool> predicate);

    public decimal Average(Func<TEntityDto, decimal> selector);

    public bool Contains(TEntityDto item);

    public bool Contains(TEntityDto item, IEqualityComparer<TEntityDto> comparer);

    public IEnumerable<TEntityDto> Where(Func<TEntityDto, bool> predicate);

    public List<TEntityDto> Reverse();

    public List<TEntityDto> Distinct();

    public List<TEntityDto> OrderBy(Func<TEntityDto, object> keySelector);

    public List<TEntityDto> OrderByDescending(Func<TEntityDto, object> keySelector);

    public List<IGrouping<TKey, TEntityDto>> GroupBy<TKey>(Func<TEntityDto, TKey> keySelector);

    // ==================================================
    // Dodaj inne synchroniczne operacje według potrzeb
    // ==================================================
}