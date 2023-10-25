namespace HrHarmony.Models.Dto;

public abstract class EntityDto<TKey> : IEntityDto<TKey>
{
    public TKey Id { get; set; }
}