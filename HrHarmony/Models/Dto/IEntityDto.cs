namespace HrHarmony.Models.Dto;

public interface IEntityDto<TKey>
{
    public TKey Id { get; set; }
}